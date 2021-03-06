﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enumSex { Female, Male, None}
[System.Serializable]
public class Fighter : Creature{

    public int nHealthMax;
    public int nCurrentHealth;
    public int nPower;
    public bool bIsImportant;

    public enumSex sexe;

    //public int nInitiative;
    //public int nArmor;
    //public int nPrecision;

    public bool bHasbeenAttcked;
    public bool bHasTookDamage;
    

    public FighterUI currentUI;
    public FighterUI lastAttackedUI;

    public bool bTryToescape = false;

    public bool justTookDamage = false;

    public bool justdidAction = false;
    public RollResultEnum lastActionResult;
    public ActionType lastAction = null;
    public bool performingAction = false;

    public Fighter() : base() {

    }


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
        this.bHasTookDamage = fighter.bHasTookDamage;
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
        performingAction = true;
        justdidAction = true;
        lastAction = action;

        lastAttackedUI = fighter.currentUI;

        if (fighter == null || fighter.sName == null)
            Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName);
        else
            Debug.Log("Fighter :" + this.sName + "  Perform action :" + action.sName + "on target " + fighter.sName);

        if (action == ActionType.ATTACK){

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            // float rand = 0.5f;
            float rand = Random.Range(0f, 1f);

            float terrain = GameObject.FindGameObjectWithTag("CombatTerrain").GetComponent<CombatTerrainInfo>().modRoll.GetValueOfAction(action, this.eCreatureType);
            rand += terrain;

            if (this.eCreatureType == CreatureType.Human)
            {
                if (!combatManager.humanGroupFighter.bIsSpecial)
                {
                    float bonus = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().GetBonus(action, (Human)this, (GroupMonsterFighter)combatManager.monsterGroupFighter);

                    rand += bonus;
                }

            }


            if (combatManager.protoScript != null && combatManager. protoScript.combat != null  && combatManager.protoScript.combat.currentTurn != null)
            {
                float l =  combatManager.protoScript.combat.currentTurn.fRoll;
                if (l != 0)
                    rand = l;
            }

            if (combatManager.humanGroupFighter.bIsSpecial)
            {
                rand = 0.5f;
            }
              int damage = this.GetDamage();

            if(eCreatureType == CreatureType.Human && !combatManager.humanGroupFighter.bIsSpecial)
            {
                float damageMod = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().GetBonusDmg((Human)this, (GroupMonsterFighter)combatManager.monsterGroupFighter);
                if(damageMod != 0)
                {
                    damage /= 2;
                }
            }

            if (rand >= combatManager.rollProbaManager.Attack.normal)
            {
                fighter.TakeDamage(damage * 2);
                AkSoundEngine.PostEvent("Play_" + sName + "_crit", GameObject.FindGameObjectWithTag("MainCamera"));

                lastActionResult = RollResultEnum.Crit;
            }
            else if (rand > combatManager.rollProbaManager.Attack.fail)
            {
                fighter.TakeDamage(damage);

                if(this.eCreatureType == CreatureType.Monster)
                    AkSoundEngine.PostEvent("Play_" + "merchant" + "_hit", GameObject.FindGameObjectWithTag("MainCamera"));
                else
                    AkSoundEngine.PostEvent("Play_" + sName + "_hit", GameObject.FindGameObjectWithTag("MainCamera"));

                lastActionResult = RollResultEnum.Normal;


            }
            else
            {

                lastActionResult = RollResultEnum.Fail;

                if(eCreatureType == CreatureType.Human)
                {
                    if (combatManager.protoScript != null && combatManager.protoScript.combat != null && combatManager.protoScript.combat.currentTurn != null)
                    {
                        if(combatManager.protoScript.combat.index < 7)
                            combatManager.protoScript.combat.HumainFailAttack();
                    }
                }


                Debug.Log("Fail");
                AkSoundEngine.PostEvent("Play_miss", GameObject.FindGameObjectWithTag("MainCamera"));
                GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
                CombatManager cm = g.GetComponent<CombatManager>();

                if (fighter.eCreatureType == CreatureType.Monster)
                    ((GroupMonsterFighter)cm.GetGroupFighterOfFighter(fighter)).OneFighterGotTargetted();
                else
                {
                    ((GroupHumanFighter)cm.GetGroupFighterOfFighter(fighter)).OneFighterGotTargetted();
 
                }
                ActionTalk(action, rand);
            }

            if (fighter.eCreatureType == CreatureType.Monster)
            {
                if (combatManager.protoScript != null && combatManager.protoScript.combat != null && combatManager.protoScript.combat.currentTurn != null)
                {
                    if (combatManager.protoScript.combat.nbAttack == 0 || (combatManager.protoScript.combat.nbAttack == 1 && combatManager.protoScript.combat.roundIt == 3) 
                        || combatManager.protoScript.combat.nbAttack == 2 || combatManager.protoScript.combat.nbAttack == 3)
                    {
                        combatManager.protoScript.combat.HumanAttack();
                    }
                    
                }
            }


            // ActionTalk(action, rand);

        }


    }

    public virtual void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        performingAction = true;
        performingAction = true;
        justdidAction = true;
        lastAction = action;


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
        if (g != null && g.GetComponent<CombatManager>().talkManager != null && g.GetComponent<CombatManager>().protoScript == null)
        {
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
           // sm.customTalk.follow = currentUI.dialogueAnchor.gameObject;
            sm.customTalk.NewTalk(eCreatureType, action, roll);
        }else if(g != null && g.GetComponent<CombatManager>().talkManager != null && g.GetComponent<CombatManager>().protoScript != null)
        {
            //g.GetComponent<CombatManager>().protoScript.combat.monsters.follow = currentUI.dialogueAnchor.gameObject;

            g.GetComponent<CombatManager>().protoScript.combat.Talk();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
            CombatManager cm = g.GetComponent<CombatManager>();
            (cm.GetGroupFighterOfFighter(this)).OneFighterTookDamage();

            bHasTookDamage = true;
            justTookDamage = true;
        }

        nCurrentHealth -= damage;
        bHasbeenAttcked = true;


        if (nCurrentHealth <= 0)
        {       nCurrentHealth = 0;
            AkSoundEngine.SetSwitch("Tension", "T4", GameObject.FindGameObjectWithTag("MainCamera"));
        }
        if (nCurrentHealth > nHealthMax)
            nCurrentHealth = nHealthMax;

    }
    public int GetDamage()
    {
        return nPower;
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
