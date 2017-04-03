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
    }
}
