using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMonsterFighter : GroupFighter {

    public GroupMonsterFighter() : base() {
        this.groupLogic = new PlayerLogic();
    }

    public override void SetInitialFighters()
    {
        foreach (Fighter fighter in lFighters) {
            Monster m = new Monster();
            m.CopyMonster((Monster)fighter);
            lInitialFighters.Add(m);
        }

    }

    public void MonsterEscaping()
    {
        bool allGroupEscape = true;

        foreach (Fighter fighter in lFighters)
        {
            if (!fighter.IsDead() && fighter.bTryToescape == false && !((Monster)fighter).isBoss)
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
