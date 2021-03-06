﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumClass { Paladin, Chevalier, Bard }

public class CreaturePrefabManager : MonoBehaviour {

    public static bool created = false;

    [System.Serializable]
    public class PrefabData
    {
        public int id;
        public string name;
        public GameObject prefab;
    }

    [System.Serializable]
    public class PrefabDataHumain
    {
        public int id;
        public string name;
        public GameObject prefabM;
        public GameObject prefabF;
    }
    /*
    [System.Serializable]
    public class PrefabDataWithHeads
    {
        public int id;
        public string name;
        public GameObject prefab;
        public List<GameObject> heads = new List<GameObject>(1);
    }*/

    public List<PrefabData> lMonsters = new List<PrefabData>(1);
    public List<PrefabData> lBoss = new List<PrefabData>(1);
    public List<PrefabDataHumain> lHumains = new List<PrefabDataHumain>(1);
    public List<PrefabData> lSpecial = new List<PrefabData>(1);

    public enumSex GetSexPrefabId(GameObject prefab, int id)
    {
        PrefabDataHumain prefabData = GetHumanData(id);

        if (prefab == prefabData.prefabF)
            return enumSex.Female;
        else if (prefab == prefabData.prefabM)
            return enumSex.Male;
        else
            return enumSex.None;
    }

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetRandomHuman()
    {
        // Random la class
        // Random la tete en fonction de la class
        // Set les script de l'humain en fonction de la couleur des cheveux / hache ou pas 

        return null;
    }
    public int GetRandomHumanID()
    {
        int randomIndex = Random.Range(0, lHumains.Count);

        return lHumains[randomIndex].id;
    }

    public GameObject GetSpecial(int id)
    {
        foreach (PrefabData prefabD in lSpecial)
        {
            if (prefabD.id == id)
            {
                return prefabD.prefab;
            }
        }
        return null;
    }

    public PrefabDataHumain GetHumanData(int id)
    {
        foreach (PrefabDataHumain prefabD in lHumains)
        {
            if (prefabD.id == id)
            {
                return prefabD;

            }
        }
        return null;
    }

    public GameObject GetHumanOfSex(int id, enumSex sex)
    {
        foreach (PrefabDataHumain prefabD in lHumains)
        {
            if (prefabD.id == id)
            {
                if(sex == enumSex.Female)
                    return prefabD.prefabF;
                else
                    return prefabD.prefabM;
            }
        }
        return null;
    }

    public GameObject GetHuman(int id)
    {
        foreach (PrefabDataHumain prefabD in lHumains)
        {
            if (prefabD.id == id)
            {
                float randomGender = Random.Range(0f, 1f);
                if (randomGender > 0.5)
                    return prefabD.prefabF;
                else
                    return prefabD.prefabM;
            }
        }
        return null;
    }

    public GameObject GetMonster(int id)
    {
        foreach(PrefabData prefabD in lMonsters)
        {

            if (prefabD.id == id)
                return prefabD.prefab;
        }
        return null;
    }
}
