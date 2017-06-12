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
    public void  SetActive(bool active)
    {
        bIsActive = active;

        if (!bIsActive)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;
            
        }else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
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

        if (bMouseClicking && bMouseOver && bIsActive)
        {
            bMouseClicking = false;
            bMouseOver = false;
            combatManager.PlayerClickedAction(ActionType.GetActionTypeWithID((int)action));

            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;

        } else if (bIsActive && !remainHighlighted)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
        }
        else if (remainHighlighted && !combatManager.bTargetChoosed == false)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = clickSprite;
        }
        else if (bIsActive)
        {
           // GetComponentInChildren<SpriteRenderer>().sprite = normalSprite;
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
