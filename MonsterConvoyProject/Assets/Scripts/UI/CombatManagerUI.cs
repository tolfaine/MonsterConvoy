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

    public GameObject actionWheel;

    public GameObject humanAnchor;
  
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

        if(combatManager.fighterMouvementManager.bIsAtFightPosition && !combatManager.bActionInProgress && combatManager.currentGroupLogic.GetLogicType() == LogicType.Player)
        {
            actionWheel.SetActive(true);
        }else
        {
            foreach (Transform child in actionWheel.transform)
            {
                MouseOverAction mouse = child.GetComponent<MouseOverAction>();
                if (mouse != null)
                {
                    mouse.bMouseOver = false;
                    mouse.bMouseClicking = false;
                }
            }

            actionWheel.SetActive(false);
        }
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
            rpgTalk.follow = humanAnchor;
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
                sDisplayed += "Monsters Won , \n the humans are \n convinced :)";
            else if(combatManager.combatEndType == CombatManager.CombatEndType.HumansDead)
                sDisplayed += "Monsters Won ,\n  the humans are \n dead :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.HumansFeared)
                sDisplayed += "Monsters Won ,\n the humans are \n feared :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.HumansFeared)
                sDisplayed += "Humans Won , \nthe monsters are \n dead :(";

        }
    }
}
