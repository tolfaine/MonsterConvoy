using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerUI : MonoBehaviour {

    public CombatManager combatManager;
    private TextMesh textMesh;

    public string sDisplayed;

    public GameObject dialogueHumanObj;
    public GameObject dialogueObj;

    public RPGTalk rpgTalk;

    public bool bHumanFearTriggred = false;
  
    // Action , target, fighter, Type 


	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckHumanFear();
        CheckCombatManager();
        textMesh.text = sDisplayed;
    }

    public bool DialogueInProgress()
    {
        if (dialogueHumanObj.activeSelf || dialogueObj.activeSelf)
            return true;
        return false;
    }

    void CheckHumanFear()
    {
        if(combatManager.combatEndType== CombatManager.CombatEndType.HumansFeared && !bHumanFearTriggred
            && !combatManager.fighterMouvementManager.bIsAtFightPosition)
        {
            bHumanFearTriggred = true;
            rpgTalk.lineToStart = 9;
            rpgTalk.lineToBreak = 9;
            rpgTalk.NewTalk();
        }
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
            if (combatManager.combatEndType == CombatManager.CombatEndType.HumansConvinced)
                sDisplayed += "Monsters Win , the humans are convinced :)";
            else if(combatManager.combatEndType == CombatManager.CombatEndType.HumansDead)
                sDisplayed += "Monsters Win , the humans are dead :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.HumansFeared)
                sDisplayed += "Monsters Win , the humans are feared :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.HumansFeared)
                sDisplayed += "Humans Win , the monsters are dead :(";

        }
    }
}
