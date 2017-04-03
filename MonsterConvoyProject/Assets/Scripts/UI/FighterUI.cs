using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterUI : MonoBehaviour {

    public Fighter fighter;
    public TextMesh textMesh;
    private Renderer fighterRenderer;

	// Use this for initialization
	void Start () {
        SetUIInFighter();
        fighterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(fighter.nCurrentHealth == 0)
        {
            fighterRenderer.enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        UpdateText();

    }

    void UpdateText()
    {
        textMesh.text = fighter.nCurrentHealth + " / " + fighter.nHealthMax;
    }

    void SetUIInFighter()
    {
        fighter.currentUI = this;
    }
    

}
