using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHumanFighter : GroupFighter {

    protected int nFear = 50;
    protected int nCurrentFear = 0;
    protected int nConvice = 50;
    protected int nCurrentConvice = 0;
    protected bool bCanBeFeared = true;
    protected bool bCanListen = true;

    protected bool isFeared;
    protected bool isConviced;

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
            isFeared = true;
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
            isConviced = true;
        }
    }

}
