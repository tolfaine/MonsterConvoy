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

    public bool bHasbeenAttcked;

    public FighterUI currentUI;

    public bool bTryToescape = false;

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

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            // float rand = 0.5f;
            float rand = Random.Range(0f, 1f);

            if (combatManager.scriptManager != null && combatManager.scriptManager.currentTurn != null)
            {
                rand = combatManager.scriptManager.currentTurn.fRoll;
            }

            if(rand >= 0.90)
            {
                fighter.TakeDamage(this.GetDamage()*2);
            }
            else if(rand > 0.1)
            {
                fighter.TakeDamage(this.GetDamage());
            }else
            {
                Debug.Log("Fail");
            }

        }

       // ActionTalk();
    }

    public virtual void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName + "on all enemy Group " );
       // ActionTalk();
    }

    public virtual void PerformActionOnSelf(ActionType action, GroupFighter groupFighter)
    {
       
    }

    public virtual void ActionTalk(ActionType action, float roll)
    {
        /*
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().scriptManager != null && g.GetComponent<CombatManager>().scriptManager.currentTurn != null)
        {
            // int index = g.GetComponent<CombatManager>().GetNextIndexLine();
            int index = g.GetComponent<CombatManager>().scriptManager.currentTurn.indexLineStart;

            if (index != -1)
            {
                ScriptManager sm = g.GetComponent<CombatManager>().scriptManager;
                sm.rpgTalk.lineToStart = index;
                sm.rpgTalk.lineToBreak = index;
                sm.rpgTalk.follow = currentUI.dialogueAnchor.gameObject;
                sm.rpgTalk.NewTalk();
            }
        }
        */

        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().talkManager != null)
        {

        }
    }

    public virtual void TakeDamage(int damage)
    {
        nCurrentHealth -= damage;
        bHasbeenAttcked = true;
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
        if (!IsDead() && !bTryToescape)
            return true;
        return false;
    }
    public bool CanAttack()
    {
        if (!IsDead() && !bTryToescape)
            return true;
        return false;
    }
}
