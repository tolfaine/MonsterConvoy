using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlip : GroupIA
{
    public bool bWantsToAtaack = true;

    public GroupSlip groupSlip;

    public bool isDead;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override ActionType SelectAction(List<Fighter> Enemies, List<Fighter> Allies)
    {

        return ActionType.ATTACK;

    }

}
