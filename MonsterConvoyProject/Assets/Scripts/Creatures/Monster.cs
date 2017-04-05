using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Fighter{

    public int nFearPower;

     public Monster() : base() {
        this.eCreatureType = CreatureType.Monster;
    }

    public void CopyMonster(Monster monster)
    {
        this.CopyFighter(monster);
        this.nFearPower = monster.nFearPower;
        this.eCreatureType = CreatureType.Monster;
    }
    public override void PerformActionOnTarget(ActionType action , Fighter fighter)
    {
        base.PerformActionOnTarget(action, fighter);

        if (action == ActionType.FEAR)
        {
            fighter.TakeDamage(this.GetDamage());
        }

    }

    public override void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        base.PerformActionOnTarget(action, groupHuman);

        if (action == ActionType.FEAR)
        {
         //   ((GroupHumanFighter)groupHuman).GetFeared(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            float rand = 0.5f;

            if (combatManager.scriptManager != null)
            {
                rand = combatManager.scriptManager.currentTurn.fRoll;
            }

            if (rand > 0.99f)
            {
                ((GroupHumanFighter)groupHuman).GetCritFeared(this);
            }
            else if (rand > 0.3f)
            {
                ((GroupHumanFighter)groupHuman).GetFeared(this);
            }
            else
            {
                Debug.Log("Fail Fear:" + rand.ToString());
            }
        }

        if (action == ActionType.TALK)
        {
           //((GroupHumanFighter)groupHuman).GetConvinced(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            float rand = 0.5f;

            if (combatManager.scriptManager != null)
            {
                rand = combatManager.scriptManager.currentTurn.fRoll;
            }

            if (rand > 0.95)
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(this);
            }
            else if (rand > 0.3)
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(this);
            }
            else
            {
                Debug.Log("Fail Convice:" + rand.ToString());
            }
        }

    }
}
