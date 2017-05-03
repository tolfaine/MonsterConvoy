using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureType { None, Monster, Human }

[System.Serializable]
public class Creature {

    public int nID;
    public string sName;
    public CreatureType eCreatureType = CreatureType.None;
    public GameObject prefab;

    public Creature() { }

    public void CopyCreature(Creature creature)
    {
        this.nID = creature.nID;
        this.sName = creature.sName;
        this.eCreatureType = creature.eCreatureType;
    }

    public CreatureType GetCreatureType() { return this.eCreatureType; }
   
}
