using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fighter : Creature{

    public int nHealthMax;
    public int nCurrentHealth;
    public int nPower;
    public bool bIsImportant;

    //public int nInitiative;
    //public int nArmor;
    //public int nPrecision;

    public bool bHasbeenAttcked;
    

    public FighterUI currentUI;

    public bool bTryToescape = false;

    public bool justTookDamage = false;

    public Fighter() : base() {}


    public void CopyFighter(Fighter fighter)
    {
        this.CopyCreature(fighter);
   
        this.nHealthMax = fighter.nHealthMax;
        this.nCurrentHealth = fighter.nCurrentHealth;
        this.nPower = fighter.nPower;
        this.bIsImportant = fighter.bIsImportant;
        this.currentUI = fighter.currentUI;
        this.justTookDamage = fighter.justTookDamage;
        this.bHasbeenAttcked = fighter.bHasbeenAttcked;
       // this.nInitiative = fighter.nInitiative;
       // this.nArmor = fighter.nArmor;
       // this.nPrecision = fighter.nPrecision;

    }
    public int GetRandomInitiative()
    {
        return (int)Random.Range(1, 20); // + this.nInitiative;
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

            if (rand >= 0.90)
            {
                fighter.TakeDamage(this.GetDamage() * 2);
                AkSoundEngine.PostEvent("Play_miss", GameObject.FindGameObjectWithTag("MainCamera"));

            }
            else if (rand > 0.1)
            {
                fighter.TakeDamage(this.GetDamage());
                AkSoundEngine.PostEvent("Play_" + sName + "_hit", GameObject.FindGameObjectWithTag("MainCamera"));
            }
            else
            {
                Debug.Log("Fail");
                AkSoundEngine.PostEvent("Play_miss", GameObject.FindGameObjectWithTag("MainCamera"));
                GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
                CombatManager cm = g.GetComponent<CombatManager>();
                ((GroupMonsterFighter)cm.GetGroupFighterOfFighter(this)).OneFighterGotTargetted();

            }

           // ActionTalk(action, rand);

        }


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
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            sm.customTalk.follow = currentUI.dialogueAnchor.gameObject;
            sm.customTalk.NewTalk(eCreatureType, action, roll);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
            CombatManager cm = g.GetComponent<CombatManager>();
            (cm.GetGroupFighterOfFighter(this)).OneFighterTookDamage();
        }

        nCurrentHealth -= damage;
        justTookDamage = true;

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
