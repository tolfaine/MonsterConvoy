using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterNameUI : MonoBehaviour {

    private TextMesh textMesh;
    public FighterUI fighterUI;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
        textMesh.text = fighterUI.fighter.sName;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
