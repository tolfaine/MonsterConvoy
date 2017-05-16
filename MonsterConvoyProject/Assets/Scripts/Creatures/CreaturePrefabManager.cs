using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePrefabManager : MonoBehaviour {

    public class PrefabData
    {
        public int id;
        public string name;
        public GameObject prefab;
    }

    public List<PrefabData> lFighters = new List<PrefabData>(1);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
