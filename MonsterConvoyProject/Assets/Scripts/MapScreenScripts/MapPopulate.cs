using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class MapPopulate : MonoBehaviour {

	public GameObject MapNode;

	GameObject[] mapNodes;
	public GameObject activeNode;
	public GameObject finalNode;
	public GameObject player; 

	int minNodes = 12;
	int maxNodes = 30;
	int numNodes;

	float left,right,top,bottom;

	LineRenderer pathLineRenderer;
	List<LineRenderer> lineRenderers = new List<LineRenderer> ();
	List<Vector2> paths = new List<Vector2> (); 

	List<Site> siteList = new List<Site>();

	Vector2[] nodePositions; 
	int[] indices;

	void Start () {
		pathLineRenderer = GetComponent<LineRenderer> ();
		float border = 2.0f; 
		left = GetComponent<SpriteRenderer> ().sprite.bounds.min.x + border;
		right = GetComponent<SpriteRenderer> ().sprite.bounds.max.x -border;
		top = GetComponent<SpriteRenderer> ().sprite.bounds.max.y - border;
		bottom = GetComponent<SpriteRenderer> ().sprite.bounds.min.y + border;
	
		numNodes = Random.Range (minNodes, maxNodes);

		//Completely random placement. Maybe works for prototype.
		for (int i = 0; i < numNodes; i++) {
			Vector2f randPos = new Vector2f (Random.Range (left, right), Random.Range (bottom, top));
			siteList.Add (Site.Create (randPos, i, 1.0f));
		}
		Site.SortSites (siteList);

		mapNodes = new GameObject[siteList.Count];
		nodePositions = new Vector2[siteList.Count];
		for (int i = 0; i < siteList.Count; i++) {
			mapNodes [i] = Instantiate (MapNode);
			mapNodes [i].transform.parent = transform;
			mapNodes [i].transform.position = new Vector2 (siteList [i].Coord.x, siteList [i].Coord.y);
			nodePositions [i] = mapNodes [i].transform.position;
			for (int j = 0; j < siteList [i].NeighborSites ().Count; j++) {
				List<Site> tempSiteList = siteList [i].NeighborSites ();
				mapNodes [i].GetComponent<ConnectNodes> ().neighbourNodes [j].transform.position = new Vector2 (tempSiteList [j].Coord.x, tempSiteList [j].Coord.y);//TODO fix
			}
		}

		activeNode = mapNodes [0];
		activeNode.GetComponent<ConnectNodes> ().activeNode = true;

		player = Instantiate (player);
		player.transform.position = activeNode.transform.position;

		Triangulator t = new Triangulator (nodePositions);
		indices = t.Triangulate ();
		//Set player start to furthest left node. Set end point to furthest right node. Currently necessary 
		float furthestLeft = activeNode.transform.position.x;
		float furthestRight = activeNode.transform.position.x;
		for (int i = 0; i < mapNodes.Length; i++) {
			if (mapNodes [i].transform.position.x < furthestLeft) {
				activeNode.GetComponent<ConnectNodes> ().activeNode = false;
				activeNode = mapNodes [i];
				activeNode.GetComponent<ConnectNodes> ().activeNode = true;
				furthestLeft = activeNode.transform.position.x;
			} else if (mapNodes [i].transform.position.x >= furthestRight) {
				finalNode = mapNodes [i];
				furthestRight = mapNodes [i].transform.position.x;
			}
		}
		for (int i = 0; i < indices.Length - 2; i+=3) {
			mapNodes [indices [i]].GetComponent<ConnectNodes> ().neighbourNodes.Add (mapNodes [indices [i + 1]]);
			mapNodes [indices [i]].GetComponent<ConnectNodes> ().neighbourNodes.Add (mapNodes [indices [i + 2]]);
			mapNodes [indices [i+1]].GetComponent<ConnectNodes> ().neighbourNodes.Add (mapNodes [indices [i + 2]]);
		//	Vector3[] v3 = {mapNodes [indices [i]].transform.position, mapNodes [indices [i + 1]].transform.position, mapNodes [indices [i + 2]].transform.position};
		//	pathLineRenderer.SetPositions (v3);
		//	gameObject.AddComponent<LineRenderer> ();
		//	lineRenderers.Add (pathLineRenderer);
		}
		foreach (GameObject mn in mapNodes) {
			mn.GetComponent<ConnectNodes> ().updateNeighbours ();
		}
	}
		

	public void Update()
	{
		player.transform.position = activeNode.transform.position;

		if (finalNode.Equals (activeNode)) {
			//Congrats. you have unlocked nothing TODO end level logic (not here)  
		}
	}
}
