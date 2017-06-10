using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GroupFighter {

    public List<Fighter> lFighters = new List<Fighter>();
    public List<Fighter> lInitialFighters = new List<Fighter>();

    public List<Fighter> lDeadFightersNotReplace = new List<Fighter>();

    public CreatureType eCreatureType;
    public bool bHasBeenAttacked = false;
    public GroupLogic groupLogic;
    public bool allFightersDead = false;

    public bool bEscaping= false;

    public bool bIsSpecial = false;

    public int nbFightersAlive = 0;

    public GroupFighter() { }

    public GroupLogic GetGroupLogic() { return this.groupLogic; }


    public bool SomeTryToRun()
    {
        foreach (Fighter fighter in lFighters)
        {
            if (fighter.bTryToescape)
                return true;
        }

        return false;
    }
    public virtual void SetInitialFighters()
    {
        foreach(Fighter fighter in lFighters)
        {
            if(eCreatureType == CreatureType.Human)
            {
                Human h = new Human();
                h.CopyHuman((Human)fighter);
                lInitialFighters.Add(h);
            }
            else
            {
                Monster m = new Monster();
                m.CopyMonster((Monster)fighter);
                lInitialFighters.Add(m);
            }
        }
    }

    public int GetNbFighterAlive()
    {
        int i = 0;

        foreach (Fighter fighter in this.lFighters)
        {
            if (!fighter.IsDead())
                i++;
        }
        return i;
    }

    public Fighter ReplacDeadFighter()
    {
        Fighter f = lDeadFightersNotReplace[0];
        lDeadFightersNotReplace.Remove(f);
        return f;
    }

    public virtual void CheckFightersLife()
    {
        nbFightersAlive = 0;

        foreach(Fighter fighter in this.lFighters)
        {
            if (fighter.nCurrentHealth > 0)
            {
                if(eCreatureType == CreatureType.Human)
                    nbFightersAlive++;
                else
                {
                    Monster m = (Monster)fighter;
                    if(!m.isBoss)
                        nbFightersAlive++;
                }
            }

            else
            {
                lDeadFightersNotReplace.Add(fighter);
                lFighters.Remove(fighter);
            }
        }

        if (nbFightersAlive == 0)
            allFightersDead = true;
    }

    public virtual void OneFighterTookDamage()
    {
        OneFighterGotTargetted();
    }

    public virtual void OneFighterGotTargetted()
    {

    }

    public virtual void GetFeared(RollResultEnum rollResult, Monster monster)
    {

    }

    public virtual void GetConvinced(RollResultEnum rollResult, Monster monster)
    {

    }
}
