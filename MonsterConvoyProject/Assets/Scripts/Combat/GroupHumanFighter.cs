using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHumanFighter : GroupFighter {

    public int nFear = 50;
    public int nCurrentFear = 0;
    public int nConvice = 50;
    public int nCurrentConvice = 0;
    public  bool bCanBeFeared = true;
    public bool bCanListen = true;

    // Modificateur discute
    // Modificateur peur
    // Modificateur preci

    public bool bIsFeared;
    public bool bIsConviced;

    public bool bInConversation = false;

    public GroupHumanFighter() : base() {
        this.groupLogic = new GroupIA();
        ((GroupIA)this.groupLogic).groupHumanFighter = this ;
    }

    public void GetFeared(Monster monster)
    {
        if (bCanBeFeared)
        {
            int fearDamage = 0;

            bInConversation = false;

            foreach (Fighter fighter in lFighters)
            {
                fearDamage += monster.nFearPower * 1;
            }

            nCurrentFear += fearDamage;

            if (nCurrentFear > nFear)
            {
                nCurrentFear = nFear;
            }

            if (nCurrentFear == nFear)
            {
                bIsFeared = true;
            }
        }
    }

    public void GetCritFeared(Monster monster)
    {
        if (bCanBeFeared)
        {
            int fearDamage = 0;

            bInConversation = false;

            foreach (Fighter fighter in lFighters)
            {
                fearDamage += monster.nFearPower * 20;
            }

            nCurrentFear += fearDamage;

            if (nCurrentFear > nFear)
            {
                nCurrentFear = nFear;
            }

            if (nCurrentFear == nFear)
            {
                bIsFeared = true;
            }
        }
    }

    public void GetConvinced(Monster monster)
    {
        if (bCanListen)
        {
            bInConversation = true;

            int convinceDamage = 0;

            foreach (Fighter fighter in lFighters)
            {
                convinceDamage += monster.nFearPower * 1;
            }

            nCurrentConvice += convinceDamage;

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

    public void GetCritConvinced(Monster monster)
    {
        if (bCanListen)
        {
            bInConversation = true;

            int convinceDamage = 0;

            foreach (Fighter fighter in lFighters)
            {
                convinceDamage += monster.nFearPower * 20;
            }

            nCurrentConvice += convinceDamage;

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

    public override void OneFighterTookDamage()
    {
        base.OneFighterTookDamage();

        if (bInConversation)
            bInConversation = false;

    }

    public override void OneFighterGotTargetted()
    {
        base.OneFighterGotTargetted();

        if (bInConversation)
            bInConversation = false;

    }

}
