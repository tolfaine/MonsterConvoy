using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollProbaManager : MonoBehaviour {
    public static bool created = false;

    [System.Serializable]
    public class RollProba
    {
        public float fail;
        public float normal;
    }

    [System.Serializable]
    public class SpecialProba
    {
        public float probaFight;
        public float bard;
        public float ed;
        public float slip;

    }

    [System.Serializable]
    public class ComportementProbaHumain
    {
        public float escape;
        public float discussion;
        public float attack;
    }

    public RollProba Attack;
    public RollProba Fear;
    public RollProba Discussion;
    public RollProba Escape;

    public SpecialProba specialProba;

    public ComportementProbaHumain humanComp;

    // Use this for initialization
    void Start () {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
