using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWheel : MonoBehaviour {

    public MouseOverAction talk;
    public MouseOverAction fear;
    public MouseOverAction attack;
    public MouseOverAction escape;

    public Fighter currentFighter = null;

    public ProtoScript protoScript;
    public CombatManager combatManager;
    public ScriptedCombat scriptedCombat;
    

    // Use this for initialization
    void Start () {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g != null)
        {
            protoScript = g.GetComponent<ProtoScript>();
            scriptedCombat = protoScript.combat;

        }
    }

    private void FixedUpdate()
    {


    }

	// Update is called once per frame
	void Update () {

        if (combatManager.currentFighter.eCreatureType == CreatureType.Monster)
        {

            if (scriptedCombat != null)
            {
                ScriptTurnInfo turnInfo = scriptedCombat.currentTurn;
                if(turnInfo != null)
                {
                    OnlyAction(ActionType.GetActionTypeWithID(turnInfo.ActionCode));
                }
                
            }
            else
            {
                if (!combatManager.currentFighter.bHasTookDamage)
                    BlockOneAction(ActionType.ATTACK);
            }
        }
		
	}

    public void AllAction()
    {
        talk.SetActive(true);
        fear.SetActive(true);
        attack.SetActive(true);
        escape.SetActive(true);
    }

    public void BlockOneAction(ActionType action)
    {
        talk.SetActive(true);
        fear.SetActive(true);
        attack.SetActive(true);
        escape.SetActive(true);

        if (action == ActionType.ATTACK)
            attack.SetActive(false);
        if (action == ActionType.ESCAPE)
            escape.SetActive(false);
        if (action == ActionType.FEAR)
            fear.SetActive(false);
        if (action == ActionType.TALK)
            escape.SetActive(false);
    }

    public void OnlyAction(ActionType action)
    {
        talk.SetActive(false);
        fear.SetActive(false);
        attack.SetActive(false);
        escape.SetActive(false);

        if (action == ActionType.ATTACK)
            attack.SetActive(true);
        if (action == ActionType.ESCAPE)
            escape.SetActive(true);
        if (action == ActionType.FEAR)
            fear.SetActive(true);
        if (action == ActionType.TALK)
            talk.SetActive(true);
    }

    public void SetFighter(Fighter fighter)
    {
        currentFighter = fighter;

        if (currentFighter.eCreatureType == CreatureType.Monster)
        {
            if (currentFighter.bHasbeenAttcked)
                attack.SetActive(true);
            else
                attack.SetActive(false);
        }

    }

    public void SetAction(ActionType type, bool active)
    {
        if (type == ActionType.FEAR)
            fear.SetActive(active);
        if (type == ActionType.TALK)
            talk.SetActive(active);
    }

}
