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
            if (fighter.bTryToescape == false)
                allGroupEscape = false;
        }

        if (allGroupEscape)
        {
            bEscaping = true;
        }

    }

}
