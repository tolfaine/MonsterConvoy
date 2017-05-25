using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroupFighter {

    public List<Fighter> lFighters = new List<Fighter>();
    public CreatureType eCreatureType;
    public bool bHasBeenAttacked = false;
    public GroupLogic groupLogic;
    public bool allFightersDead = false;

    public bool bEscaping= false;

    public GroupFighter() { }

    public GroupLogic GetGroupLogic() { return this.groupLogic; }

    public virtual void CheckFightersLife()
    {
        int nNbFighterAlive = 0;

        foreach(Fighter fighter in this.lFighters)
        {
            if (fighter.nCurrentHealth > 0)
                nNbFighterAlive++;
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
