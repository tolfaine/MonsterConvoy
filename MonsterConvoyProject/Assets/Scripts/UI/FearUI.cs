using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearUI : MonoBehaviour {

    public CombatManager combatManager;
    private TextMesh textMesh;
    public string sDisplayed;

    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMesh>(); ;
    }
	
	// Update is called once per frame
	void Update () {
        CheckFear();
        textMesh.text = sDisplayed;
    }

    void CheckFear()
    {
        sDisplayed = "Feared : ";
        GroupHumanFighter group = (GroupHumanFighter)(combatManager.humanGroupFighter);
        sDisplayed += group.nCurrentFear + " / " + group.nFear;
    }
}
