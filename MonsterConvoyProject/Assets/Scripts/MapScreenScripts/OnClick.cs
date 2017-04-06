using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {

	public  Sprite mapNode;
	public  Sprite mapNodeHighlighted;
    
    void OnMouseDown()
	{
        if (GetComponent<ConnectNodes>().neighbourNodes.Contains(ConnectNodes.activeNode))
        {
            ConnectNodes.activeNode = gameObject;

            Color visited = new Color(0, 255, 0);
            GetComponent<SpriteRenderer>().color = visited;
            //TODO Place the actual battle scene here. TODO test if this unloads the current Scene thus resetting the map on reload.
            //SceneManager.LoadScene ("scene 1");
        }
    }

	void OnMouseOver()
	{
        if (GetComponent<ConnectNodes>().neighbourNodes.Contains(ConnectNodes.activeNode))
        {
            GetComponent<SpriteRenderer> ().sprite = mapNodeHighlighted;
		}
	}

	void OnMouseExit()
	{
		GetComponent<SpriteRenderer> ().sprite = mapNode;
	}
}
