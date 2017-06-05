using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Fighter{

    //public int nFearPower;


    public Monster() : base() {
        this.eCreatureType = CreatureType.Monster;
    }

    public Monster(Fighter fighter) : base()
    {
        this.eCreatureType = CreatureType.Monster;
        this.CopyFighter(fighter);
    }


    public void CopyMonster(Monster monster)
    {
        this.CopyFighter(monster);
        //this.nFearPower = monster.nFearPower;
        this.eCreatureType = CreatureType.Monster;
    }
    public override void PerformActionOnTarget(ActionType action , Fighter fighter)
    {
        base.PerformActionOnTarget(action, fighter);

        if (action == ActionType.FEAR)
        {
            fighter.TakeDamage(this.GetDamage());
        }

    }

    public override void PerformActionOnTarget(ActionType action, GroupFighter groupHuman)
    {
        base.PerformActionOnTarget(action, groupHuman);

        if (action == ActionType.FEAR)
        {
         //   ((GroupHumanFighter)groupHuman).GetFeared(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            //float rand = 0.1f;
            float rand = Random.Range(0f, 1f);

            // CEST PAS OPTI DE LE CALCULER A CHAQUE FOIS
            float bonus = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().GetBonus(action,this, (GroupHumanFighter)groupHuman);

            rand += bonus;

            float terrain = GameObject.FindGameObjectWithTag("CombatTerrain").GetComponent<CombatTerrainInfo>().modRoll.GetValueOfAction(action, CreatureType.Monster);

            rand += terrain;

            if (combatManager.protoScript != null && combatManager.protoScript.combat != null && combatManager.protoScript.combat.currentTurn != null)
            {
                rand = combatManager.protoScript.combat.currentTurn.fRoll;
            }

            if (rand > 0.9f)
            {
                ((GroupHumanFighter)groupHuman).GetFeared(RollResultEnum.Crit, this);
            }
            else if (rand > 0.2f)
            {
                ((GroupHumanFighter)groupHuman).GetFeared(RollResultEnum.Normal, this);
            }
            else
            {
                ((GroupHumanFighter)groupHuman).GetFeared(RollResultEnum.Fail, this);
               // ((GroupHumanFighter)groupHuman).bCanBeFeared = false;
                Debug.Log("Fail Fear:" + rand.ToString());
            }

            ActionTalk(action, rand);
        }

        if (action == ActionType.TALK)
        {
           //((GroupHumanFighter)groupHuman).GetConvinced(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

           // float rand = 0.1f;
            float rand = Random.Range(0f, 1f);

            // CEST PAS OPTI DE LE CALCULER A CHAQUE FOIS
            float bonus = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().GetBonus(action, this, (GroupHumanFighter)groupHuman);
            rand += bonus;

            float terrain = GameObject.FindGameObjectWithTag("CombatTerrain").GetComponent<CombatTerrainInfo>().modRoll.GetValueOfAction(action, CreatureType.Monster);

            rand += terrain;

            if (combatManager.protoScript != null && combatManager.protoScript.combat != null && combatManager.protoScript.combat.currentTurn != null)
            {
                rand = combatManager.protoScript.combat.currentTurn.fRoll;
            }

            if (rand > 0.9f)
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(RollResultEnum.Crit, this);
            }
            else if (rand > 0.2f)
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(RollResultEnum.Normal, this);
            }
            else
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(RollResultEnum.Fail, this);
                Debug.Log("Fail Convice:" + rand.ToString());
                //((GroupHumanFighter)groupHuman).bCanListen = false;
            }

            ActionTalk(action, rand);
        }

    }

    public override void PerformActionOnSelf(ActionType action, GroupFighter monsterFighter)
    {
        if (action == ActionType.ESCAPE)
        {
            float rand = Random.Range(0f, 1f);

            if (rand > 0.3f)
            {
                bTryToescape = true;
                AkSoundEngine.PostEvent("Play_flee", GameObject.FindGameObjectWithTag("MainCamera"));
                ((GroupMonsterFighter)monsterFighter).MonsterEscaping();
            }
            else
            {

            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        CombatManager cm = g.GetComponent<CombatManager>();
   
        ((GroupMonsterFighter)cm.GetGroupFighterOfFighter(this)).MonsterEscaping();


    }
}
