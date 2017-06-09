using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum RollResultEnum { Fail, Normal, Crit}

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

    public SpecialManager specialManager;

    public GroupFighter monsterGroupFighter;
    public GroupFighter humanGroupFighter;     // En espérant que les GD ne demande pas de monster vs monster <3

    public List<Order> combatOrder = new List<Order>();
    public int currentFighterIndex = -1;

    public bool bTurnInProgress = false;
    public Fighter currentFighter;
    public int currentInitiative = 0;
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
    public bool waitedOneFrame = false;


    public ScriptManager scriptManager;
    public TalkManager talkManager;

    public bool bDialogueInProgres = false;
    public int nCurrentLine = 0;

    public ActionWheel actionWheel;
    // public bool bDialogueHasEnded = false;

    public CombatManagerUI combatManagerUI;

    private CreaturePrefabManager creaturePrefabManager;

    public Human defaultHuman;

    public float timeBeforeStartFight = 1.0f;

    public ProtoScript protoScript = null;

    private int successfulTalkCount = 0;

    public Tip discoveredTip;

    public bool actionLaunched = false;

    public bool bSpecialFight= false;
    public SpecialType specialType;
    public GroupIA specialGroup;

    public RollProbaManager rollProbaManager;

    public bool isBossCombat = false;

    void Start()
    {

        bool canFight = GameObject.FindGameObjectWithTag("CombatTerrain").GetComponent<CombatTerrainInfo>().bCanFightHere;

        if (!canFight)
            gameObject.SetActive(false);

        GameObject rollObj = GameObject.FindGameObjectWithTag("RollProbaManager");
        if (rollObj != null)
        {
            rollProbaManager = rollObj.GetComponent<RollProbaManager>();
        }

        discoveredTip = null ;

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g != null)
        {
            protoScript = g.GetComponent<ProtoScript>();
        }

        specialManager = GameObject.FindGameObjectWithTag("SpecialManager").GetComponent<SpecialManager>();

        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
        fighterMouvementManager = GameObject.FindGameObjectWithTag("FighterMouvementManager").GetComponent<FighterMouvementManager>();
        creaturePrefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();


        
        float rand = Random.Range(0f, 1f);

        
        if (rand < rollProbaManager.specialProba.probaFight && protoScript == null && !isBossCombat)
        {

            float rand2 = Random.Range(0f, 1f);

            if (rand2 < rollProbaManager.specialProba.bard)
            {
                if (!specialManager.iaBard.endStory)
                {
                    bSpecialFight = true;
                    specialType = SpecialType.Bard;
                }

            }
            else if (!specialManager.iaEd.isDead)
            {
                bSpecialFight = true;
                specialType = SpecialType.Ed;
            }

        }
        

        InstantiateMonster();
        InstantiateHuman();


        monsterGroupFighter.groupLogic = logic.GetComponent<PlayerLogic>();

        /*
         *         
        bSpecialFight = true;
                specialType = SpecialType.Ed;
        humanGroupFighter.groupLogic = specialManager.iaEd;
        ((IAEd)(humanGroupFighter.groupLogic)).groupEd = (GroupEd) humanGroupFighter;

        */


        if (bSpecialFight)
        {
            if(specialType == SpecialType.Bard)
            {
                humanGroupFighter.groupLogic = specialManager.iaBard;
                ((IABard)(humanGroupFighter.groupLogic)).groupBard = (GroupBard)humanGroupFighter;
                specialManager.iaBard.combatJustStarted = true;
            }
            else
            {
                humanGroupFighter.groupLogic = specialManager.iaEd;
                ((IAEd)(humanGroupFighter.groupLogic)).groupEd = (GroupEd)humanGroupFighter;
            }
        }else
        {
            humanGroupFighter.groupLogic = logic.GetComponent<GroupIA>();
            ((GroupIA)humanGroupFighter.groupLogic).groupHumanFighter = (GroupHumanFighter)humanGroupFighter;
        }


        RollInitiative();

        bCombatStarted = false;

        Invoke("CanStartCombat", timeBeforeStartFight);

        //Play Music 
        AkSoundEngine.SetSwitch("Tension", "T2", gameObject);
        AkSoundEngine.PostEvent("Play_FightMusic", gameObject);

        for (int i = 0; i <  SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).name != "Menu" && SceneManager.GetSceneAt(i).name != "CARTE")
                AkSoundEngine.PostEvent(SceneManager.GetSceneAt(i).name,gameObject);
        }
    }

    public void CanStartCombat()
    {
        bCombatStarted = true;

        monsterGroupFighter.SetInitialFighters();
        humanGroupFighter.SetInitialFighters();
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
            {
                AkSoundEngine.SetSwitch("Tension", "T4", gameObject);
                combatOrder.Remove(order);
            }
        }
        for (int i = 0; i < combatOrder.Count; i++)
        {
            if (combatOrder[i].fighter == currentFighter && combatOrder[i].nInitiative == currentInitiative)
            {
                currentFighterIndex = i;
                break;
            }
        }


        if(monsterGroupFighter.lFighters.Count < 4)
        {
            if(caravane.lFighters.Count>= 4)
            {
                Fighter lastDeadFighter = monsterGroupFighter.ReplacDeadFighter();

                GameObject prefab = prefabMonster;
                CreatureType creatureType = CreatureType.Monster;

                Fighter fighter = caravane.lFighters[3];

                fighter.bTryToescape = false;
                GameObject g = Instantiate(prefab, fighterMouvementManager.SpawnPosition, Quaternion.Euler(0, 90, 0)) as GameObject;

                GameObject mo;

                GameObject model = creaturePrefabManager.GetMonster(fighter.nID);
                mo = Instantiate(model, Vector3.zero, Quaternion.Euler(0, 270, 0)) as GameObject;


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

                fighterMouvementManager.lastDeadFighterPosition = lastDeadFighter.currentUI.gameObject.transform.position;
                fighterMouvementManager.SpawnMonster(g);

            }
        }
    }

    void CheckCombatEnded()
    {
        if (!bCombatEnded)
        {
            this.monsterGroupFighter.CheckFightersLife();
            this.humanGroupFighter.CheckFightersLife();

            if (this.monsterGroupFighter.allFightersDead)
            {
                bCombatEnded = true;
                combatEndType = CombatEndType.MonstersDead;
                AkSoundEngine.PostEvent("CombatLose", gameObject);
            }
            else if (this.monsterGroupFighter.bEscaping)
            {
                bCombatEnded = true;
                combatEndType = CombatEndType.MonsterEscape;
                fighterMouvementManager.bMonsterRun = true;
                AkSoundEngine.PostEvent("CombatLose", gameObject);
            }
            else if (this.humanGroupFighter.allFightersDead)
            {
                bCombatEnded = true;
                combatEndType = CombatEndType.HumansDead;
                AkSoundEngine.PostEvent("CombatWin", gameObject);
            }

                if (((GroupHumanFighter)humanGroupFighter).bIsConviced)
                {
                    bCombatEnded = true;
                    combatEndType = CombatEndType.HumansConvinced;
                    AkSoundEngine.PostEvent("CombatWin", gameObject);
                }
                if (((GroupHumanFighter)humanGroupFighter).bIsFeared)
                {
                    bCombatEnded = true;
                    combatEndType = CombatEndType.HumansFeared;
                    fighterMouvementManager.bHumanRun = true;
                    AkSoundEngine.PostEvent("CombatWin", gameObject);
                    AkSoundEngine.PostEvent("Play_flee", gameObject);
                }

            if (bCombatEnded)
            {
                if(bSpecialFight && specialType == SpecialType.Bard)
                {
                    ((IABard)humanGroupFighter.groupLogic).EndCombat();
                }
                if (bSpecialFight && specialType == SpecialType.Ed)
                {
                    ((IAEd)humanGroupFighter.groupLogic).EndCombat();
                }
                if (combatEndType != CombatManager.CombatEndType.MonsterEscape && combatEndType != CombatManager.CombatEndType.MonstersDead)
                {
                    if (discoveredTip == null)
                    {
                        TipsManager tipManager = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>();
                        discoveredTip = tipManager.GetRandTipConcerningGroups((GroupMonsterFighter)monsterGroupFighter, (GroupHumanFighter)humanGroupFighter);
                    }
                }

            }
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
                if (!waitedOneFrame)
                    waitedOneFrame = true;
                else
                {
                    ActionEnded();
                    waitedOneFrame = false;
                }
            }

            if (!bTurnInProgress && !fighterMouvementManager.bFighterJoiningCombat)
            {
                if (protoScript != null)
                    if(protoScript.combat != null)
                        protoScript.combat.NextTurn();

                currentFighter = GetNextFighter();
                

                actionLaunched = false;
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
                        if (protoScript != null && protoScript.combat != null && protoScript.combat.currentTurn != null)
                            actionChoosed =  ActionType.GetActionTypeWithID(protoScript.combat.currentTurn.ActionCode);
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
                        if(currentFighter.eCreatureType == CreatureType.Monster)
                        {
                            if (((Monster)currentFighter).isBoss)
                            {
                                actionChoosed = ActionType.FEAR;
                                bActionChoosed = true;
                            }
                        }
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
        if (!actionLaunched)
        {
            actionLaunched = true;

            if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
            {
                uint state;
                AkSoundEngine.GetSwitch("Tension", gameObject, out state);
                if (actionChoosed == ActionType.ATTACK && currentFighter.GetCreatureType() == CreatureType.Human && state != 4)
                {
                    AkSoundEngine.SetSwitch("Tension", "T3", gameObject);
                }

                currentFighter.PerformActionOnTarget(actionChoosed, targetChoosed);
            }
            else if (actionChoosed.GetTargetType() == ActionType.ActionTargetType.AllTarget)
            {
                if (currentFighter.GetCreatureType() == CreatureType.Human)
                {
                    currentFighter.PerformActionOnTarget(actionChoosed, monsterGroupFighter);
                    if (actionChoosed == ActionType.FEAR)
                    {
                        //   AkSoundEngine.SetSwitch("Tension", "T3", gameObject);
                    }
                    else if (actionChoosed == ActionType.TALK)
                    {

                        successfulTalkCount++;
                        if (successfulTalkCount > 1)
                        {
                            AkSoundEngine.SetSwitch("Tension", "T1", gameObject);
                        }
                        AkSoundEngine.PostEvent("Play_HumanTalk", gameObject);
                    }
                }
                else
                {
                    currentFighter.PerformActionOnTarget(actionChoosed, humanGroupFighter);
                    if (actionChoosed == ActionType.FEAR)
                    {
                        AkSoundEngine.PostEvent("Play_" + currentFighter.sName + "Fear", gameObject);
                    }
                    if (actionChoosed == ActionType.TALK)
                    {
                        AkSoundEngine.PostEvent("Play_" + currentFighter.sName + "Talk", gameObject);
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
        }else if (!currentFighter.performingAction)
        {
            PutFighterInInitialPosition();
        }


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

        if (protoScript != null)
        {
            bool takeM = true;

            if (protoScript.combat.iteration == 2)
                takeM = false;


            int indM = 0;
            int indH = 0;

            int nbFighters = 0;
            nbFighters += monsterGroupFighter.lFighters.Count;
            nbFighters += humanGroupFighter.lFighters.Count;

            for (int i = 0; i < 8; i++)
            {
                if (takeM)
                {
                    int initiative = monsterGroupFighter.lFighters[indM].GetRandomInitiative();
                    Order order = new Order(monsterGroupFighter.lFighters[indM], i);
                    combatOrder.Add(order);
                    indM++;
                }
                else
                {
                    int initiative = humanGroupFighter.lFighters[indH].GetRandomInitiative();
                    Order order = new Order(humanGroupFighter.lFighters[indH], i);
                    combatOrder.Add(order);
                    indH++;
                }

                takeM = !takeM;

            }




        }
        else
        {
            /*
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
            */

            bool takeM = true;

            float rand = Random.Range(0f, 1f);

            if (rand > 0.5)
                takeM = false;


            int indM = 0;
            int indH = 0;


            bool blocked = false;

            int nbFighters = 0;
            nbFighters += monsterGroupFighter.lFighters.Count;
            nbFighters += humanGroupFighter.lFighters.Count;

            if (humanGroupFighter.bIsSpecial)
            {
                if (specialType == SpecialType.Ed || specialType == SpecialType.Bard)
                {
                    nbFighters = 0;
                    nbFighters += monsterGroupFighter.lFighters.Count;
                    nbFighters += monsterGroupFighter.lFighters.Count;

                    for (int i = 0; i < nbFighters; i++)
                    {
                        if (takeM)
                        {
                            //  int initiative = monsterGroupFighter.lFighters[indM].GetRandomInitiative();
                            Order order = new Order(monsterGroupFighter.lFighters[indM], i);
                            combatOrder.Add(order);
                            indM++;

                        }
                        else
                        {
                            //  int initiative = humanGroupFighter.lFighters[indH].GetRandomInitiative();
                            Order order = new Order(humanGroupFighter.lFighters[0], i);
                            combatOrder.Add(order);
                            indH++;
                        }

                        takeM = !takeM;

                    }
                }
            }else
            {
                for (int i = 0; i < nbFighters; i++)
                {
                    if (takeM)
                    {
                        // int initiative = monsterGroupFighter.lFighters[indM].GetRandomInitiative();
                        Order order = new Order(monsterGroupFighter.lFighters[indM], i);
                        combatOrder.Add(order);
                        indM++;

                        if (!blocked)
                            takeM = !takeM;

                        if (indM >= monsterGroupFighter.lFighters.Count)
                            blocked = true;


                    }
                    else
                    {
                        // int initiative = humanGroupFighter.lFighters[indH].GetRandomInitiative();
                        Order order = new Order(humanGroupFighter.lFighters[indH], i);
                        combatOrder.Add(order);
                        indH++;

                        if (!blocked)
                            takeM = !takeM;

                        if (indH >= humanGroupFighter.lFighters.Count)
                            blocked = true;
                    }
                }


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


            for (int i = 0; i < nNbCreaturePerGroup && i < caravane.lFighters.Count; i++)
            {
                fighter = caravane.lFighters[i];
                fighter.bTryToescape = false;
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

         if (isBossCombat)
        {

            monstersPosition = new List<Transform>();
            foreach (Transform child in monstersPositionParent.transform)
            {
                monstersPosition.Add(child);
            }

            humanGroupFighter = new GroupEd();

            int idModel = 40;
            GameObject model = creaturePrefabManager.GetSpecial(idModel);
            //Human humain = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetFighterOfID<Human>(creatureType, idModel);
            Monster monster = new Monster();
            monster.nID = idModel;
            monster.sName = "PlantePirate";
            monster.isBoss = true;
            monster.nPower = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().defaultMonster.nPower;
            monster.nHealthMax = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().defaultMonster.nHealthMax;
            monster.nCurrentHealth = monster.nHealthMax;

            GameObject g = Instantiate(prefab, monstersPosition[4].position, Quaternion.Euler(0, 90, 0)) as GameObject;

            GameObject mo = Instantiate(model, monstersPosition[4].position, Quaternion.Euler(0, 90, 0)) as GameObject;
            mo.transform.parent = g.transform;
            mo.transform.localPosition = Vector3.zero;
            mo.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);

            mo.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

            MouseOverCreature mouseOver = mo.transform.GetChild(0).gameObject.AddComponent<MouseOverCreature>();
            mouseOver.fighterUI = g.GetComponent<FighterUI>();

            g.GetComponent<FighterUI>().fighter = monster;
            monsterGroupFighter.lFighters.Add(monster);
            g.transform.parent = GameObject.FindGameObjectWithTag("Monsters").transform;
            g.name = monster.sName;

            g.GetComponent<FighterUI>().fighterRenderer = mouseOver.gameObject.GetComponent<Renderer>();
        }
 

    }
    void InstantiateHuman()
    {


        GameObject prefab = prefabHuman;
        Fighter fighter;
        CreatureType creatureType = CreatureType.Human;

        GameObject humansPositionParent = GameObject.FindGameObjectWithTag("HumansPosition");
        List<Transform> humansPosition = new List<Transform>();
        foreach (Transform child in humansPositionParent.transform)
        {
            humansPosition.Add(child);
        }

        if (!bSpecialFight)
        {
            humanGroupFighter = new GroupHumanFighter();

            for (int i = 0; i < nNbCreaturePerGroup; i++)
            {
                int idModel = creaturePrefabManager.GetRandomHumanID();
                GameObject model = creaturePrefabManager.GetHuman(idModel);
                Human humain = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetFighterOfID<Human>(creatureType, idModel);

                ModelHumainUI modelUI = model.GetComponent<ModelHumainUI>();
                if (modelUI != null)
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
        }else
        {
            if(specialType == SpecialType.Ed)
            {
                humanGroupFighter = new GroupEd();

                int idModel = 30;
                GameObject model = creaturePrefabManager.GetSpecial(idModel);
                //Human humain = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetFighterOfID<Human>(creatureType, idModel);
                Human humain = new Human();
                humain.CopyHuman(defaultHuman);
                humain.nID = idModel;
                humain.sName = "Ed";
                humain.nPower = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().defaultHuman.nPower;

                GameObject g = Instantiate(prefab, humansPosition[0].position, Quaternion.Euler(0, 90, 0)) as GameObject;

                GameObject mo = Instantiate(model, humansPosition[0].position, Quaternion.Euler(0, 90, 0)) as GameObject;
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
            if (specialType == SpecialType.Bard)
            {
                humanGroupFighter = new GroupBard();
                int idModel = 31;
                GameObject model = creaturePrefabManager.GetSpecial(idModel);
                //Human humain = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetFighterOfID<Human>(creatureType, idModel);
                Human humain = new Human();
                humain.CopyHuman(defaultHuman);
                humain.nID = idModel;
                humain.sName = "Bard";

                GameObject g = Instantiate(prefab, humansPosition[0].position, Quaternion.Euler(0, 90, 0)) as GameObject;

                GameObject mo = Instantiate(model, humansPosition[0].position, Quaternion.Euler(0, 90, 0)) as GameObject;
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
        }




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

        bool changeFighter = false;

        while ((fighter == null || changeFighter) || counter > 20)
        {

            currentFighterIndex++;

            if (combatOrder.Count == 0)
                Debug.LogError("WTF");
            if (currentFighterIndex >= combatOrder.Count)
                currentFighterIndex = 0;

            fighter = combatOrder[currentFighterIndex].fighter;
            currentInitiative = combatOrder[currentFighterIndex].nInitiative;

            if (!fighter.CanAttack())
            {
                fighter = null;
            }

            if (bSpecialFight && fighter!= null)
            {
                if(currentFighter == fighter)
                {
                    changeFighter = true;
                }else
                {
                    changeFighter = false;
                }
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
