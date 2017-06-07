using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TipsManager : MonoBehaviour {
    public static bool created = false;
    public List<Tip> listTips = new List<Tip>();

    public List<Tip> tipsNotKnownByPlayer = new List<Tip>();
    public List<Tip> tipsKnownByPlayer = new List<Tip>();

    public Tip lastRevealedTip = null;


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);

        Initialise();


    }
    // Use this for initialization

    void Start () {
       // LearnTip(tipsNotKnownByPlayer[0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Initialise()
    {
        CaractHumainStuff test = CaractHumainStuff.ARC;
        CaractHumainCheveux test2 = CaractHumainCheveux.BLANC;

        List<CaractMonster> allCaracMonster = new List<CaractMonster>();
        List<CaractHumain> allCaracHumain = new List<CaractHumain>();

        CaractMonster[] allCaracMonsterA = new CaractMonster[CaractMonster.GetAllCarac().Count];
        CaractHumain[] allCaracHumainA = new CaractHumain[CaractHumain.GetAllCarac().Count];

        CaractMonster.GetAllCarac().CopyTo(allCaracMonsterA);
        CaractHumain.GetAllCarac().CopyTo(allCaracHumainA);

        allCaracMonster = allCaracMonsterA.ToList();
        allCaracHumain = allCaracHumainA.ToList();

        //System.Array.Copy( , allCaracMonster, CaractMonster.GetAllCarac().Count);
        Tip tip;

        for (int i = 0; i < allCaracMonster.Count && allCaracHumain.Count>0;)
        {
            tip = new Tip();

            int randIndexMonster = Random.Range(0, allCaracMonster.Count);
            int randIndexHumain = Random.Range(0, allCaracHumain.Count);

            tip.caracMonster = allCaracMonster[randIndexMonster];
            tip.caracHumain = allCaracHumain[randIndexHumain];

            allCaracMonster.RemoveAt(randIndexMonster);
            allCaracHumain.RemoveAt(randIndexHumain);

            listTips.Add(tip);

            ModRoll roll = ModRoll.GetRandomMod();

            tip.modroll = roll;

        }

        CaractMonster.GetAllCarac().CopyTo(allCaracMonsterA);
        allCaracMonster = allCaracMonsterA.ToList();

        for (int i = 0; i < allCaracMonster.Count && allCaracHumain.Count > 0;)
        {
            tip = new Tip();

            int randIndexMonster = Random.Range(0, allCaracMonster.Count);
            int randIndexHumain = Random.Range(0, allCaracHumain.Count);

            tip.caracMonster = allCaracMonster[randIndexMonster];
            tip.caracHumain = allCaracHumain[randIndexHumain];

            allCaracMonster.RemoveAt(randIndexMonster);
            allCaracHumain.RemoveAt(randIndexHumain);

            ModRoll roll = ModRoll.GetRandomMod();

            tip.modroll = roll;

            listTips.Add(tip);
        }

        tipsNotKnownByPlayer = listTips;
        int lel = 10;
    }

    public Tip GetRandomUnknownTip()
    {
        int randIndex = Random.Range(0, tipsNotKnownByPlayer.Count);
        return tipsNotKnownByPlayer[randIndex];
    }

    public void LearnTip(Tip tip)
    {
        lastRevealedTip = tip;

        CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

        if (combatManager.protoScript != null && combatManager.protoScript.combat != null)
        {
            combatManager.protoScript.combat.customTalk.caractMonster = tip.caracMonster.enumCaract.ToString();
            combatManager.protoScript.combat.customTalk.caractHumain = tip.caracHumain. sName;
        }


        if (tipsNotKnownByPlayer.Contains(tip))
        {
            tipsKnownByPlayer.Add(tip);
            tipsNotKnownByPlayer.Remove(tip);
        }
    }

    public float GetBonus(ActionType actionType, Monster monster, GroupHumanFighter groupHuman)
    {
        float bonus = 0;

        List<Tip> myTips = GetTipsOfAction(actionType, monster.eCreatureType);

        CaractMonster cm = monster.currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>().caractMonster;

        myTips = GetTipsAboutMonsterCarac(cm, myTips);

        foreach (Fighter fighter in groupHuman.lFighters)
        {
            if (fighter.CanAttack())
            {
                ModelHumainUI modelH = ((Human)fighter).currentUI.gameObject.GetComponentInChildren<ModelHumainUI>();

                CaractHumainCheveux cheveux = modelH.caractCheveux;
                CaractHumainStuff stuff = modelH.caractStuff;

                foreach (Tip tip in myTips)
                {
                    if (tip.caracHumain == cheveux)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, monster.eCreatureType);
                    }
                    else if (tip.caracHumain == stuff)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, monster.eCreatureType);
                    }

                    if (bonus != 0)
                        break;
                }
            }

            if (bonus != 0)
                break;
        }


        myTips = GetTipsOfAction(actionType, monster.eCreatureType);
        cm = monster.currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>().permanentCarMutation;
        myTips = GetTipsAboutMonsterCarac(cm, myTips);

        foreach (Fighter fighter in groupHuman.lFighters)
        {
            if (fighter.CanAttack())
            {
                ModelHumainUI modelH = ((Human)fighter).currentUI.gameObject.GetComponentInChildren<ModelHumainUI>();
                CaractHumainCheveux cheveux = modelH.caractCheveux;
                CaractHumainStuff stuff = modelH.caractStuff;

                foreach (Tip tip in myTips)
                {
                    if (tip.caracHumain == cheveux)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, monster.eCreatureType);
                    }
                    else if (tip.caracHumain == stuff)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, monster.eCreatureType);
                    }

                    if (bonus != 0)
                        break;
                }
            }

            if (bonus != 0)
                break;
        }
        //   CaractMonster mutation;


        return bonus;
    }
    public float GetBonus(ActionType actionType, Human human, GroupMonsterFighter groupMonster)
    {
        float bonus = 0;

        List<Tip> myTips = GetTipsOfAction(actionType, human.eCreatureType);

        CaractHumainCheveux chm = human.currentUI.gameObject.GetComponentInChildren<ModelHumainUI>().caractCheveux;

        myTips = GetTipsAboutHumainCarac(chm, myTips);

        foreach (Fighter fighter in groupMonster.lFighters)
        {
            if (fighter.CanAttack())
            {
                ModelMonsterUI modelM = ((Monster)fighter).currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>();

                CaractMonster tempCarac = modelM.caractMonster;
                CaractMonster permaCarac = modelM.permanentCarMutation;

                foreach (Tip tip in myTips)
                {
                    if (tip.caracMonster == tempCarac)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, human.eCreatureType);
                    }
                    else if (tip.caracMonster == permaCarac)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, human.eCreatureType);
                    }

                    if (bonus  != 0)
                        break;
                }
            }

            if (bonus != 0)
                break;
        }


         myTips = GetTipsOfAction(actionType, human.eCreatureType);

        CaractHumainStuff chs = human.currentUI.gameObject.GetComponentInChildren<ModelHumainUI>().caractStuff;

        myTips = GetTipsAboutHumainCarac(chs, myTips);

        foreach (Fighter fighter in groupMonster.lFighters)
        {
            if (fighter.CanAttack())
            {
                ModelMonsterUI modelM = ((Monster)fighter).currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>();

                CaractMonster tempCarac = modelM.caractMonster;
                CaractMonster permaCarac = modelM.permanentCarMutation;

                foreach (Tip tip in myTips)
                {
                    if (tip.caracMonster == tempCarac)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, human.eCreatureType);
                    }
                    else if (tip.caracMonster == permaCarac)
                    {
                        bonus += tip.modroll.GetValueOfAction(actionType, human.eCreatureType);
                    }

                    if (bonus != 0)
                        break;
                }
            }

            if (bonus != 0)
                break;
        }
        //   CaractMonster mutation;


        return bonus;
    }

    public List<Tip> GetAllTipConcerningGroups(GroupMonsterFighter groupMonster, GroupHumanFighter groupHuman, bool inInitialGroup)
    {
        List<Tip> finalList = new List<Tip>();

        List<Fighter> allHuman = groupHuman.lFighters;
        List<Fighter> allMonster = groupMonster.lFighters;

        if (inInitialGroup)
        {
            allHuman = groupHuman.lInitialFighters;
            allMonster = groupMonster.lInitialFighters;

        }


        foreach (Tip tip in tipsNotKnownByPlayer)
        {
            bool bConcernM = false;
            bool bConcernH = false;

            foreach(Fighter fighter in allHuman)
            {
                ModelHumainUI modelH = ((Human)fighter).currentUI.gameObject.GetComponentInChildren<ModelHumainUI>();      
                CaractHumainCheveux cheveux = modelH.caractCheveux;
                CaractHumainStuff stuff = modelH.caractStuff;
                if (tip.caracHumain == cheveux)
                    bConcernH = true;
                else if (tip.caracHumain == stuff)
                    bConcernH = true;
            }
            foreach (Fighter fighter in allMonster)
            {
                ModelMonsterUI modelM = ((Monster)fighter).currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>();
                CaractMonster firstMutation = modelM.caractMonster;
                CaractMonster permaMutation = modelM.permanentCarMutation;

                if (tip.caracMonster == firstMutation)
                    bConcernM = true;
                else if (tip.caracMonster == permaMutation)
                    bConcernM = true;
            }

            if (bConcernH && bConcernM)
                finalList.Add(tip);
        }


        return finalList;
    }

    public Tip GetRandTipConcerningGroups(GroupMonsterFighter groupMonster, GroupHumanFighter groupHuman)
    {
        List<Tip> lFinalTips = GetAllTipConcerningGroups(groupMonster, groupHuman, true);

        if (lFinalTips.Count == 0)
            return null;
        int randIndex = Random.Range(0, lFinalTips.Count);
        Tip tip = lFinalTips[randIndex];

        LearnTip(tip);

        return tip;
    }

    public List<Tip> GetTipsAboutMonsterCarac(CaractMonster caracMonster)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in listTips)
        {
            if (tip.caracMonster == caracMonster)
                finalList.Add(tip);
        }
        return finalList;
    }
    public List<Tip> GetTipsAboutMonsterCarac(CaractMonster caracMonster, List<Tip> aList)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in aList)
        {
            if (tip.caracMonster == caracMonster)
                finalList.Add(tip);
        }
        return finalList;
    }
    public List<Tip> GetTipsOfAction(ActionType actionType, List<Tip> aList,CreatureType creatureType)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in aList)
        {
            if (tip.modAction(actionType, creatureType))
                finalList.Add(tip);
        }
        return finalList;
    }
    public List<Tip> GetTipsOfAction(ActionType actionType, CreatureType creatureType)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in listTips)
        {
            if (tip.modAction(actionType, creatureType))
                finalList.Add(tip);
        }
        return finalList;
    }
    public List<Tip> GetTipsAboutHumainCarac(CaractHumain caracHumain)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in listTips)
        {
            if (tip.caracHumain == caracHumain)
                finalList.Add(tip);
        }
        return finalList;
    }
    public List<Tip> GetTipsAboutHumainCarac(CaractHumain caracHumain, List<Tip> aList)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in aList)
        {
            if (tip.caracHumain == caracHumain)
                finalList.Add(tip);
        }
        return finalList;
    }
}
