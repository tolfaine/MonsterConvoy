using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCombat : MonoBehaviour {

    public enum CombatTalk { EnterCombat, EndCombat, Talk, HumanAttack, MonsterFailFear, HumainFailFear }


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

    public CombatManager combatManager;

    public bool needToTalk;
    public CombatTalk typeTalk;

    public bool secondNeedToTalk;
    public CombatTalk secondTypeTalk;

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
            combatManager = g.GetComponent<CombatManager>();
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            monsters = sm.customTalk;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (needToTalk)
        {
            switch (typeTalk)
            {
                case CombatTalk.EndCombat:
                    needToTalk = false;
                    if (iteration == 1)
                    {
                        customTalk.NewTalkScripted(ScriptText, 12, 12);
                        // AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
                    }
                    if (iteration == 2)
                    {
                        customTalk.NewTalkScripted(ScriptText, 26, 26);
                        // AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
                    }


                    break;
                case CombatTalk.EnterCombat:
                    //  AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
                    needToTalk = false;
                    break;
                case CombatTalk.HumanAttack:
                    if (combatManager.bFighterInInitialPosition)
                    {
                        needToTalk = false;
                        if (iteration == 2)
                        {
                            //   AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
                            if (nbAttack == 0)
                            {
                                customTalk.NewTalkScripted(ScriptText, 17, 17);
                            }
                            else if (nbAttack == 1)
                            {
                                customTalk.NewTalkScripted(ScriptText, 23, 23);
                            }
                            else if (nbAttack == 2)
                            {
                                customTalk.NewTalkScripted(ScriptText, 24, 24);
                            }

                            nbAttack++;
                        }
                    }

                    break;
                case CombatTalk.MonsterFailFear:
                    needToTalk = false;
                    customTalk.NewTalkScripted(ScriptText, 22, 22);
                    break;
                case CombatTalk.Talk:
                    needToTalk = false;
                    if (iteration == 1)
                    {

                        if (roundIt == 0)
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

                    break;

            }


        }

        if (secondNeedToTalk)
        {
            if (combatManager.bFighterInInitialPosition)
            {
                switch (secondTypeTalk)
                {
                    case CombatTalk.MonsterFailFear:
                        secondNeedToTalk = false;
                        customTalk.NewTalkScripted(ScriptText, 22, 22);
                        break;

                    case CombatTalk.HumanAttack:
                        needToTalk = false;
                        customTalk.NewTalkScripted(ScriptText, 20, 20);
                        break;
                }
            }

        }
    }


    public void EnterCombat()
    {
           needToTalk = true;
        typeTalk = CombatTalk.EnterCombat;
        
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
            combatManager = g.GetComponent<CombatManager>();
        }
        
    }

    public void EndCombat()
    {
        needToTalk = true;
        typeTalk = CombatTalk.EndCombat;

        /*
        if (iteration == 1)
            customTalk.NewTalkScripted(ScriptText, 12, 12);
        if (iteration == 2)
            customTalk.NewTalkScripted(ScriptText, 26, 26);
            */
    }

    public void Talk()
    {
        needToTalk = true;
        typeTalk = CombatTalk.Talk;

        /*
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
        */
    }

    public void HumanAttack()
    {
        needToTalk = true;
        typeTalk = CombatTalk.HumanAttack;
        /*
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
        */
    }

    public void MonsterFailFear()
    {
        secondNeedToTalk = true;
        secondTypeTalk = CombatTalk.MonsterFailFear;

        /*
        monsters.NewTalkScripted(ScriptText, 22,22);
        */
    }

    public void HumainFailAttack()
    {
     //   secondNeedToTalk = true;
   //     secondTypeTalk = CombatTalk.HumainFailFear;

        /*
        monsters.NewTalkScripted(ScriptText, 22,22);
        */
    }
}
