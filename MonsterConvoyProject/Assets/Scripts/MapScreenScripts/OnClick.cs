﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {
    
    bool visited = false;
    public GameObject spotLight;
    GameObject oneLight;

    private void Start()
    {
        oneLight = spotLight;
        oneLight.SetActive(false);
        oneLight.transform.position = transform.position + (Vector3.up * 6);

        oneLight = GameObject.Instantiate(oneLight);

        switch (GetComponent<PlaceType>().placeType)
        {
            case (PlaceType.Place.TERRAIN):
                switch (GetComponent<PlaceType>().terrainType)
                {
                    case (PlaceType.Terrain.PLAGE):
                        break;
                    case (PlaceType.Terrain.GROTTE):
                        break;
                    case (PlaceType.Terrain.DESERT):
                        break;
                    case (PlaceType.Terrain.MONTAGNE):
                        break;
                    case (PlaceType.Terrain.PLAINE):
                        break;
                    case (PlaceType.Terrain.NEIGE):
                        break;
                    case (PlaceType.Terrain.MARECAGE):
                        break;
                }
                break;
            case (PlaceType.Place.DEPART):
                break;
            case (PlaceType.Place.PORTAIL):
                break;
            case (PlaceType.Place.CAMPEMENT):
                break;
            case (PlaceType.Place.DONJON):
                break;
            case (PlaceType.Place.TAVERNE):
                break;
            default:
                break;
        }
    }
    void OnMouseDown()
	{
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            NodeConnections.activeNode = gameObject;
            
            /*
            //Change scene on node click
            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
            }
            SceneManager.LoadSceneAsync("Proto", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Proto"));
            */
        }
    }

    private void Update()
    {
        if (gameObject.Equals(NodeConnections.activeNode) && !visited)
            visited = true;
    }
    void OnMouseOver()
	{

        //Cleanup 
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            //Turn on highlight effect
            oneLight.SetActive(true);
        }
    }

	void OnMouseExit()
	{
        //Turn off highlight effect
        //Cleanup
        oneLight.SetActive(false);
    }
}
