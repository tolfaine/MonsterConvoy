using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterinCombat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("LoadCombat", 3);
	}

    void LoadCombat()
    {
        SceneManager.LoadScene("TestCombatWhitney", LoadSceneMode.Single);
    }
}
