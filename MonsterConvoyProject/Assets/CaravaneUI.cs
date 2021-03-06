﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravaneUI : MonoBehaviour {

    public GameObject listMonsters;
    public GameObject prefabSlot;

    public GameObject upArrow;
    public GameObject downArrow;

    public Caravane caravane;

    int teamIndex = 0;
    const int maxTeamDisplay = 4;

    public bool updateOnce = false;

    private void Awake()
    {
        teamIndex = 0;
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
        UpdateUI();
    }
    
	void Start () {
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        upArrow.SetActive(true);
        downArrow.SetActive(true);

        foreach (Transform child in listMonsters.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = teamIndex; (i < caravane.lFighters.Count) && (i < teamIndex + maxTeamDisplay); i++)
        {
            GameObject g = Instantiate(prefabSlot, Vector3.zero, Quaternion.identity) as GameObject;
            g.transform.parent = listMonsters.transform;

            
            Slot s = g.GetComponent<Slot>();
            if(s != null)
            {
                s.monster = caravane.lFighters[i];
            }
            

            ImageFighterManager ifm = g.GetComponentInChildren<ImageFighterManager>();
            ifm.UpdateImage(caravane.lFighters[i].nID);

            HealthUiFighter health = g.GetComponentInChildren<HealthUiFighter>();
            health.UpdateLife((float)caravane.lFighters[i].nCurrentHealth / (float)caravane.lFighters[i].nHealthMax);

            Tooltip tip = g.GetComponentInChildren<Tooltip>();
            if (tip != null)
            {
                tip.caravaneIndex = i;
                tip.gameObject.SetActive(false);
            }
        }

        if (teamIndex + maxTeamDisplay < caravane.lFighters.Count)
        {
            downArrow.SetActive(true);
        }
        else
        {
            downArrow.SetActive(false);
        }
        if (teamIndex > 0)
        {
            upArrow.SetActive(true);
        }
        else
        {
            upArrow.SetActive(false);
        }
    }

    public void IncreaseIndex()
    {
        teamIndex++;
        UpdateUI();
    }

    public void DecreaseIndex()
    {
        teamIndex--;
        UpdateUI();
    }

}
