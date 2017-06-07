using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToCarte : MonoBehaviour {

    Scene currentScene;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToCarte()
    {

        string s = "CARTE";

        // SceneManager.UnloadScene(currentScene);
        //  SceneManager.UnloadSceneAsync(currentScene);

        Caravane caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();

        if (caravane.lFighters.Count == 4)
        {
            GameObject g =  GameObject.FindGameObjectWithTag("ProtoManager");
            if(g!= null)
            {
                ProtoScript ps = g.GetComponent<ProtoScript>();

                if (ps.nbFoisSelection == 2)
                {
                     Destroy(g);
                }
            }

            if (GameObject.FindGameObjectWithTag("RecrutementManager").GetComponent<RecrutementManager>().isAtCapital)
                SceneManager.LoadScene(s);
            else
            {
                SceneManager.UnloadScene(currentScene);

                SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
                for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
                {
                    GameObject go = SceneManager.GetActiveScene().GetRootGameObjects()[i];
                    if (go.tag != "Spotligth")
                        SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(true);
                }

            }

            AkSoundEngine.PostEvent("Play_Map", gameObject);
            /*
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));

            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                GameObject go = SceneManager.GetActiveScene().GetRootGameObjects()[i];
                if (go.tag != "Spotligth")
                    SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(true);
            }
            */
        }
    }
}
