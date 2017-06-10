using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHumanFighter : GroupFighter {

    public int nFear = 6;
    public int nConvice = 6;

    public int nCurrentFear = 0;
    public int nCurrentConvice = 0;

    public  bool bCanBeFeared = true;
    public bool bCanListen = true;

    public bool bHasBeenAttackedOnce = false;
    public bool bHasTookDamageOnce = false;
    public bool bHasAttackOnce = false;

    public bool bInConversation = false;
    public bool bWantsToAttack = false;


    // Modificateur discute
    // Modificateur peur
    // Modificateur preci

    public bool bIsFeared;
    public bool bIsConviced;

    public override void SetInitialFighters()
    {
        foreach (Fighter fighter in lFighters)
        {
            Human m = new Human();
            m.CopyHuman((Human)fighter);
            lInitialFighters.Add(m);
        }

    }


    public GroupHumanFighter() : base() {
        bIsSpecial = false;
        this.groupLogic = new GroupIA();
        ((GroupIA)this.groupLogic).groupHumanFighter = this ;
    }

    public override void GetFeared(RollResultEnum rollResult ,Monster monster)
    {
        if (bCanBeFeared)
        {
            int fearDamage = 0;

            if (rollResult == RollResultEnum.Crit)
            {
                fearDamage = 1000;
                bInConversation = false;
                bWantsToAttack = true;
            }
            else if (rollResult == RollResultEnum.Normal)
            {
                fearDamage = 1;
                bInConversation = false;
                bWantsToAttack = true;
            }

            else
            {
                bCanBeFeared = false;
                bInConversation = false;
                bWantsToAttack = true;
            }

            AddFear(fearDamage);
        }
    }

    public override void GetConvinced(RollResultEnum rollResult, Monster monster)
    {
        if (bCanListen)
        {

            int convinceDamage = 0;

            if (rollResult == RollResultEnum.Crit)
            {
                convinceDamage = 1000;
                bInConversation = true;
                bWantsToAttack = false;
            }
            else if (rollResult == RollResultEnum.Normal)
            {
                convinceDamage = 1;
                bInConversation = true;
                bWantsToAttack = false;
            }
            else
            {
                bCanListen = false;
                bWantsToAttack = true;
                bInConversation = false;
            }


            AddConvice(convinceDamage);

        }
    }

    public override void OneFighterTookDamage()
    {
        bHasTookDamageOnce = true;
        bHasBeenAttackedOnce = true;

        base.OneFighterTookDamage();

        if (bInConversation)
            bInConversation = false;

        bWantsToAttack = true;

    }

    public override void OneFighterGotTargetted()
    {
        bHasBeenAttackedOnce = true;

        base.OneFighterGotTargetted();

      //  if (bInConversation)
         //   bInConversation = false;

    }

    protected void AddFear(int fear)
    {
        nCurrentFear += fear;

        if (nCurrentFear > nFear)
        {
            nCurrentFear = nFear;
        }

        if (nCurrentFear == nFear)
        {
            bIsFeared = true;
        }
    }

    protected void AddConvice(int convice)
    {
        nCurrentConvice += convice;

        if (nCurrentConvice > nConvice)
        {
            nCurrentConvice = nConvice;
        }

        if (nCurrentConvice == nConvice)
        {
            bIsConviced = true;
        }
    }

    public override void CheckFightersLife()
    {
        int nbFightBefore = nbFightersAlive;
        base.CheckFightersLife();

        if(nbFightBefore > nbFightersAlive)
        {
            if (!bCanBeFeared)
                bCanBeFeared = true;
        }

    }
}
