using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {
    
    bool visited = false;
    public GameObject spotLight;
    GameObject oneLight;

    bool mouseOver = false;

    private void Start()
    {
        oneLight = spotLight;
        oneLight.SetActive(true);
        oneLight.GetComponent<Light>().enabled = false;
        oneLight.transform.position = transform.position + (Vector3.up * 6);

        oneLight = GameObject.Instantiate(oneLight);

        /*
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
        */

    }
    void OnMouseDown()
    {
        //If the neighbours of the node we click on contains the current active node. We can travel.
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            NodeConnections.activeNode = gameObject;

            string sceneType;

            if (GetComponent<PlaceType>().placeType != PlaceType.Place.TERRAIN)
                sceneType = GetComponent<PlaceType>().placeType.ToString();
            else
                sceneType = GetComponent<PlaceType>().terrainType.ToString();

            mouseOver = false;

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

    private void Update()
    {
        if (gameObject.Equals(NodeConnections.activeNode) && !visited)
            visited = true;

        if (gameObject.Equals(NodeConnections.activeNode))
        {
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            
        }

        if (mouseOver)
        {
        }
        else
        {
        }

        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            oneLight.GetComponent<Light>().enabled = true;
        }

    }
    void OnMouseOver()
	{

        //Cleanup Whitney abstracted this for some reason :/ Ask her why 
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            gameObject.transform.Rotate(new Vector3(0, 1f, 0));

            //Turn on highlight effect
            // oneLight.SetActive(true);
            //  oneLight.GetComponent<Light>().enabled = true;
        }
    }

	void OnMouseExit()
	{
        //Turn off highlight effect
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

        //Cleanup
        //oneLight.SetActive(false);
        // oneLight.GetComponent<Light>().enabled = false;
    }
}
