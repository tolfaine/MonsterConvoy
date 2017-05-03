using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverAction : MouseOver
{
    public ActionType.ActionEnum action;
    private CombatManager combatManager;
    private bool bIsActive = true;

    public void  SetActive(bool active)
    {
        bIsActive = active;
    }

    protected override void Start()
    {
        base.Start();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    protected override void ProcessStates()
    {
        if (bMouseClicking && bMouseOver && bIsActive)
        {
            bMouseClicking = false;
            bMouseOver = false;
            //Debug.Log("[MouseOverAction] bMouseClicking");
            combatManager.PlayerClickedAction(ActionType.GetActionTypeWithID((int)action));
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
