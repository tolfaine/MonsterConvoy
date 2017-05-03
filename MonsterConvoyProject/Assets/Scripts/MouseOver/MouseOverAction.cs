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
        base.ProcessStates();

        if (bMouseClicking && bMouseOver && bIsActive)
        {
            bMouseClicking = false;
            bMouseOver = false;
            combatManager.PlayerClickedAction(ActionType.GetActionTypeWithID((int)action));
        }

    }
}
