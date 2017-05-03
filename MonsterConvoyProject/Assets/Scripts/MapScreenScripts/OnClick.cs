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

            PlaceType.Terrain type = GetComponent<PlaceType>().terrainType;

            string s = "";
            switch (type)
            {
                case (PlaceType.Terrain.DESERT):
                    s = "DESERT";
                    break;
                case (PlaceType.Terrain.FORET):
                    s = "FORET";
                    break;
                case (PlaceType.Terrain.GROTTE):
                    s = "GROTTE";
                    break;
                case (PlaceType.Terrain.MARECAGE):
                    s = "MARECAGE";
                    break;
                case (PlaceType.Terrain.MONTAGNE):
                    s = "MONTAGNE";
                    break;
                case (PlaceType.Terrain.NEIGE):
                    s = "NEIGE";
                    break;
                case (PlaceType.Terrain.PLAGE):
                    s = "PLAGE";
                    break;
                case (PlaceType.Terrain.PLAINE):
                    s = "PLAINE";
                    break;
            }

            mouseOver = false;

            //Change scene on node click
            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
            }
            SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
                    
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
            gameObject.transform.Rotate(new Vector3(0, 1f, 0));
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.EulerAngles(new Vector3(0, 0, 0));
        }

        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            oneLight.GetComponent<Light>().enabled = true;
        }

    }
    void OnMouseOver()
	{

        //Cleanup 
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            mouseOver = true;
            //Turn on highlight effect
            // oneLight.SetActive(true);
          //  oneLight.GetComponent<Light>().enabled = true;
        }
    }

	void OnMouseExit()
	{
        mouseOver = false;
        //Turn off highlight effect
        //Cleanup
        //oneLight.SetActive(false);
       // oneLight.GetComponent<Light>().enabled = false;
    }
}
