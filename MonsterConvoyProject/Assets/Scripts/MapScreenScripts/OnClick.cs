using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {
    
    bool visited = false;
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
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
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
    float strobeSpeed = 0.5f;

    private void Update()
    {
        if (gameObject.Equals(NodeConnections.activeNode) && !visited)
            visited = true;

        //TODO Don't put this in update. 
        if (gameObject.Equals(NodeConnections.activeNode))
        {
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); 
        }
        else
        {
            if (gameObject.GetComponent<PlaceType>().placeType != PlaceType.Place.PORTAIL)
                gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            spotLight.GetComponent<Light>().enabled = true;
            if (strobeUp)
            {
                spotLight.GetComponent<Light>().intensity = Mathf.Lerp(spotLight.GetComponent<Light>().intensity, maxLightIntensity, strobeSpeed);
                if (spotLight.GetComponent<Light>().intensity >= maxLightIntensity - 0.1f)
                    strobeUp = false;
            }
            else
            {
                spotLight.GetComponent<Light>().intensity = Mathf.Lerp(spotLight.GetComponent<Light>().intensity, minLightIntensity, strobeSpeed);
                if (spotLight.GetComponent<Light>().intensity <= minLightIntensity + 0.1f)
                    strobeUp = true;
            }

        }

    }

    void OnMouseOver()
	{
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            gameObject.transform.Rotate(new Vector3(0, 1f, 0));
        }
    }

	void OnMouseExit()
	{
        //Turn off highlight effect
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
