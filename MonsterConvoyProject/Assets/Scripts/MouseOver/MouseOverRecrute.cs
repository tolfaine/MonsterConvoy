using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverRecrute : MouseOver
{

    private RecrutementManager recrutementManager;
   // public Monster monster;
    public FighterUI fighterUI;
    public GameObject fightherObj;

    protected override void Start()
    {
        base.Start();
       // monster = new Monster(fighterUI.fighter);

        recrutementManager = GameObject.FindGameObjectWithTag("RecrutementManager").GetComponent<RecrutementManager>();
    }

    protected override void ProcessStates()
    {
        if (bMouseClicking && !bClickProcessed)
        {
            bClickProcessed = true;
            recrutementManager.MonsterSelected(fightherObj);
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
