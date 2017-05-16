using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TipsManager : MonoBehaviour {

    public List<Tip> listTips = new List<Tip>();

    public List<Tip> tipsNotKnownByPlayer = new List<Tip>();
    public List<Tip> tipsKnownByPlayer = new List<Tip>();


    void Awake()
    {
       Initialise();
    }
    // Use this for initialization

    void Start () {
        LearnTip(tipsNotKnownByPlayer[0]);
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
        if (tipsNotKnownByPlayer.Contains(tip))
        {
            tipsKnownByPlayer.Add(tip);
            tipsNotKnownByPlayer.Remove(tip);
        }
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
    public List<Tip> GetTipsOfAction(ActionType actionType, List<Tip> aList)
    {
        List<Tip> finalList = new List<Tip>();

        foreach (Tip tip in listTips)
        {
            if (tip.modroll.actionType == actionType)
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
