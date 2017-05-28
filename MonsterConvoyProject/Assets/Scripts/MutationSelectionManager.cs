using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationSelectionManager : MonoBehaviour {

    public GameObject prefabUIMutation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialise()
    {
        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();


    }

    public void SelectMutation(int idMonster, CaractMonster mutation)
    {

    }
}
