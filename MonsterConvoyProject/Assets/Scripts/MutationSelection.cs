using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MutationSelection : MonoBehaviour {

    public int idMonster;
    public CaractMonster mutation;
    public Text text;
    Scene currentScene;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
    }

    public void SelectMutation() {
        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();

        mutationManager.AddMutationID(idMonster, mutation);


        string s = "CAPITAL";
        SceneManager.LoadScene(s);

        // SceneManager.UnloadScene(currentScene);
        /*
         SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
         for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
         {
             GameObject go = SceneManager.GetActiveScene().GetRootGameObjects()[i];
             if (go.tag != "Spotligth")
                 SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(true);
         }

         Invasion invasion = GameObject.FindGameObjectWithTag("Invasion").GetComponent<Invasion>();
         invasion.OnNewLoop();
         */

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
        ProtoScript ps = null;

        if (g != null)
        {
            ps = g.GetComponent<ProtoScript>();
            ps.map.EnterMap();
            ps.currentIndex++;
        }


    }
}
