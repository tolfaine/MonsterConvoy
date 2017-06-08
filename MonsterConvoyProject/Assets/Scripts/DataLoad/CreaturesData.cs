using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class CreaturesData : MonoBehaviour {

    public static bool created = false;
    string sPath;
    JsonData jsCreatureData;

    public MonsterData defaultMonster;
    public HumanData defaultHuman;

    public List<MonsterData> lMonsters = new List<MonsterData>();
    public List<HumanData> lHumans = new List<HumanData>();

    // Use this for initialization
    void Awake () {

        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);

        //DontDestroyOnLoad(transform.parent.gameObject);
        sPath = File.ReadAllText(Application.dataPath + "/Ressources/CreaturesJSON.json");
        jsCreatureData = JsonMapper.ToObject(sPath);

        LoadMonsters();
        LoadHumans();
    }

    void LoadMonsters() {
        lMonsters = new List<MonsterData>();

        JsonData creature;
        string sCreatureType = "Monsters";

        for (int i = 0; i < jsCreatureData[sCreatureType].Count; i++)
        {
            creature = jsCreatureData[sCreatureType][i];
            MonsterData monsterData = new MonsterData(creature);

            if (monsterData.nPower == -1)
                monsterData.nPower = defaultMonster.nPower;
            if (monsterData.nHealthMax == -1)
                monsterData.nHealthMax = defaultMonster.nHealthMax;

            lMonsters.Add(monsterData);
        }
    }
    void LoadHumans()
    {
        lHumans = new List<HumanData>();
        JsonData creature;
        string sCreatureType = "Humans";

        for (int i = 0; i < jsCreatureData[sCreatureType].Count; i++)
        {
            creature = jsCreatureData[sCreatureType][i];
            HumanData humanData = new HumanData(creature);

            if (humanData.nPower == -1)
                humanData.nPower = defaultHuman.nPower;
            if (humanData.nHealthMax == -1)
                humanData.nHealthMax = defaultHuman.nHealthMax;

            lHumans.Add(humanData);
        }
    }
    public T GetFighterOfID<T>(CreatureType type, int id)
    {
        if (type == CreatureType.Human)
        {
            foreach(HumanData hData in lHumans)
            {
                if(hData.nId == id)
                {
                    return (T)(object)(hData.GetHuman());
                }
            }
        }
        else
        {
            foreach (MonsterData mData in lMonsters)
            {
                if (mData.nId == id)
                {
                    return (T)(object)(mData.GetMonster());
                }
            }
        }

        return default(T);
    }

    public Fighter GetClassFighterOfID(int id)
    {
        foreach (HumanData hData in lHumans)
        {
            if (hData.nId == id)
            {
                return (Human)(object)(hData.GetHuman());
            }
        }
        foreach (MonsterData mData in lMonsters)
        {
            if (mData.nId == id)
            {
                return (Monster)(object)(mData.GetMonster());
            }
        }

        return null;
    }

    public List<MonsterData> GetAllMonsterImportance(bool importance)
    {
        List<MonsterData> finalList = new List<MonsterData>();

        foreach (MonsterData data in lMonsters)
        {
            if (data.isImportant == importance)
                finalList.Add(data);
        }

        return finalList;
    }
    public MonsterData GetRandomMonsterWithImportance(bool importance)
    {
        List<MonsterData> finalList = new List<MonsterData>();

        foreach(MonsterData data in lMonsters)
        {
            if (data.isImportant == importance)
                finalList.Add(data);
        }

        int count = finalList.Count;
        int rand = (int)Random.Range(0, count - 1);

        return finalList[rand]; 
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
    
   // public int nInitiative;
   // public int nArmor;
    //public int nPrecision;

    public CreatureData() {}
    public CreatureData(JsonData data) {

        this.nId = int.Parse(data["id"].ToString());
        this.sName = data["name"].ToString();
        try
        {
            this.nHealthMax = int.Parse(data["healthMax"].ToString());

        }
        catch (KeyNotFoundException)
        {
            this.nHealthMax = -1;
        }

        try
        {
            this.nPower = int.Parse(data["power"].ToString());

        }
        catch (KeyNotFoundException)
        {
            this.nPower = -1;
        }

        // this.nInitiative = int.Parse(data["initiative"].ToString());
        //this.nArmor = int.Parse(data["armor"].ToString());
        //this.nPrecision = int.Parse(data["precision"].ToString());

        // IS IMPORTAT
    }
}

[System.Serializable]
public class MonsterData : CreatureData {

    // public int nFearPower;
    public bool isImportant;
    public MonsterData() : base() { }
    public MonsterData(JsonData data): base(data) {

        try
        {
            int imp = int.Parse(data["IsImportant"].ToString());

            if (imp > 0)
                this.isImportant = true;
            else
                this.isImportant = false;

        }
        catch (KeyNotFoundException)
        {
            this.isImportant = false;
        }

        


        //this.nFearPower = int.Parse(data["fearPower"].ToString());
    }

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
       // monster.nFearPower = this.nFearPower;
        monster.eCreatureType = CreatureType.Monster;
        monster.bIsImportant = this.isImportant;
        return monster;

        // IS IMPORTAT
    }
}

[System.Serializable]
public class HumanData : CreatureData {

   // public int nFearTolerance;

    public HumanData() : base() { }
    public HumanData(JsonData data): base(data){
        //this.nFearTolerance = int.Parse(data["fearTolerance"].ToString());

    }
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
        //human.nFearTolerance = this.nFearTolerance;
        human.eCreatureType = CreatureType.Human;
        return human;

        // IS IMPORTAT
    }
}