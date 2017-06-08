using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABard : GroupIA
{
    private int currentLine =4;
    public bool theyLeftOnce = false;
    public bool combatJustStarted = false;
    public bool combatJustEnded = false;

    public bool endStory = false;
    // Text Bard 

    public GroupBard groupBard;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override ActionType SelectAction(List<Fighter> Enemies, List<Fighter> Allies)
    {
        if(!endStory)
            return ActionType.TALK;
        groupHumanFighter.bIsFeared = true;
        return ActionType.ESCAPE;

    }

    public void Talk()
    {
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>())
        {
            bool remind = false;

            if (theyLeftOnce && combatJustStarted)
            {
                remind = true;
                combatJustStarted = false;
            }


            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            sm.customTalk.NewTalkBard  (currentLine, remind, combatJustEnded);
            currentLine++;

            if (currentLine == 25)
                endStory = true;
        }
    }

    public void EndCombat()
    {
        combatJustEnded = true;
        theyLeftOnce = true;
        Talk();
        combatJustEnded = false;
    }



}
