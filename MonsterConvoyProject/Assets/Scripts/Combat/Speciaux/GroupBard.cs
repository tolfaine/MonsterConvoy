using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBard : GroupHumanFighter
{
    public Human Bard;

    public GroupBard()
    {
        bIsSpecial = true;
        eCreatureType = CreatureType.Human;
    }

    public Human GetHuman()
    {
        return Bard;
    }

    public override void GetFeared(RollResultEnum rollResult, Monster monster)
    {


    }

    public override void GetConvinced(RollResultEnum rollResult, Monster monster)
    {

    }

}
