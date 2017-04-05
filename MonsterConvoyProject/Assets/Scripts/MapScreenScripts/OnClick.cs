using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {

	public  Sprite mapNode;
	public  Sprite mapNodeHighlighted; 

	void OnMouseDown()
	{	
		if (GetComponent<ConnectNodes> ().neighbourNodes.Count > 0)
		if (GetComponentInParent<MapPopulate>().activeNode.GetComponent<ConnectNodes> ().neighbourNodes.Contains (gameObject)
			|| GetComponent<ConnectNodes>().neighbourNodes.Contains(GetComponentInParent<MapPopulate>().activeNode))
		{	
			gameObject.GetComponent<ConnectNodes>().activeNode = false;
			GetComponentInParent<MapPopulate>().activeNode = gameObject; //Make this the new active node
			gameObject.GetComponent<ConnectNodes>().activeNode = true;
			Color visited = new Color (0, 255, 0);
			GetComponent<SpriteRenderer> ().color = visited;
			//TODO Place the actual battle scene here. 
			//SceneManager.LoadScene ("BattleScene");
		}
	}

	void OnMouseOver()
	{
		if (GetComponent<ConnectNodes> ().neighbourNodes.Count > 0)
		if (GetComponentInParent<MapPopulate>().activeNode.GetComponent<ConnectNodes> ().neighbourNodes.Contains (gameObject)
			|| GetComponent<ConnectNodes>().neighbourNodes.Contains(GetComponentInParent<MapPopulate>().activeNode))
		{
			GetComponent<SpriteRenderer> ().sprite = mapNodeHighlighted;
			//TODO HighLight and unhighlight on mouse exit. 
		}
	}

	void OnMouseExit()
	{
		GetComponent<SpriteRenderer> ().sprite = mapNode;
	}
}
