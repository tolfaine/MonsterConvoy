using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHumanFighter : GroupFighter {

    public int nFear = 50;
    public int nCurrentFear = 0;
    public int nConvice = 50;
    public int nCurrentConvice = 0;
    protected bool bCanBeFeared = true;
    protected bool bCanListen = true;

    public bool bIsFeared;
    public bool bIsConviced;

    public GroupHumanFighter() : base() {
        this.groupLogic = new GroupIA();
        ((GroupIA)this.groupLogic).groupHumanFighter = this ;
    }

    public void GetFeared(Monster monster)
    {
        int fearDamage = 0;

        foreach (Fighter fighter in lFighters)
        {
            fearDamage += monster.nFearPower * 1;
        }

        nCurrentFear += fearDamage;

        if(nCurrentFear > nFear)
        {
            nCurrentFear = nFear;
        }

        if(nCurrentFear == nFear)
        {
            bIsFeared = true;
        }
    }
    public void GetConvinced(Monster monster)
    {
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
