using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConviceUI : MonoBehaviour {

    public CombatManager combatManager;
    private TextMesh textMesh;
    public string sDisplayed;

    // Use this for initialization
    void Start()
    {
        textMesh = GetComponent<TextMesh>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        CheckConvince();
        textMesh.text = sDisplayed;
    }

    void CheckConvince()
    {
        sDisplayed = "Convinced : ";
        GroupHumanFighter group = (GroupHumanFighter)(combatManager.humanGroupFighter);
        sDisplayed += group.nCurrentConvice + " / " + group.nConvice;
    }
}
