using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverCreature : MouseOver
{

    private CombatManager combatManager;
    public FighterUI fighterUI;

    public Fighter fighter;
    public bool isInRecrutement = false;

    protected override void Start()
    {
        base.Start();
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

        fighter = fighterUI.fighter;

        GameObject g = GameObject.FindGameObjectWithTag("RecrutementManager");
        if(g != null)
        {
            isInRecrutement = true;
        }

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
            if (fighter == null)
                fighter = fighterUI.fighter;
            else
            {
                if(fighter.eCreatureType == CreatureType.Human)
                {

                    if(combatManager.currentFighter.eCreatureType == CreatureType.Monster)
                    {
                      if(combatManager.bActionChoosed && combatManager.actionChoosed == ActionType.ATTACK && !combatManager.bTargetChoosed)
                        {
                            Renderer[] renders = gameObject.transform.parent.gameObject.GetComponentsInChildren<Renderer>();

                            foreach(Renderer render in renders)
                            {
                                render.material.color = mouseOverColor;
                            }
                        }
                    }

                }else
                {
                    if (isInRecrutement)
                    {
                        Renderer[] renders = gameObject.transform.parent.gameObject.GetComponentsInChildren<Renderer>();

                        foreach (Renderer render in renders)
                        {
                            render.material.color = mouseOverColor;
                        }
                    }
                }


            }

            
        }
        else
        {

            Renderer[] renders = gameObject.transform.parent.gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer render in renders)
            {
                render.material.color = baseColor;
            }

        }
    }
}
