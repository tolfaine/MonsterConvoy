using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMonsterFighter : GroupFighter {

    public GroupMonsterFighter() : base() {
        this.groupLogic = new PlayerLogic();
    }

    public void MonsterEscaping()
    {
        bool allGroupEscape = true;

        foreach (Fighter fighter in lFighters)
        {
            if (!fighter.IsDead() && fighter.bTryToescape == false)
                allGroupEscape = false;
        }

        if (allGroupEscape)
        {
            bEscaping = true;
        }

    }

    public override void CheckFightersLife()
    {
        base.CheckFightersLife();

        GameObject g = GameObject.FindGameObjectWithTag("Caravane");
        Caravane caravane = g.GetComponent<Caravane>();
        caravane.CheckMonsterDead();
    }
}
