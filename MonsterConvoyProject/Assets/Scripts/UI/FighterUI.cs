using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterUI : MonoBehaviour {

    public Fighter fighter;
    public TextMesh textMesh;
    public Renderer fighterRenderer;
    public GameObject dialogueAnchor;
    public GameObject ui;
    public GameObject canvas;
    public ParticleSystem slash;

    public bool bHasBeenTurned = false;

    // Use this for initialization
    void Start () {
        SetUIInFighter();
        //fighterRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(fighter.nCurrentHealth == 0 && fighterRenderer!= null)
        {

            fighterRenderer.enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ui.SetActive(false); 
            canvas.SetActive(false);
        }
        if (fighter.justTookDamage)
        {
            fighter.justTookDamage = false;
            slash.Play();



        }

        if (fighter.bTryToescape && !bHasBeenTurned )
        {
            bHasBeenTurned = true;

          //  renderer.gameObject.transform.Rotate(new Vector3(0, 180, 0));
        }

    }
    // Update is called once per frame
    void FixedUpdate () {
        UpdateText();

    }

    void UpdateText()
    {
        textMesh.text = fighter.nCurrentHealth + ""; //+ " / " + fighter.nHealthMax;
    }

    void SetUIInFighter()
    {
        fighter.currentUI = this;
    }
    

}
