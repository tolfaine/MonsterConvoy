using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionType
{
    public enum ActionEnum { Attack = 0, Escape = 1, Talk = 2, Fear = 3, Negociate = 4 };
    public enum ActionTargetType { OneTarget, AllTarget, NoTarget };

    public static readonly ActionType ATTACK = new ActionType(0, CreatureType.None, ActionTargetType.OneTarget, "ATTACK");
    public static readonly ActionType ESCAPE = new ActionType(1, CreatureType.None, ActionTargetType.NoTarget, "ESCAPE");
    public static readonly ActionType TALK = new ActionType(2, CreatureType.None, ActionTargetType.AllTarget, "TALK");
    public static readonly ActionType FEAR = new ActionType(3, CreatureType.Monster, ActionTargetType.AllTarget, "FEAR");
    public static readonly ActionType NEGOTIATE = new ActionType(4, CreatureType.Human, ActionTargetType.AllTarget, "NEGOTIATE");

    private int nId;
    private CreatureType eCreatureType;
    private bool bRequireTarget;
    private ActionTargetType targetType;
    public string sName;

    private ActionType(int id, CreatureType type, ActionTargetType targetType, string name)
    {
        this.nId = id;
        this.eCreatureType = type;
        // this.bRequireTarget = requireTarget;
        this.targetType = targetType;
        this.sName = name;
    }
    // public bool DoesRequireTarget() { return this.bRequireTarget;}
    public ActionTargetType GetTargetType() { return this.targetType; }
    public static ActionType GetActionTypeWithID(int id)
    {
        switch (id)
        {
            case 0:
                return ATTACK;
            case 1:
                return ESCAPE;
            case 2:
                return TALK;
            case 3:
                return FEAR;
            case 4:
                return NEGOTIATE;
            default:
                return null;
        }
    }
}

[System.Serializable]
public class Order
{
    public Fighter fighter;
    public int nInitiative;

    public Order(Fighter fighter, int initiative)
    {
        this.fighter = fighter;
        this.nInitiative = initiative;
    }
}


public class CombatManager : MonoBehaviour
{

    public enum CombatEndType { MonstersDead, HumansDead, HumansConvinced, HumansFeared, MonsterEscape }

    private int currentTension = 1; //Used to deny us from going from heavy tension to light tension. I'm assuming this is desired. 

    // public List<GameObject> lMonsterPrefab;
    //  public List<GameObject> lHumanPrefab;

    public GroupFighter monsterGroupFighter;
    public GroupFighter humanGroupFighter;     // En espérant que les GD ne demande pas de monster vs monster <3
    public List<Order> combatOrder = new List<Order>();
    public int currentFighterIndex = -1;

    public bool bTurnInProgress = false;
    public Fighter currentFighter;
    public GroupLogic currentGroupLogic;

    public ActionType actionChoosed = null;
    public bool bActionChoosed = false;
    public bool bActionRequireTarget; // ???? Je sais pas trop. Mais faut un truc du genre quelque part pour le fear et fuite;

    public Fighter targetChoosed;
    public bool bTargetChoosed = false;

    public bool bActionInProgress = false; // Quand on lance l'action et que les animations (entre autres) sont en cours. On attend que tout soit finit pour passer au prochain tour

    public bool bCombatStarted = false;
    // public bool         bMonsterWin = false;
    public bool bCombatEnded = false;
    public CombatEndType combatEndType;

    public GameObject prefabMonster;
    public GameObject prefabHuman;

    public GameObject logic;

    int nNbCreaturePerGroup = 4;

    public Caravane caravane;
    public HumanCaravane humanCamp;

    public FighterMouvementManager fighterMouvementManager;


    public bool bFighterInFightPosition = false;
    public bool bFighterInInitialPosition = true;


    public ScriptManager scriptManager;
    public TalkManager talkManager;

    public bool bDialogueInProgres = false;
    public int nCurrentLine = 0;

    public ActionWheel actionWheel;
    // public bool bDialogueHasEnded = false;

    public CombatManagerUI combatManagerUI;

    private CreaturePrefabManager creaturePrefabManager;

    public Human defaultHuman;

    void Start()
    {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
        fighterMouvementManager = GameObject.FindGameObjectWithTag("FighterMouvementManager").GetComponent<FighterMouvementManager>();
        creaturePrefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();

        InstantiateMonster();
        InstantiateHuman();

        monsterGroupFighter.groupLogic = logic.GetComponent<PlayerLogic>();
        humanGroupFighter.groupLogic = logic.GetComponent<GroupIA>();

        ((GroupIA)humanGroupFighter.groupLogic).groupHumanFighter = (GroupHumanFighter)humanGroupFighter;

        RollInitiative();

        bCombatStarted = true;

        //Play Music
        //AkSoundEngine.SetSwitch("Tension", "T2", gameObject);
       // AkSoundEngine.PostEvent("Play_FightMusic", gameObject);

    }

    void Update()
    {

        CheckDialogue();
        CheckCombatEnded();
        CheckDeadFighters();

        if (!bCombatEnded)
            ProcessCombat();
    }
    /*
    public int GetNextIndexLine()
    {
        return scriptManager.GetNextIndexLine();
    }
    */
    void CheckDialogue()
    {
        if (combatManagerUI.DialogueInProgress())
            bDialogueInProgres = true;
        else
            bDialogueInProgres = false;
    }

    void CheckDeadFighters()
    {
        foreach (Order order in combatOrder)
        {
            if (order.fighter.IsDead())
                combatOrder.Remove(order);
        }
        for (int i = 0; i < combatOrder.Count; i++)
        {
            if (combatOrder[i].fighter == currentFighter)
                currentFighterIndex = i;
        }
    }

    void CheckCombatEnded()
    {
        this.monsterGroupFighter.CheckFightersLife();
        this.humanGroupFighter.CheckFightersLife();

        if (this.monsterGroupFighter.allFightersDead)
        {
            bCombatEnded = true;
            combatEndType = CombatEndType.MonstersDead;
        }
        else if (this.monsterGroupFighter.bEscaping)
        {
            bCombatEnded = true;
            combatEndType = CombatEndType.MonsterEscape;
            fighterMouvementManager.bMonsterRun = true;
        }
        else if (this.humanGroupFighter.allFightersDead)
        {
            bCombatEnded = true;
            combatEndType = CombatEndType.HumansDead;
        }

        if (((GroupHumanFighter)humanGroupFighter).bIsConviced)
        {
            bCombatEnded = true;
            combatEndType = CombatEndType.HumansConvinced;
        }
        if (((GroupHumanFighter)humanGroupFighter).bIsFeared)
        {
            bCombatEnded = true;
            combatEndType = CombatEndType.HumansFeared;
            fighterMouvementManager.bHumanRun = true;
        }

    }

    void ProcessCombat()
    {
        if (!bCombatStarted)
        {

        }
        else
        {
            if (bActionInProgress && bFighterInInitialPosition)
            {
                ActionEnded();
            }

            if (!bTurnInProgress)
            {
                if (scriptManager != null)
                    scriptManager.NextTurn();

                currentFighter = GetNextFighter();

                actionWheel.SetFighter(currentFighter);

                if (currentFighter.eCreatureType == CreatureType.Monster)
                {
                    // actionWheel.SetAction(ActionType.FEAR, ((GroupHumanFighter)humanGroupFighter).bCanBeFeared);
                    // actionWheel.SetAction(ActionType.TALK, ((GroupHumanFighter)humanGroupFighter).bCanListen);
                }

                currentGroupLogic = GetGroupLogicOfFighter(currentFighter);
                bTurnInProgress = true;
                targetChoosed = null;
                PutFighterInFightPosition();

            }
            else if (!bActionInProgress && bFighterInFightPosition)
            {

                if (currentGroupLogic.GetLogicType() == LogicType.IA)
                {
                    if (!bActionChoosed)
                    {
                        if (scriptManager != null && scriptManager.currentTurn != null)
                            actionChoosed = scriptManager.currentTurn.actionType;
                        else
                            actionChoosed = ((GroupIA)currentGroupLogic).SelectAction(monsterGroupFighter.lFighters, humanGroupFighter.lFighters);

                        bActionChoosed = true;

                        if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
                            bActionRequireTarget = true;
                        else
                            bActionRequireTarget = false;
                    }
                    else if (bActionRequireTarget && !bTargetChoosed)
                    {
                        targetChoosed = ((GroupIA)currentGroupLogic).SelectTarget(monsterGroupFighter.lFighters, humanGroupFighter.lFighters);
                        bTargetChoosed = true;
                    }
                    else
                    {
                        // Current Fighter perform action on target
                        PerformAction();
                    }
                }
                else  // If LogicType = Player
                {
                    if (!bActionChoosed)
                    {
                        // Choose action
                    }
                    else if (bActionRequireTarget && !bTargetChoosed)
                    {
                        // Chose Target
                    }
                    else
                    {
                        // Current Fighter perform action on target
                        // Debug.Log("Action in progress player");
                        PerformAction();
                    }
                }
            }
        }
    }
    void PerformAction()
    {
        if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
        {
            if (actionChoosed == ActionType.ATTACK && currentFighter.GetCreatureType() == CreatureType.Human && currentTension < 4)
            {
                currentTension = 3;
               // AkSoundEngine.SetSwitch("Tension", "T3", gameObject);
            }

            currentFighter.PerformActionOnTarget(actionChoosed, targetChoosed);
        }
        else if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.AllTarget)
        {
            if (currentFighter.GetCreatureType() == CreatureType.Human)
            {
                currentFighter.PerformActionOnTarget(actionChoosed, monsterGroupFighter);
                if (actionChoosed == ActionType.FEAR && currentTension < 3)
                {
                    currentTension = 2;
                   // AkSoundEngine.SetSwitch("Tension", "T2", gameObject);
                }
                else if (actionChoosed == ActionType.TALK && currentTension < 3)
                {
                   // AkSoundEngine.PostEvent("Play_HumanTalk", gameObject);
                }
            }
            else
            {
                currentFighter.PerformActionOnTarget(actionChoosed, humanGroupFighter);
                if (actionChoosed == ActionType.FEAR)
                { }
                if (actionChoosed == ActionType.TALK)
                {
                    switch (currentFighter.sName)
                    {
                        case "Slime":
                           // AkSoundEngine.PostEvent("Play_SlimeTalk", gameObject);
                            break;
                        case "Quadrapus":
                           // AkSoundEngine.PostEvent("Play_KappaTalk", gameObject);
                            break;
                        case "Decalepus":
                          //  AkSoundEngine.PostEvent("Play_MummyTalk", gameObject);
                            break;
                        case "Gentlacule":
                           // AkSoundEngine.PostEvent("Play_SirenTalk", gameObject);
                            break;
                    }
                }
            }
        }
        else if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.NoTarget)
        {
            if (currentFighter.GetCreatureType() == CreatureType.Human)
                currentFighter.PerformActionOnSelf(actionChoosed, humanGroupFighter);
            else
                currentFighter.PerformActionOnSelf(actionChoosed, monsterGroupFighter);
        }

        PutFighterInInitialPosition();
        // Invoke("ActionEnded", 2);
    }
    void PutFighterInInitialPosition()
    {

        fighterMouvementManager.bMoveToInitialPosition = true;
        bActionInProgress = true;
        bFighterInInitialPosition = false;
    }

    void PutFighterInFightPosition()
    {
        bFighterInInitialPosition = false;
        fighterMouvementManager.SetFighter(currentFighter.currentUI.gameObject);
        fighterMouvementManager.bMoveToFightPosition = true;
    }

    void RollInitiative()
    {

        if (scriptManager != null)
        {
            int idFghter = 0;
            int initiative = 0;

            foreach (Fighter fighter in monsterGroupFighter.lFighters)
            {
                initiative = fighter.GetRandomInitiative();
                Order order = new Order(fighter, initiative);
                combatOrder.Add(order);
            }

            foreach (Fighter fighter in humanGroupFighter.lFighters)
            {
                initiative = fighter.GetRandomInitiative();
                Order order = new Order(fighter, initiative);
                combatOrder.Add(order);
            }


            foreach (Order order in combatOrder)
            {
                int fighterId = order.fighter.nID;

                foreach (ScriptOrder scriptOrder in scriptManager.lOrder)
                {
                    if (scriptOrder.nId == fighterId)
                        order.nInitiative = scriptOrder.nRoll;
                }
            }
        }
        else
        {
            foreach (Fighter fighter in monsterGroupFighter.lFighters)
            {
                int initiative = fighter.GetRandomInitiative();
                Order order = new Order(fighter, initiative);
                combatOrder.Add(order);
            }

            foreach (Fighter fighter in humanGroupFighter.lFighters)
            {
                int initiative = fighter.GetRandomInitiative();
                Order order = new Order(fighter, initiative);
                combatOrder.Add(order);
            }
        }

        combatOrder.Sort(SortByInitiative);
    }
    void InstantiateMonster()
    {
        monsterGroupFighter = new GroupMonsterFighter();

        GameObject prefab = prefabMonster;
        Fighter fighter;
        CreatureType creatureType = CreatureType.Monster;

        GameObject monstersPositionParent = GameObject.FindGameObjectWithTag("MonstersPosition");
        List<Transform> monstersPosition = new List<Transform>();
        foreach (Transform child in monstersPositionParent.transform)
        {
            monstersPosition.Add(child);
        }

        for (int i = 0; i < nNbCreaturePerGroup; i++)
        {
            fighter = caravane.lFighters[i];
            GameObject g = Instantiate(prefab, monstersPosition[i].position, Quaternion.Euler(0, 90, 0)) as GameObject;

            GameObject mo;

            GameObject model = creaturePrefabManager.GetMonster(fighter.nID);
            mo = Instantiate(model, monstersPosition[i].position, Quaternion.Euler(0, 270, 0)) as GameObject;
                

            mo.transform.parent = g.transform;
            mo.transform.localPosition = Vector3.zero;
            mo.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            mo.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();


            g.GetComponent<FighterUI>().fighter = fighter;
            monsterGroupFighter.lFighters.Add(fighter);
            g.transform.parent = GameObject.FindGameObjectWithTag("Monsters").transform;
            g.name = fighter.sName;

            MouseOverCreature mouseOver = mo.transform.GetChild(0).gameObject.AddComponent<MouseOverCreature>();
            mouseOver.fighterUI = g.GetComponent<FighterUI>();

            g.GetComponent<FighterUI>().fighterRenderer = mouseOver.gameObject.GetComponent<Renderer>();
        }
    }
    void InstantiateHuman()
    {
        humanGroupFighter = new GroupHumanFighter();

        GameObject prefab = prefabHuman;
        Fighter fighter;
        CreatureType creatureType = CreatureType.Human;

        GameObject humansPositionParent = GameObject.FindGameObjectWithTag("HumansPosition");
        List<Transform> humansPosition = new List<Transform>();
        foreach (Transform child in humansPositionParent.transform)
        {
            humansPosition.Add(child);
        }

        for (int i = 0; i < nNbCreaturePerGroup; i++)
        {
            int idModel = creaturePrefabManager.GetRandomHumanID();
            GameObject model = creaturePrefabManager.GetHuman(idModel);
            Human humain = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetFighterOfID<Human>(creatureType, idModel);

            ModelHumainUI modelUI = model.GetComponent<ModelHumainUI>();
            if(modelUI != null)
            {
                modelUI.RandomCheveux();
            }

            if (humain == null)
            {
                humain = new Human();
                humain.CopyHuman(defaultHuman);
                humain.nID = idModel;
                //humain.sName = model.name;
            }



            GameObject g = Instantiate(prefab, humansPosition[i].position, Quaternion.Euler(0, 90, 0)) as GameObject;

            GameObject mo = Instantiate(model, humansPosition[i].position, Quaternion.Euler(0, 90, 0)) as GameObject;
            mo.transform.parent = g.transform;
            mo.transform.localPosition = Vector3.zero;
            mo.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            mo.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

            MouseOverCreature mouseOver = mo.transform.GetChild(0).gameObject.AddComponent<MouseOverCreature>();
            mouseOver.fighterUI = g.GetComponent<FighterUI>();

            g.GetComponent<FighterUI>().fighter = humain;
            humanGroupFighter.lFighters.Add(humain);
            g.transform.parent = GameObject.FindGameObjectWithTag("Humans").transform;
            g.name = humain.sName;

            g.GetComponent<FighterUI>().fighterRenderer = mouseOver.gameObject.GetComponent<Renderer>();
        }
        /*
         
        for (int i = 0; i < nNbCreaturePerGroup; i++)
        {
            fighter = null;

            if (humanCamp != null)
                fighter = humanCamp.lFighters[i];
            else
            {
                fighter = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetRandomFighter<Human>(creatureType);
            }
                

            GameObject g = Instantiate(prefab, humansPosition[i].position, Quaternion.Euler(0, 90, 0)) as GameObject;

            GameObject mo = Instantiate(humanCamp.lFighters[i].prefab, humansPosition[i].position, Quaternion.Euler(0, 90, 0)) as GameObject;
            mo.transform.parent = g.transform;
            mo.transform.localPosition = Vector3.zero;
            mo.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            mo.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

            MouseOverCreature mouseOver = mo.transform.GetChild(0).gameObject.AddComponent<MouseOverCreature>();
            mouseOver.fighterUI = g.GetComponent<FighterUI>();

            g.GetComponent<FighterUI>().fighter = fighter;
            humanGroupFighter.lFighters.Add(fighter);
            g.transform.parent = GameObject.FindGameObjectWithTag("Humans").transform;
            g.name = fighter.sName;

            g.GetComponent<FighterUI>().fighterRenderer = mouseOver.gameObject.GetComponent<Renderer>();
        }

        */
    }
    void NextFighterTurn() { }

    void ActionEnded()
    {
        // Debug.Log("Action End");
        bTurnInProgress = false;
        bActionInProgress = false;
        bActionChoosed = false;
        bActionRequireTarget = false;
        bTargetChoosed = false;

        targetChoosed = null;
        actionChoosed = null;

    }
    public void PlayerClickedCreature(Creature creature)
    {
        //  Debug.Log("[CombatManager] PlayerClickedCreature()");

        if (creature.GetCreatureType() != currentFighter.GetCreatureType())
        {
            targetChoosed = (Fighter)creature;
            bTargetChoosed = true;
        }
    }
    public void PlayerClickedAction(ActionType actionType)
    {
        // Debug.Log("[CombatManager] PlayerClickedAction()");

        actionChoosed = actionType;
        bActionChoosed = true;

        if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
            bActionRequireTarget = true;
        else
            bActionRequireTarget = false;
    }

    public GroupFighter GetGroupFighterOfFighter(Fighter fighter)
    {
        if (fighter.GetCreatureType() == CreatureType.Monster)
            return monsterGroupFighter;
        if (fighter.GetCreatureType() == CreatureType.Human)
            return humanGroupFighter;
        else
            return null;
    }

    GroupLogic GetGroupLogicOfFighter(Fighter fighter)
    {
        if (fighter.GetCreatureType() == CreatureType.Monster)
            return monsterGroupFighter.GetGroupLogic();
        if (fighter.GetCreatureType() == CreatureType.Human)
            return humanGroupFighter.GetGroupLogic();
        else
            return null;
    }
    Fighter GetNextFighter()
    {

        int counter = 0;
        Fighter fighter = null;

        while (fighter == null || counter > 20)
        {
            currentFighterIndex++;

            if (combatOrder.Count == 0)
                Debug.LogError("WTF");
            if (currentFighterIndex >= combatOrder.Count)
                currentFighterIndex = 0;

            fighter = combatOrder[currentFighterIndex].fighter;

            if (!fighter.CanAttack()) //Does this mean they're dead? 
            {
                fighter = null;
               // AkSoundEngine.SetSwitch("Tension", "T4", gameObject);
                currentTension = 4;
            }
            counter++;
        }

        return fighter;

    }

    static int SortByInitiative(Order o1, Order o2)
    {
        return o1.nInitiative.CompareTo(o2.nInitiative);
    }

}
