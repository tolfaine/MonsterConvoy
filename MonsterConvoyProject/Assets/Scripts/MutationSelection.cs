using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationSelection : MonoBehaviour {

    public int idMonster;
    public CaractMonster mutation;
    public Text text;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectMutation() {
        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();

        mutationManager.AddMutationID(idMonster, mutation);
    }
}
