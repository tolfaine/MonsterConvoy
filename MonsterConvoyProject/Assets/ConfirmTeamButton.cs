using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmTeamButton : MonoBehaviour {

    Caravane caravane;
	// Use this for initialization
	void Start () {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
	}
	
	// Update is called once per frame
	void Update () {
        if (caravane.lFighters.Count == 4)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/Oui");
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/Non");
        }
    }
}
