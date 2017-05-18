using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumClass { Paladin, Chevalier, Bard }

public class CreaturePrefabManager : MonoBehaviour {

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

    [System.Serializable]
    public class PrefabDataWithHeads
    {
        public int id;
        public string name;
        public GameObject prefab;
        public List<GameObject> heads = new List<GameObject>(1);
    }

    public List<PrefabData> lMonsters = new List<PrefabData>(1);
    public List<PrefabDataHumain> lHumains = new List<PrefabDataHumain>(1);

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
