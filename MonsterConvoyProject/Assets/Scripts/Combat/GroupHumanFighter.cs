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
        this.groupLogic = new GroupIA();
        ((GroupIA)this.groupLogic).groupHumanFighter = this ;
    }

    public void GetFeared(RollResultEnum rollResult ,Monster monster)
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
            }


            AddFear(fearDamage);
        }
    }

    public void GetConvinced(RollResultEnum rollResult, Monster monster)
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
                bCanBeFeared = false;
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

    private void AddFear(int fear)
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

    private void AddConvice(int convice)
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
}
