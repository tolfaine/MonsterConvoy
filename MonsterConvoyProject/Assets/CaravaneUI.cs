using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravaneUI : MonoBehaviour {

    public GameObject listMonsters;
    public GameObject prefabSlot;

    public Caravane caravane;

	// Use this for initialization
	void Start () {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
        UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateUI()
    {
        foreach(Transform child in listMonsters.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Fighter fighter in caravane.lFighters)
        {

            GameObject g = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity) as GameObject;
            g.transform.parent = listMonsters.transform;

            ImageFighterManager ifm = g.GetComponentInChildren<ImageFighterManager>();
            ifm.UpdateImage(fighter.nID);
        }
    }
}
