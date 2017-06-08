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
            lastActionResult = RollResultEnum.Normal;

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

            GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
            CombatManager cm = g.GetComponent<CombatManager>();
            ((GroupHumanFighter)cm.GetGroupFighterOfFighter(this)).bInConversation = true; ;

            
            if(combatManager.humanGroupFighter.bIsSpecial && combatManager.specialType == SpecialType.Bard)
            {
                ((IABard)combatManager.humanGroupFighter.groupLogic).Talk();

            }
            else if (combatManager.humanGroupFighter.bIsSpecial && combatManager.specialType == SpecialType.Ed)
            {
                ((IAEd)combatManager.humanGroupFighter.groupLogic).Talk();

            }else
            {
                ActionTalk(action, rand);
            }


        }
    }


}
