﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invasion : MonoBehaviour {

    Vector3 invasionOrigin; //The location of the portal 
    public int invasionSize = 1; //Multiplier which increases the invasion radius on every turn
    int invasionRadius = 12; //The initial radius of the invasion.
    float invasionGrowthRate = 0.0f;
    float lerpSpeed = 0.1f; //The speed which the invasion grows at the start of each turn (Does not impact the size of the invasion).
    Vector3 capitalPosition;
    Vector3 initialScale;
    public GameObject flag;
     
    void Start ()
    {
        //Set the invasion origin to the position of the portal. 
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("MapNode");
        for (int i = 0; i < nodes.Length; ++i)
        {
            if (nodes[i].GetComponent<PlaceType>().placeType == PlaceType.Place.PORTAIL)
                invasionOrigin = nodes[i].transform.position;
            else if (nodes[i].GetComponent<PlaceType>().placeType == PlaceType.Place.DEPART)
            {
                capitalPosition = nodes[i].transform.position;
            }
            invasionOrigin.y = transform.position.y; //We don't want to change the Y position. I set this manually.
            transform.position = invasionOrigin;
        }

        initialScale = new Vector3(invasionRadius, invasionRadius);
        transform.localScale = initialScale;
        OnNewLoop(); 
    }
    
    private void OnEnable()
    {
        IncreaseInvasionArea();
    }

    //HardCode
    private int turnNumber = 0;
    public void OnNewLoop()
    {
        invasionSize = 1;
        float distanceFromPortalToCapital = Vector3.Distance(capitalPosition, invasionOrigin);
        transform.localScale = initialScale;
        switch (turnNumber)
        {
            case 0:
                invasionGrowthRate = distanceFromPortalToCapital / 2;
                break;
            case 1:
                invasionGrowthRate = distanceFromPortalToCapital / 4;
                break;
            case 2:
                invasionGrowthRate = distanceFromPortalToCapital / 6;
                break;
            case 3:
                invasionGrowthRate = distanceFromPortalToCapital / 13;
                break;
            case 4:
                invasionGrowthRate = distanceFromPortalToCapital / 16;
                break;
            case 5:
                invasionGrowthRate = distanceFromPortalToCapital / 18;
                break;
            case 6:
                invasionGrowthRate = distanceFromPortalToCapital / 21;
                break;
            case 7:
                invasionGrowthRate = distanceFromPortalToCapital / 25;
                break;
            default:
                invasionGrowthRate = distanceFromPortalToCapital / 25;
                break;
        }
        turnNumber++;
    }

    private void Update()
    {
        Vector3 newScale = new Vector3(invasionRadius, invasionRadius) * invasionSize;

        newScale.x = Mathf.Lerp(newScale.x, transform.localScale.x, (1 - lerpSpeed));
        newScale.y = Mathf.Lerp(newScale.y, transform.localScale.y, (1 - lerpSpeed));
        transform.localScale = newScale;
    }

    //Set nodes which collide with invasion to invaded. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapNode"))
        {
            other.GetComponent<PlaceType>().invasionStatus = true;

            flag.transform.position = other.transform.position + (Vector3.left * 3) + (Vector3.back);
            GameObject.Instantiate(flag, other.transform);

            if (other.GetComponent<PlaceType>().placeType.Equals(PlaceType.Place.DEPART))
            {

                GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
                ProtoScript ps = null;

                if (g != null)
                {
                    ps = g.GetComponent<ProtoScript>();
                    ps.map.ToMutation();
                }else
                {
                    string sceneType = "mutations";

                    //Change scene on node click
                    for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
                    {
                        //TODO preserve node types in file and reload from there. 
                        SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
                    }
                    SceneManager.LoadSceneAsync(sceneType, LoadSceneMode.Additive);
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneType));
                }

                ps.map.ToMutation();
                //OnNewLoop();
            }
        }
    }

    public void IncreaseInvasionArea()
    {
        invasionSize += (int)invasionGrowthRate;
    }
}
