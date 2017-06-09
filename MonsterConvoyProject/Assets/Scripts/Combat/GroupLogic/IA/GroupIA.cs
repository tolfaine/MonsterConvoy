using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupIA : GroupLogic {

    protected ActionType    ePlannedAction;
    protected bool          bActionIsLocked;
    protected Fighter       target;
    protected bool          bTargetLocked;
    public  GroupHumanFighter groupHumanFighter;

    public ActionType currentAction;

    public bool bIsFirstLogicTurn = true;

    public RollProbaManager rollProbaManager;

    // Use this for initialization
    void Start () {
        GameObject rollObj = GameObject.FindGameObjectWithTag("RollProbaManager");
        if (rollObj != null)
        {
            rollProbaManager = rollObj.GetComponent<RollProbaManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public virtual ActionType SelectAction(List<Fighter> Enemies , List<Fighter> Allies)
    {

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g != null)
        {
            ProtoScript protoScript = g.GetComponent<ProtoScript>();
            if(protoScript.combat.iteration == 1)
                return ActionType.TALK;
            else
                return ActionType.ATTACK;
        }

        if (groupHumanFighter.bWantsToAttack)
            return ActionType.ATTACK;
        if (groupHumanFighter.bInConversation)
            return ActionType.TALK;

        if (!groupHumanFighter.bCanBeFeared || !groupHumanFighter.bCanListen)
        {
            groupHumanFighter.bWantsToAttack = true;
            return ActionType.ATTACK;
        }

        if (bIsFirstLogicTurn)
        {
            bIsFirstLogicTurn = false;
            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            if (combatManager.isBossCombat)
            {
                return ActionType.ATTACK;

            }
            float rand = Random.Range(0.0f, 1.0f);

            ActionType.ActionEnum enumAction = GameObject.FindGameObjectWithTag("CombatTerrain").GetComponent<CombatTerrainInfo>().modComportement.action;
            ActionType acType = ActionType.GetActionTypeWithID((int)enumAction);

            if (rand < rollProbaManager.humanComp.escape || (acType == ActionType.ESCAPE && (rand - 0.1) < 0.2))
            {
                groupHumanFighter.bIsFeared = true;
                return ActionType.ESCAPE;
            }
            if (rand > rollProbaManager.humanComp.discussion || (acType == ActionType.TALK && (rand + 0.1) > 0.7))
            {
                groupHumanFighter.bInConversation = true;
                groupHumanFighter.bWantsToAttack = false;
                return ActionType.TALK;
            }

            else if (rand < rollProbaManager.humanComp.discussion || (acType == ActionType.ATTACK && (rand - 0.1) < 0.7))
            {
                groupHumanFighter.bWantsToAttack = true;
                groupHumanFighter.bInConversation = false;
                return ActionType.ATTACK;
            }
            else
            {
                groupHumanFighter.bInConversation = true;
                groupHumanFighter.bWantsToAttack = false;
                return ActionType.TALK;
            }
        }


        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.6)
        {
            groupHumanFighter.bWantsToAttack = true;
            groupHumanFighter.bInConversation = false;
            return ActionType.ATTACK;
        }

        else
        {
            groupHumanFighter.bInConversation = true;
            groupHumanFighter.bWantsToAttack = false;
            return ActionType.TALK;
        }
    }
    
    public virtual Fighter SelectTarget(List<Fighter> Enemies, List<Fighter> Allies)
    {

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g != null)
        {
            ProtoScript protoScript = g.GetComponent<ProtoScript>();

            if(protoScript.combat.iteration == 2)
            {
                if(Enemies.Count >= 3)
                {
                    if (Enemies[2].CanAttack())
                        return Enemies[2];
                }

            }

        }

        List<Fighter> possibleTarget = new List<Fighter>();

        foreach(Fighter fighter in Enemies)
        {
            if (fighter.CanBeAttack())
                possibleTarget.Add(fighter);
        }

        int random = (int)Random.Range(0.0f, possibleTarget.Count);
        return possibleTarget[random];

    }

    public override LogicType GetLogicType() { return LogicType.IA; }
}
