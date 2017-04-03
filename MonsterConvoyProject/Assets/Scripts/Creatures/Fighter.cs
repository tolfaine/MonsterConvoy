using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fighter : Creature{

    public int nHealthMax;
    public int nCurrentHealth;
    public int nPower;
    public int nInitiative;
    public int nArmor;
    public int nPrecision;

    public FighterUI currentUI;

    public Fighter() : base() {}


    public void CopyFighter(Fighter fighter)
    {
        this.CopyCreature(fighter);
   
        this.nHealthMax = fighter.nHealthMax;
        this.nPower = fighter.nPower;
        this.nInitiative = fighter.nInitiative;
        this.nArmor = fighter.nArmor;
        this.nPrecision = fighter.nPrecision;

    }
    public int GetRandomInitiative()
    {
        return (int)Random.Range(1, 20) + this.nInitiative;
    }
    public virtual void PerformActionOnTarget(ActionType action, Fighter fighter)
    {
        if(fighter == null || fighter.sName == null)
            Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName);
        else
            Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName + "on target " + fighter.sName);

        if (action == ActionType.ATTACK){
            fighter.TakeDamage(this.GetDamage());
        }
        else if(action == ActionType.ESCAPE){


        }else if(action == ActionType.TALK){


        }
    }

    public virtual void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName + "on all enemy Group " );
    }

    public void TakeDamage(int damage)
    {
        nCurrentHealth -= damage;
        if (nCurrentHealth < 0)
            nCurrentHealth = 0;
        if (nCurrentHealth > nHealthMax)
            nCurrentHealth = nHealthMax;
    }
    public int GetDamage()
    {
        return nPower + 10;
    }
    public bool IsDead()
    {
        if (this.nCurrentHealth == 0)
            return true;
        return false;
    }

    public bool CanBeAttack()
    {
        if (!IsDead())
            return true;
        return false;
    }

}
