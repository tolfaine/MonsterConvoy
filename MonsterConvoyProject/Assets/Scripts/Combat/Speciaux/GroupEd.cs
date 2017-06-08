using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupEd : GroupHumanFighter
{
    public Human Ed;

    public GroupEd()
    {
        bIsSpecial = true;
        eCreatureType = CreatureType.Human;
    }

    public Human GetHuman()
    {
        return Ed;
    }

    public override void GetFeared(RollResultEnum rollResult, Monster monster)
    {

            int fearDamage = 0;

            if (rollResult == RollResultEnum.Crit)
            {
                fearDamage = 1000;
            }
            else if (rollResult == RollResultEnum.Normal)
            {
                fearDamage = 1;
            }

            AddFear(fearDamage);
        
    }

    public override void GetConvinced(RollResultEnum rollResult, Monster monster)
    {

            int convinceDamage = 0;

            if (rollResult == RollResultEnum.Crit)
            {
                convinceDamage = 1000;
            }
            else if (rollResult == RollResultEnum.Normal)
            {
                convinceDamage = 1;
            }

            AddConvice(convinceDamage);
       
    }

    public virtual void CheckFightersLife()
    {
        base.CheckFightersLife();

       ((IAEd)groupLogic).isDead = allFightersDead;

    }

}
