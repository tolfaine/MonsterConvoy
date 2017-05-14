﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class CreaturesData : MonoBehaviour {

    string sPath;
    JsonData jsCreatureData;

    public List<MonsterData> lMonsters = new List<MonsterData>();
    public List<HumanData> lHumans = new List<HumanData>();

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(transform.parent.gameObject);
        sPath = File.ReadAllText(Application.dataPath + "/Ressources/CreaturesJSON.json");
        jsCreatureData = JsonMapper.ToObject(sPath);
        LoadMonsters();
        LoadHumans();
    }

    void LoadMonsters() {

        JsonData creature;
        string sCreatureType = "Monsters";

        for (int i = 0; i < jsCreatureData[sCreatureType].Count; i++)
        {
            creature = jsCreatureData[sCreatureType][i];
            MonsterData monsterData = new MonsterData(creature);
            lMonsters.Add(monsterData);
        }
    }
    void LoadHumans()
    {

        JsonData creature;
        string sCreatureType = "Humans";

        for (int i = 0; i < jsCreatureData[sCreatureType].Count; i++)
        {
            creature = jsCreatureData[sCreatureType][i];
            HumanData humanData = new HumanData(creature);
            lHumans.Add(humanData);
        }
    }

    public T GetRandomFighter<T>(CreatureType type) {
        if(type == CreatureType.Human)
        {
            int count = lHumans.Count;
            int rand = (int)Random.Range(0, count - 1);
            Human human = lHumans[rand].GetHuman();
            return (T)(object)human;
        }
       else
        {
            int count = lMonsters.Count;
            int rand = (int)Random.Range(0, count - 1);
            Monster monster = lMonsters[rand].GetMonster();
            return (T)(object)monster;

        }
    }
}

[System.Serializable]
public class CreatureData
{
    public int nId;
    public string sName;
    public int nHealthMax;
    public int nPower;
    public int nInitiative;
    public int nArmor;
    public int nPrecision;

    public CreatureData() {}
    public CreatureData(JsonData data) {

        this.nId = int.Parse(data["id"].ToString());
        this.sName = data["name"].ToString();
        this.nHealthMax = int.Parse(data["healthMax"].ToString());
        this.nPower = int.Parse(data["power"].ToString());
        this.nInitiative = int.Parse(data["initiative"].ToString());
        this.nArmor = int.Parse(data["armor"].ToString());
        this.nPrecision = int.Parse(data["precision"].ToString());
    }
}

[System.Serializable]
public class MonsterData : CreatureData {

    public int nFearPower;

    public MonsterData() : base() { }
    public MonsterData(JsonData data): base(data) { this.nFearPower = int.Parse(data["fearPower"].ToString()); }

    public Monster GetMonster()
    {
        Monster monster = new Monster();
        monster.nID = this.nId;
        monster.sName = this.sName;
        monster.nHealthMax = this.nHealthMax;
        monster.nCurrentHealth = monster.nHealthMax;
        monster.nPower = this.nPower;
        //monster.nInitiative = this.nInitiative;
        //monster.nArmor = this.nArmor;
        //monster.nPrecision = this.nPrecision;
        monster.nFearPower = this.nFearPower;
        monster.eCreatureType = CreatureType.Monster;
        return monster;
    }
}

[System.Serializable]
public class HumanData : CreatureData {

    public int nFearTolerance;

    public HumanData() : base() { }
    public HumanData(JsonData data): base(data){ this.nFearTolerance = int.Parse(data["fearTolerance"].ToString()); }
    public Human GetHuman()
    {
        Human human = new Human();
        human.nID = this.nId;
        human.sName = this.sName;
        human.nHealthMax = this.nHealthMax;
        human.nCurrentHealth = human.nHealthMax;
        human.nPower = this.nPower;
        //human.nInitiative = this.nInitiative;
        //human.nArmor = this.nArmor;
       // human.nPrecision = this.nPrecision;
        human.nFearTolerance = this.nFearTolerance;
        human.eCreatureType = CreatureType.Human;
        return human;
    }
}