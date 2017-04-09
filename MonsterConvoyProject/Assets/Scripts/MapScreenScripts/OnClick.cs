using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {

	public  Sprite mapNode;
	public  Sprite mapNodeHighlighted;
    
    void OnMouseDown()
	{
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            NodeConnections.activeNode = gameObject;

            Color visited = new Color(0, 255, 0);
            GetComponent<SpriteRenderer>().color = visited;
            
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

	void OnMouseOver()
	{
        if (GetComponent<NodeConnections>().neighbourNodes.Contains(NodeConnections.activeNode))
        {
            GetComponent<SpriteRenderer> ().sprite = mapNodeHighlighted;
		}
	}

	void OnMouseExit()
	{
		GetComponent<SpriteRenderer> ().sprite = mapNode;
	}
}
