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
        if (bMouseClicking && !bClickProcessed)
        {
            bClickProcessed = true;
            //Debug.Log("[MouseOverAction] bMouseClicking");
            combatManager.PlayerClickedCreature(fighterUI.fighter);
            gameObject.GetComponentInChildren<Renderer>().material.color = mouseClickedColor;
        }
        else if (bMouseOver)
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = mouseOverColor;
        }
        else
        {
            gameObject.GetComponentInChildren<Renderer>().material.color = baseColor;
        }
    }
}
