using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModRoll
{
    public ActionType actionType;
    public float modRollTalk = 0;
    public float modRollFear = 0;
    public float modRollAttack = 0;

    private static List<ModRoll> allBonus = new List<ModRoll>();

    public static readonly ModRoll TALK = new ModRoll(0.1f, 0, 0, ActionType.TALK);
    public static readonly ModRoll FEAR = new ModRoll(0, 0.1f, 0, ActionType.FEAR);
    public static readonly ModRoll ATTACK = new ModRoll(0, 0, 0.1f, ActionType.TALK);

    public static  ModRoll GetRandomMod()
    {
        int randIndex = Random.Range(0, allBonus.Count);
        return allBonus[randIndex];
    }

    private ModRoll(float talk, float fear, float attack, ActionType actionType)
    {
        modRollTalk = talk;
        modRollFear = fear;
        modRollAttack = attack;
        this.actionType = actionType;

        allBonus.Add(this);
    }

    public float GetValueOfAction(ActionType actionType)
    {
        if(actionType == ActionType.ATTACK)
        {
            return modRollAttack;
        }
        if (actionType == ActionType.FEAR)
        {
            return modRollFear;
        }
        if (actionType == ActionType.TALK)
        {
            return modRollTalk;
        }
        return 0;
    }
}

[System.Serializable]
public class Tip
{

    public CaractHumain caracHumain;
    public CaractMonster caracMonster;
    public ModRoll modroll;
}