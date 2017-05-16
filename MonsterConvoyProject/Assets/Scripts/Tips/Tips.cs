using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModRoll
{
    public ActionType actionType;
    public int modRollTalk = 0;
    public int modRollFear = 0;
    public int modRollAttack = 0;

    private static List<ModRoll> allBonus = new List<ModRoll>();

    public static readonly ModRoll TALK = new ModRoll(1, 0, 0, ActionType.TALK);
    public static readonly ModRoll FEAR = new ModRoll(0, 1, 0, ActionType.FEAR);
    public static readonly ModRoll ATTACK = new ModRoll(0, 0, 1, ActionType.TALK);

    public static  ModRoll GetRandomMod()
    {
        int randIndex = Random.Range(0, allBonus.Count);
        return allBonus[randIndex];
    }

    private ModRoll(int talk, int fear, int attack, ActionType actionType)
    {
        modRollTalk = talk;
        modRollFear = fear;
        modRollAttack = attack;
        this.actionType = actionType;

        allBonus.Add(this);
    }
}

[System.Serializable]
public class Tip
{

    public CaractHumain caracHumain;
    public CaractMonster caracMonster;
    public ModRoll modroll;
}