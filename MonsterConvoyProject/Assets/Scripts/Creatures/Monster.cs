using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Fighter{

    public int nFearPower;

     public Monster() : base() {
        this.eCreatureType = CreatureType.Monster;
    }

    public void CopyMonster(Monster monster)
    {
        this.CopyFighter(monster);
        this.nFearPower = monster.nFearPower;
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

            float rand = 0.1f;
            //float rand = Random.Range(0f, 1f);

            if (combatManager.scriptManager != null && combatManager.scriptManager.currentTurn != null)
            {
                rand = combatManager.scriptManager.currentTurn.fRoll;
            }

            if (rand > 0.99f)
            {
                ((GroupHumanFighter)groupHuman).GetCritFeared(this);
            }
            else if (rand > 0.2f)
            {
                ((GroupHumanFighter)groupHuman).GetFeared(this);
            }
            else
            {
                ((GroupHumanFighter)groupHuman).bCanBeFeared = false;
                Debug.Log("Fail Fear:" + rand.ToString());
            }
        }

        if (action == ActionType.TALK)
        {
           //((GroupHumanFighter)groupHuman).GetConvinced(this);

            CombatManager combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();

            float rand = 0.1f;
            //float rand = Random.Range(0f, 1f);

            if (combatManager.scriptManager != null && combatManager.scriptManager.currentTurn != null)
            {
                rand = combatManager.scriptManager.currentTurn.fRoll;
            }

            if (rand > 0.9f)
            {
                ((GroupHumanFighter)groupHuman).GetCritConvinced(this);
            }
            else if (rand > 0.2f)
            {
                ((GroupHumanFighter)groupHuman).GetConvinced(this);
            }
            else
            {
                Debug.Log("Fail Convice:" + rand.ToString());
                ((GroupHumanFighter)groupHuman).bCanListen = false;
            }

            ActionTalk(action, rand);
        }

    }

    public override void ActionTalk(ActionType action, float roll)
    {
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>().talkManager != null)
        {
            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            sm.customTalk.follow = currentUI.dialogueAnchor.gameObject;
            sm.customTalk.NewTalk(CreatureType.Monster, roll);
        }
    }

    public override void PerformActionOnSelf(ActionType action, GroupFighter monsterFighter)
    {
        if (action == ActionType.ESCAPE)
        {
            float rand = 0.5f;

            if (rand > 0.2f)
            {
                bTryToescape = true;
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
