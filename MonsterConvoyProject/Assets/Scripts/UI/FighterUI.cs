using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterUI : MonoBehaviour {

    public Fighter fighter;
    public TextMesh textMesh;
    public Renderer fighterRenderer;
    public GameObject dialogueAnchor;
    public GameObject ui;
    public GameObject canvas;
    public ParticleSystem slash;

    public ParticleSystem attackParticules = null;
    public ParticleSystem impParticules = null;

    public ParticleSystem talkParticules = null;
    public ParticleSystem fearParticules = null;



    public ParticleSystem particulPlaying = null;

    public bool bHasBeenTurned = false;

    public ParticuleManager pm;

    public bool initPar = false;

    public Image lifeImage;
    public float maxLifeWidth;
    public Vector3 positionMax;

    public Text hpTxt;




    // Use this for initialization
    void Start () {
        SetUIInFighter();

        RectTransform t = lifeImage.transform as RectTransform;
        maxLifeWidth = t.rect.width;
        positionMax = t.localPosition;
        //fighterRenderer = GetComponent<Renderer>();
    }

    public void InitParticules()
    {
        GameObject g = GameObject.FindGameObjectWithTag("ParticuleManager");
        if (g != null)
        {
            pm = g.GetComponent<ParticuleManager>();

            GameObject g1 = pm.GetParticuleAttack(fighter);
            if(g1 != null )
                attackParticules = g1.GetComponent<ParticleSystem>();

            GameObject g2 = pm.GetParticuleImpact(fighter);
            if (g2 != null)
                impParticules = g2.GetComponent<ParticleSystem>();
        }

    }
    void Update()
    {
        if(fighter.nID != 0)
        {
            if (!initPar)
            {
                initPar = true;
                InitParticules();
            }
        }
        if(fighter.nCurrentHealth == 0 && fighterRenderer!= null)
        {

            fighterRenderer.enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ui.SetActive(false); 
            canvas.SetActive(false);

            Renderer[] rs = GetComponentsInChildren<Renderer>();
                foreach (Renderer r in rs)
                    r.enabled = false;

            // mutation 
        }
        if (fighter.justTookDamage)
        {
            fighter.justTookDamage = false;
            slash.Play();
     
        }

        if (fighter.bTryToescape && !bHasBeenTurned )
        {
            bHasBeenTurned = true;
            fighterRenderer.gameObject.transform.Rotate(new Vector3(0, 180, 0));
            //  renderer.gameObject.transform.Rotate(new Vector3(0, 180, 0));
        }

        if(fighter.justdidAction)
        {
            fighter.justdidAction = false;

            if(fighter.lastAction == ActionType.ATTACK)
            {

                if(fighter.lastActionResult == RollResultEnum.Fail)
                {
                    GameObject go = Instantiate(pm.GetParticuleOfAction(fighter.lastAction, fighter.lastActionResult)) as GameObject;
                    go.transform.position = ui.transform.position;
                    go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);

                    particulPlaying = go.GetComponent<ParticleSystem>();

                    particulPlaying.Play();

                    Destroy(go, 5f);
                }
                else
                {
                    GameObject partic = pm.GetParticuleAttack(fighter);
                    if(partic != null)
                    {
                        GameObject go = Instantiate(partic) as GameObject;
                        go.transform.position = ui.transform.position;
                        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);

                        particulPlaying = go.GetComponent<ParticleSystem>();

                        particulPlaying.Play();

                        Destroy(go, 5f);
                    }


                    partic = pm.GetParticuleImpact(fighter);
                    if (partic != null)
                    {
                        GameObject go2 = Instantiate(partic) as GameObject;
                        go2.transform.position = fighter.lastAttackedUI.ui.transform.position;
                        go2.transform.localPosition = new Vector3(go2.transform.localPosition.x , go2.transform.localPosition.y, go2.transform.localPosition.z);
                        go2.GetComponent<ParticleSystem>().Play();

                        Destroy(go2, 5f);
                    }



                    // attackParticules.Play();
                    //  particulPlaying = attackParticules;
                }

            }


            else if(fighter.lastAction == ActionType.TALK)
            {
                RollResultEnum result = RollResultEnum.Normal;

                if(fighter.eCreatureType == CreatureType.Monster)
                {
                    result = fighter.lastActionResult;
                }

                GameObject go = Instantiate(pm.GetParticuleOfAction(fighter.lastAction, result)) as GameObject;
                GameObject meshHolder = GameObject.FindGameObjectWithTag("DialogueMesh");

                if (meshHolder == null)
                    go.transform.position = ui.transform.position;
                else
                {
                //    meshHolder.GetComponent<MeshMonsterBuble>().UpdateMeshHoldePosition();
                    go.transform.position = meshHolder.transform.position;

                }

                go.transform.localPosition = new Vector3(go.transform.localPosition.x -1f, go.transform.localPosition.y, go.transform.localPosition.z);

                talkParticules = go.GetComponent<ParticleSystem>();

                talkParticules.Play();
                particulPlaying = talkParticules;

                Destroy(go, 5f);
            }
            else if(fighter.lastAction == ActionType.FEAR)
            {

                GameObject go = Instantiate(pm.GetParticuleOfAction(fighter.lastAction, fighter.lastActionResult)) as GameObject;

                GameObject meshHolder = GameObject.FindGameObjectWithTag("DialogueMesh");

                if (meshHolder == null)
                    go.transform.position = ui.transform.position;
                else
                {
                  //  meshHolder.GetComponent<MeshMonsterBuble>().UpdateMeshHoldePosition();
                    go.transform.position = meshHolder.transform.position;

                }
                go.transform.localPosition = new Vector3(go.transform.localPosition.x -1f, go.transform.localPosition.y, go.transform.localPosition.z);

                fearParticules = go.GetComponent<ParticleSystem>();

                fearParticules.Play();
                particulPlaying = fearParticules;

                Destroy(go, 5f);
            }
            else
            {
                particulPlaying = null;
            }
        }

        if(particulPlaying == null || particulPlaying.isStopped)
        {
            fighter.performingAction = false;
        }

    }
    // Update is called once per frame
    void FixedUpdate () {
        UpdateText(); //TODO this is broken
    }

    void UpdateText()
    {
        // textMesh.text = fighter.nCurrentHealth + ""; //+ " / " + fighter.nHealthMax;
        hpTxt.text = fighter.nCurrentHealth + "";

        float curr = (float)fighter.nCurrentHealth;
        float max = (float)fighter.nHealthMax;
        float ratio = curr / max;

        //RectTransform t = lifeImage.transform as RectTransform;

        //  lifeImage.rectTransform.sizeDelta = new Vector2(maxLifeWidth * ratio, t.rect.height);
        lifeImage.fillAmount = ratio;


    }

    void SetUIInFighter()
    {
        fighter.currentUI = this;
    }
    

}
