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
    bool remainHighlighted = false; //Hotfix
    bool hasBeenClicked = false;


    public void  SetActive(bool active)
    {
        bIsActive = active;

        if (!bIsActive)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;

        }
        else
        {
            if (!remainHighlighted || !hasBeenClicked)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
                GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        if (action == ActionType.ActionEnum.Attack)
        { remainHighlighted = true; }
    }

    protected override void ProcessStates()
    {
        //  base.ProcessStates();
        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g == null)
        {
            hasBeenClicked = false;
        }
            if (bMouseClicking && bMouseOver && bIsActive)
        {
            bMouseClicking = false;
            bMouseOver = false;
            combatManager.PlayerClickedAction(ActionType.GetActionTypeWithID((int)action));
            hasBeenClicked = true;
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;

        } else if (bIsActive && !remainHighlighted)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
        }
        else if (remainHighlighted && combatManager.bTargetChoosed)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;
        }
    }

    protected override void OnMouseOver()
    {
            bMouseOver = true;

    }

    protected override void OnMouseExit()
    {

            bMouseOver = false;

    }



}
