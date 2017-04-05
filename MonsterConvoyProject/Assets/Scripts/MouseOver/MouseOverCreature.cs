using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverCreature : MouseOver
{

    private CombatManager combatManager;
    public FighterUI fighterUI;

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
            combatManager.PlayerClickedCreature(fighterUI.fighter);
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
