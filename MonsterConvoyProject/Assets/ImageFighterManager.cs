using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFighterManager : MonoBehaviour {

    private CombatManager cm;
	// Use this for initialization
	void Start () {
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if(g != null)
        {
            cm = g.GetComponent<CombatManager>();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
        if(cm != null)
        {
            if (cm.currentFighter != null)
                UpdateImage(cm.currentFighter.nID);
        }
	}

    public void UpdateImage(int id)
    {
        foreach(Transform trans in gameObject.transform)
        {
            ImageFighter image = trans.gameObject.GetComponent<ImageFighter>();
            if (image.idMonster == id)
                gameObject.GetComponentInChildren<Image>().sprite = image.sprite;

        }
    } 
}
