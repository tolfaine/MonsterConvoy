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

    private bool bIsFirstLogicTurn = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public ActionType SelectAction(List<Fighter> Enemies , List<Fighter> Allies)
    {

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        if (g != null)
        {
            ProtoScript protoScript = g.GetComponent<ProtoScript>();
            return ActionType.TALK;
        }

        if (bIsFirstLogicTurn)
        {
            bIsFirstLogicTurn = false;

            float rand = Random.Range(0.0f, 1.0f);

            if (rand < 0.2)
            {
                groupHumanFighter.bIsFeared = true;
                return ActionType.ESCAPE;
            }
            else if (rand < 0.7)
                return ActionType.ATTACK;
            else
                return ActionType.TALK;
        }

        if(!groupHumanFighter.bCanBeFeared || !groupHumanFighter.bCanListen)
            return ActionType.ATTACK;
        if(groupHumanFighter.bWantsToAttack)
            return ActionType.ATTACK;
        if (groupHumanFighter.bInConversation)
            return ActionType.TALK;


        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.6)
            return ActionType.ATTACK;
        else
            return ActionType.TALK;
    }
    
    public Fighter SelectTarget(List<Fighter> Enemies, List<Fighter> Allies)
    {
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
