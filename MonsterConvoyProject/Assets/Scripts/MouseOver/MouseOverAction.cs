using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverAction : MouseOver
{
    public Sprite clickSprite;
    public Sprite normalSprite;

    public ActionType.ActionEnum action;
    private CombatManager combatManager;

    public  bool bIsActive = true;

    public void  SetActive(bool active)
    {
        bIsActive = active;

        if (!bIsActive)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;
            GetComponentInChildren<SpriteRenderer>().color = Color.black;
        }else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }

    protected override void Start()
    {
        base.Start();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    protected override void ProcessStates()
    {
      //  base.ProcessStates();

        if (bMouseClicking && bMouseOver && bIsActive)
        {
            bMouseClicking = false;
            bMouseOver = false;
            combatManager.PlayerClickedAction(ActionType.GetActionTypeWithID((int)action));
        }

    }
}
