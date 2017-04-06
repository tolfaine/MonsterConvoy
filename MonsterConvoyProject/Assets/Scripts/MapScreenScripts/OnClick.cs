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
            //TODO  This unloads the mapscene meaning all will be reinitialise on return

            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
            }
            SceneManager.LoadSceneAsync("Proto", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Proto"));
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
