using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Human : Fighter{

    public int nFearTolerance;

    public Human() : base() {
        this.eCreatureType = CreatureType.Human;
    }

    public void CopyHuman(Human human)
    {
        this.CopyFighter(human);
        this.nFearTolerance = human.nFearTolerance;
        this.eCreatureType = CreatureType.Human;
    }

    public override void PerformActionOnTarget(ActionType action, Fighter fighter)
    {
        base.PerformActionOnTarget(action, fighter);

    }
    public override void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        base.PerformActionOnTarget(action, groupHuman);

        if (action == ActionType.TALK)
        {
            //((GroupHumanFighter)groupHuman).GetConvinced(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            float rand = 0.5f;

            if (rand > 0.95)
            {

            }
            else if (rand > 0.3)
            {

            }
            else
            {
                Debug.Log("Fail Convice:" + rand.ToString());
            }

            ActionTalk(action, rand);
        }
    }

    public override void ActionTalk(ActionType action, float roll)
    {
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().talkManager != null)
        {
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            sm.customTalk.follow = currentUI.dialogueAnchor.gameObject;
            sm.customTalk.NewTalk(CreatureType.Human, roll);
        }
    }


}
