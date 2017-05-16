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
    public class PrefabDataWithHeads
    {
        public int id;
        public string name;
        public GameObject prefab;
        public List<GameObject> heads = new List<GameObject>(1);
    }

    public List<PrefabData> lMonsters = new List<PrefabData>(1);

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
