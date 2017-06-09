using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModComportement
{
    public ActionType.ActionEnum action;
}



public class CombatTerrainInfo : MonoBehaviour {


    public ModComportement modComportement;
    public ModRoll modRoll;

    public float combatChance;

    public bool bCanFightHere = true;


    private void Awake()
    {
        float rand = Random.Range(0f, 1f);

        if(rand > combatChance)
        {
            bCanFightHere = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
