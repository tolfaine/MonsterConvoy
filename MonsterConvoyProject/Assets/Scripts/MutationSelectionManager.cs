using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationSelectionManager : MonoBehaviour {

    public GameObject prefabUIMutation;

	// Use this for initialization
	void Start () {

        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        Initialise();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialise()
    {
        CaractMonster t = CaractMonster.CHAPEAU;

        MutationManager mutationManager = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>();
        CreaturesData creatureData = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>();

 
        List<MutationManager.MutationData> lData = mutationManager.GetRandomMutation(3);

        foreach(MutationManager.MutationData data in lData)
        { 
            GameObject g = Instantiate(prefabUIMutation, Vector3.zero, prefabUIMutation.transform.localRotation) as GameObject;

            MutationSelection ms = g.GetComponent<MutationSelection>();

            ms.idMonster = data.nIdMonster;
            ms.mutation = CaractMonster.GetCaractMonsterOfEnum(data.mutation);
            ms.text.text = "" + creatureData.GetFighterOfID<Monster>(CreatureType.Monster, ms.idMonster).sName + ms.mutation.sName;

            g.transform.parent = gameObject.transform;

        }


    }

    public void SelectMutation(int idMonster, CaractMonster mutation)
    {

    }
}
