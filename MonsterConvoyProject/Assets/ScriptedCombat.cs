using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCombat : MonoBehaviour {
    public CustomTalk customTalk;
    public CustomTalk monsters;

    public TextAsset ScriptText;
    public int iteration = 0;

    public int roundIt = 0;

    public List<ScriptTurnInfo> lTurnInfo = new List<ScriptTurnInfo>();

    public List<ScriptTurnInfo> lTurnInfo2 = new List<ScriptTurnInfo>();

    public ScriptTurnInfo currentTurn;
    public int index = 0;

    public int nbAttack = 0;

    public void NextTurn()
    {
        List<ScriptTurnInfo> turnList = new List<ScriptTurnInfo>();

        if (iteration == 1)
            turnList = lTurnInfo;
        if (iteration == 2)
            turnList = lTurnInfo2;

        if (index < turnList.Count)
        {
            currentTurn = turnList[index];
            index++;
        }
        else
        {
            currentTurn = null;
        }

    }

    // Use this for initialization
    void Start () {

        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().talkManager != null)
        {
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            monsters = sm.customTalk;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterCombat()
    {
        currentTurn = null;
        index = 0;
        roundIt = 0;

        if (iteration == 0)
            customTalk.NewTalkScripted(ScriptText, 2, 2);
        if (iteration == 1)
            customTalk.NewTalkScripted(ScriptText, 15, 15);
        else
        {

        }

        iteration++;

        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().talkManager != null)
        {
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            monsters = sm.customTalk;
        }
    }

    public void EndCombat()
    {
        if (iteration == 1)
            customTalk.NewTalkScripted(ScriptText, 12, 12);
        if (iteration == 2)
            customTalk.NewTalkScripted(ScriptText, 26, 26);
    }

    public void Talk()
    {



        if (iteration == 1)
        {

            if(roundIt == 0)
            {
                monsters.NewTalkScripted(ScriptText, 4, 4);
            }
            else if (roundIt == 1)
            {
                monsters.NewTalkScripted(ScriptText, 5, 5);
            }
            else if (roundIt == 2)
            {
                monsters.NewTalkScripted(ScriptText, 6, 6);
            }
            else if (roundIt == 3)
            {
                monsters.NewTalkScripted(ScriptText, 7, 7);
            }
            else if (roundIt == 4)
            {
                monsters.NewTalkScripted(ScriptText, 8, 8);
            }

            roundIt++;
        }
        if (iteration == 2)
        {
            if (roundIt == 0)
            {
                monsters.NewTalkScripted(ScriptText, 18, 18);
            }
            else if (roundIt == 1)
            {
                monsters.NewTalkScripted(ScriptText, 19, 19);
            }
            else if (roundIt == 2)
            {
                monsters.NewTalkScripted(ScriptText, 21, 21);
            }

            roundIt++;
        }
    }

    public void HumanAttack()
    {
        if (iteration == 2)
        {
            if (nbAttack == 0)
            {
                customTalk.NewTalkScripted(ScriptText, 17, 17);
            }
            else if (nbAttack == 2)
            {
                customTalk.NewTalkScripted(ScriptText, 23, 23);
            }
            else if (nbAttack == 3)
            {
                customTalk.NewTalkScripted(ScriptText, 24, 24);
            }

            nbAttack++;
        }
    }

    public void MonsterFailFear()
    {
        monsters.NewTalkScripted(ScriptText, 22,22);
    }
}
