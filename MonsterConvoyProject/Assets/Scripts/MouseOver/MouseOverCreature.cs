using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FighterUI))]
public class MouseOverCreature : MouseOver
{

    private CombatManager combatManager;

    protected override void Start()
    {
        base.Start();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    protected override void ProcessStates()
    {
        if (bMouseClicking)
        {
            //Debug.Log("[MouseOverAction] bMouseClicking");
            combatManager.PlayerClickedCreature(GetComponent<FighterUI>().fighter);
            gameObject.GetComponent<Renderer>().material.color = mouseClickedColor;
        }
        else if (bMouseOver)
        {
            gameObject.GetComponent<Renderer>().material.color = mouseOverColor;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = baseColor;
        }
    }
}
