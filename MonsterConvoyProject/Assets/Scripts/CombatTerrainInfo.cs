using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        else
        {
            bCanFightHere = true;
        }
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).name != "Menu" && SceneManager.GetSceneAt(i).name != "CARTE")
                AkSoundEngine.PostEvent(SceneManager.GetSceneAt(i).name, gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
