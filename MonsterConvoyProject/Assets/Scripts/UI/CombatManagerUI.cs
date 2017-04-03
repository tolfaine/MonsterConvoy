﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerUI : MonoBehaviour {

    public CombatManager combatManager;
    private TextMesh textMesh;

    public string sDisplayed;
    // Action , target, fighter, Type 

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {

        CheckCombatManager();
        textMesh.text = sDisplayed;
    }

    void CheckCombatManager()
    {
        sDisplayed = "";

        if (!combatManager.bCombatEnded)
        {
            if (combatManager.currentGroupLogic != null)
            {
                if (combatManager.currentGroupLogic.GetLogicType() == LogicType.IA)
                    sDisplayed += "[IA TURN]\n";
                else
                    sDisplayed += "[PLAYER TURN]\n";
            }

            if (combatManager.currentFighter != null)
                sDisplayed += "fighter : " + combatManager.currentFighter.sName + "\n";

            if (combatManager.actionChoosed != null)
                sDisplayed += "action : " + combatManager.actionChoosed.sName + "\n";
            if (combatManager.actionChoosed == null)
                sDisplayed += "action :\n";


            if (combatManager.actionChoosed != null && combatManager.actionChoosed.GetTargetType() == ActionType.ActionTargetType.AllTarget)
                sDisplayed += "on all enemies \n";
            if (combatManager.actionChoosed != null && combatManager.actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
            {
                if(combatManager.targetChoosed != null)
                    sDisplayed += "target : " + combatManager.targetChoosed.sName + "\n";
                else
                    sDisplayed += "target :\n";
            }

        }else
        {
            sDisplayed += "COMBAT END :";

            if (combatManager.bMonsterWin)
                sDisplayed += "Monster Win :)";
            else
                sDisplayed += "Human Win :(";

        }
    }
}
