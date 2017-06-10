using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMonsterBuble : MonoBehaviour {

    public GameObject positionHuman;
    public GameObject positionMonster;
    public GameObject currentMeshHolder;

    private CreaturePrefabManager prefabManager;
    private CombatManager cm;
    private CreaturesData creatureData;

    public Fighter currentFighter;

    public GameObject talk;

    public bool processedEnd = false;

    // Use this for initialization
    void Start () {
        prefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();
        creatureData = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>();
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null)
        {
            cm = g.GetComponent<CombatManager>();
        }
    }
	
    public void UpdateMeshHoldePosition()
    {

        if (currentFighter != cm.currentFighter)
        {
            currentFighter = cm.currentFighter;

            if (currentFighter.eCreatureType == CreatureType.Human)
            {
                currentMeshHolder.transform.position = positionHuman.transform.position;
            }
            else
            {
                currentMeshHolder.transform.position = positionMonster.transform.position;
            }

        }
    }

	// Update is called once per frame
	void Update () {



        if (currentFighter != cm.currentFighter)
        {
            currentFighter = cm.currentFighter;
            
            foreach(Transform child in currentMeshHolder.transform)
            {
                Destroy(child.gameObject);
            }


            if (currentFighter.eCreatureType == CreatureType.Human)
            {
                
                currentMeshHolder.transform.position = positionHuman.transform.position;

                //  GameObject gHuman = prefabManager.GetHuman(currentFighter.nID);

                GameObject gHuman = prefabManager.GetHumanOfSex(currentFighter.nID, currentFighter.sexe);
                int layer = LayerMask.NameToLayer("UI");


                ModelHumainUI modelUI = gHuman.GetComponent<ModelHumainUI>();
                ModelHumainUI modelCurrent = ((Human)currentFighter).currentUI.gameObject.GetComponentInChildren<ModelHumainUI>();

                modelUI.SetCaract(modelCurrent.caractCheveux);

                GameObject instanceHuman = Instantiate(gHuman,Vector3.zero, Quaternion.Euler(0, 90, 0)) as GameObject;
                instanceHuman.transform.parent = currentMeshHolder.transform;
                instanceHuman.transform.localPosition = Vector3.zero;

                instanceHuman.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else
            {
                
                currentMeshHolder.transform.position = positionMonster.transform.position;
                
                GameObject gMonster = prefabManager.GetMonster(currentFighter.nID);

                ModelMonsterUI modelUI = gMonster.GetComponent<ModelMonsterUI>();
                ModelMonsterUI modelCurrent = ((Monster)currentFighter).currentUI.gameObject.GetComponentInChildren<ModelMonsterUI>();

                modelUI.SetCaract(modelCurrent.caractMonster);

                GameObject instanceMonster = Instantiate(gMonster, Vector3.zero, Quaternion.Euler(0, 90, 0)) as GameObject;
                instanceMonster.transform.parent = currentMeshHolder.transform;
                instanceMonster.transform.localPosition = Vector3.zero;


                instanceMonster.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }

        }

        if(cm.bCombatEnded && !processedEnd)
        {
            processedEnd = true;

            if(cm.bSpecialFight && cm.specialType == SpecialType.Bard)
            {
                foreach (Transform child in currentMeshHolder.transform)
                {
                    Destroy(child.gameObject);
                }
                currentMeshHolder.transform.position = positionHuman.transform.position;

                GameObject gHuman = prefabManager.GetSpecial(31);
              //  ModelHumainUI modelUI = gHuman.GetComponent<ModelHumainUI>();
              //  ModelHumainUI modelCurrent = ((Human)currentFighter).currentUI.gameObject.GetComponentInChildren<ModelHumainUI>();

              //  modelUI.SetCaract(modelCurrent.caractCheveux);

                GameObject instanceHuman = Instantiate(gHuman, Vector3.zero, Quaternion.Euler(0, 90, 0)) as GameObject;
                instanceHuman.transform.parent = currentMeshHolder.transform;
                instanceHuman.transform.localPosition = Vector3.zero;

                instanceHuman.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }


        }

        if (talk.active == true)
        {
            currentMeshHolder.SetActive(true);
            foreach (Transform child in currentMeshHolder.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        else
        {
            foreach (Transform child in currentMeshHolder.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }

    public void UpdateImage(int id)
    {


    }
}
