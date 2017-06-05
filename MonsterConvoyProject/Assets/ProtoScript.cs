using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ScriptedType{Selection, Combat, Map, Mutations}

public class ProtoScript : MonoBehaviour {
    public static bool created = false;

    public List<ScriptedType> orderScript = new List<ScriptedType>(1);
    public int currentIndex = 0;

    public ScriptedSelection selectin;
    public ScriptedCombat combat;
    public ScriptedMap map;
    public ScriptedMutation mutation;

    public CustomTalk customTalk;


   public  int nbFoisSelection = 0;
    public bool ended = false;
 
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        if (scene.name == "CAPITAL")
        {
            selectin.EnterSelection();
            nbFoisSelection++;

        }

        if (currentIndex < orderScript.Count)
        {
            ScriptedType type = orderScript[currentIndex];

            if (type == ScriptedType.Selection)
            {
                selectin.EnterSelection();
               // nbFoisSelection++;

            }
            else if (type == ScriptedType.Combat)
            {
                combat.EnterCombat();
            }
            else if (type == ScriptedType.Map)
            {
                map.EnterMap();
            }
            else if (type == ScriptedType.Mutations)
            {
                mutation.EnterMutation();
            }
        }
        currentIndex++;
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

        selectin.customTalk = customTalk;
        combat.customTalk = customTalk;
        map.customTalk = customTalk;
        mutation.customTalk = customTalk;
    }
    // Use this for initialization
    void Start () {






    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
