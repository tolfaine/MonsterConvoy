using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModRoll
{

   // public ActionType actionType;// Supprimer ce truc

    public float modRollTalkH = 0;
    public float modRollFearH = 0;
    public float modRollAttackH = 0;

    public float modRollTalkM = 0;
    public float modRollFearM = 0;
    public float modRollAttackM = 0;

    public string sName;

    private static List<ModRoll> allBonus = new List<ModRoll>();

   // public static readonly ModRoll TALKH = new ModRoll(0.1f, 0, 0, 0, 0, 0, "++Talk H");
   // public static readonly ModRoll FEARH = new ModRoll(0, 0.1f, 0, 0, 0, 0, "++Talk H");
    public static readonly ModRoll ATTACKH = new ModRoll(0, 0, -0.1f, 0, 0, 0, "Humain miss ++");

    public static readonly ModRoll TALKM = new ModRoll(0, 0, 0, 0.1f, 0, 0, "Monster Talk ++");
    public static readonly ModRoll FEARM = new ModRoll(0, 0, 0, 0, 0.1f, 0, "Monster Fear ++");
    public static readonly ModRoll ATTACKM = new ModRoll(0, 0, 0, 0, 0, 0.1f, "Monster Attack ++");

    public static  ModRoll GetRandomMod()
    {
        int randIndex = Random.Range(0, allBonus.Count);
        return allBonus[randIndex];
    }

    private ModRoll(float talkH, float fearH, float attackH, float talkM, float fearM, float attackM, string name)
    {
        modRollTalkH = talkH;
        modRollFearH = fearH;
        modRollAttackH = attackH;

        modRollTalkM = talkM;
        modRollFearM = fearM;
        modRollAttackM = attackM;

        sName = name;

        allBonus.Add(this);
    }

    public float GetValueOfAction(ActionType actionType, CreatureType creatureType)
    {
        if(creatureType == CreatureType.Human)
        {
            if (actionType == ActionType.ATTACK)
            {
                return modRollAttackH;
            }
            if (actionType == ActionType.FEAR)
            {
                return modRollFearH;
            }
            if (actionType == ActionType.TALK)
            {
                return modRollTalkH;
            }
        }

        if (creatureType == CreatureType.Monster)
        {
            if (actionType == ActionType.ATTACK)
            {
                return modRollAttackM;
            }
            if (actionType == ActionType.FEAR)
            {
                return modRollFearM;
            }
            if (actionType == ActionType.TALK)
            {
                return modRollTalkM;
            }

        }
        return 0;
    }

    public bool modAction(ActionType actionType, CreatureType creatureType)
    {
        if (creatureType == CreatureType.Human)
        {
            if (actionType == ActionType.ATTACK)
            {
                if (modRollAttackH != 0)
                    return true;
            }
            if (actionType == ActionType.FEAR)
            {
                if (modRollFearH!=0)
                    return true;
            }
            if (actionType == ActionType.TALK)
            {
                if (modRollTalkH != 0)
                    return true;
            }
        }

        if (creatureType == CreatureType.Monster)
        {
            if (actionType == ActionType.ATTACK)
            {
                if (modRollAttackM != 0)
                    return true;
            }
            if (actionType == ActionType.FEAR)
            {
                if (modRollFearM != 0)
                    return true;
            }
            if (actionType == ActionType.TALK)
            {
                if (modRollTalkM != 0)
                    return true;
            }
     
        }
        return false;
    }

}

[System.Serializable]
public class Tip
{

    public CaractHumain caracHumain;
    public CaractMonster caracMonster;
    public ModRoll modroll;

    public bool modAction(ActionType actionType, CreatureType creatureType)
    {
        return modroll.modAction(actionType, creatureType);
    }
}