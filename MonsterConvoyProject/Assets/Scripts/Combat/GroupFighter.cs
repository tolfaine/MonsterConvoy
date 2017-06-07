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

    public GroupFighter() { }

    public GroupLogic GetGroupLogic() { return this.groupLogic; }

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
        int nNbFighterAlive = 0;

        foreach(Fighter fighter in this.lFighters)
        {
            if (fighter.nCurrentHealth > 0)
                nNbFighterAlive++;
            else
            {
                lDeadFightersNotReplace.Add(fighter);
                lFighters.Remove(fighter);
            }
        }

        if (nNbFighterAlive == 0)
            allFightersDead = true;
    }

    public virtual void OneFighterTookDamage()
    {
        OneFighterGotTargetted();
    }

    public virtual void OneFighterGotTargetted()
    {

    }
}
