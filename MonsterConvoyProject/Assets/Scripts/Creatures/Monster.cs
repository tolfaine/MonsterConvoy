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
            ((GroupHumanFighter)groupHuman).GetFeared(this);
        }

        if (action == ActionType.TALK)
        {
            ((GroupHumanFighter)groupHuman).GetConvinced(this);
        }

    }
}
