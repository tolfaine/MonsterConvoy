﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{

    public bool visited = false;
    bool highlighted = false;
    bool altered = false;

    public GameObject spotLight;

    private void Start()
    {
        spotLight.SetActive(true);
        spotLight.GetComponent<Light>().enabled = false;
        spotLight.transform.position = transform.position + (Vector3.up * 8);
        spotLight = GameObject.Instantiate(spotLight, gameObject.transform);
    }

    void OnMouseDown()
    {
        //If the neighbours of the node we click on contains the current active node. We can travel.
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode)
            && GetComponent<PlaceType>().placeType != PlaceType.Place.DEPART
            && (GetComponent<PlaceType>().placeType != PlaceType.Place.DONJON || !visited)
            && !EventSystem.current.IsPointerOverGameObject()) //HERE
        {
            NodeConnections.activeNode = gameObject;

            string sceneType;

            if (GetComponent<PlaceType>().placeType != PlaceType.Place.TERRAIN)
                sceneType = GetComponent<PlaceType>().placeType.ToString();
            else
                sceneType = GetComponent<PlaceType>().terrainType.ToString();

            //Change scene on node click
            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                //TODO preserve node types in file and reload from there. 
                SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
            }
            SceneManager.LoadSceneAsync(sceneType, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneType));
        }
    }

    bool strobeUp;
    int minLightIntensity = 3;
    int maxLightIntensity = 8;
    float strobeSpeed = 7.0f;
    
    bool grown = false;

    private void Update()
    {
        if (gameObject.Equals(NodeConnections.activeNode) && !visited)
            visited = true;

        if (gameObject.Equals(NodeConnections.activeNode))
        {
            if (!grown)
                Grow();
        }
        else
        {
            if (grown && gameObject.GetComponent<PlaceType>().placeType != PlaceType.Place.PORTAIL)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                grown = false;
            }
        }

        if (!highlighted && altered)
        {
            ReturnToNormal();
        }

        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode) 
            && GetComponent<PlaceType>().placeType != PlaceType.Place.DEPART  
            && (GetComponent<PlaceType>().placeType != PlaceType.Place.DONJON || !visited)  
            && !EventSystem.current.IsPointerOverGameObject())
        {
            spotLight.GetComponent<Light>().enabled = true;
            if (strobeUp)
            {
                spotLight.GetComponent<Light>().intensity = Mathf.Lerp(spotLight.GetComponent<Light>().intensity, maxLightIntensity, strobeSpeed * Time.deltaTime);
                if (spotLight.GetComponent<Light>().intensity >= maxLightIntensity - 0.1f)
                    strobeUp = false;
            }
            else
            {
                spotLight.GetComponent<Light>().intensity = Mathf.Lerp(spotLight.GetComponent<Light>().intensity, minLightIntensity, strobeSpeed * Time.deltaTime);
                if (spotLight.GetComponent<Light>().intensity <= minLightIntensity + 0.1f)
                    strobeUp = true;
            }
        }
    }

    void OnMouseOver()
    {
        highlighted = true;
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode)
            && GetComponent<PlaceType>().placeType != PlaceType.Place.DEPART 
            && (GetComponent<PlaceType>().placeType != PlaceType.Place.DONJON || !visited)
            && !EventSystem.current.IsPointerOverGameObject())
        {
            gameObject.transform.Rotate(new Vector3(0, 1f, 0));
            altered = true;
        }
    }

    void OnMouseExit()
    {
        highlighted = false; 
    }

    void ReturnToNormal()
    {
        float returnToNormalSpeed = 0.5f;
        gameObject.transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(gameObject.transform.localRotation.eulerAngles.y, 0, returnToNormalSpeed), 0);
        if (gameObject.transform.localRotation.y == 0)
        {
            altered = false;
        }
    }

    void Grow()
    {
        float grownSize = 1.5f;
        float growthSpeed = 0.2f;

        gameObject.transform.localScale = new Vector3(
            Mathf.Lerp(gameObject.transform.localScale.x, grownSize, growthSpeed),
            Mathf.Lerp(gameObject.transform.localScale.y, grownSize, growthSpeed),
            Mathf.Lerp(gameObject.transform.localScale.z, grownSize, growthSpeed));
        if (gameObject.transform.localScale.x >= grownSize - 0.1f)
        {
            grown = true;
        }
    }
}