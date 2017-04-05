using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectNodes : MonoBehaviour {

	public List<GameObject> neighbourNodes = new List<GameObject>();

	public GameObject path;
	GameObject[] pathsArr;

	public bool activeNode = false; 

	void Start()
	{
	}

	public void updateNeighbours()
	{
		pathsArr = new GameObject[neighbourNodes.Count];// * 2];
			
		for (int i = 0; i < pathsArr.Length; i++) {
			pathsArr[i] = Instantiate (path, transform);
		}

		for (int i = 0; i < neighbourNodes.Count; i++) {
			pathsArr [i].GetComponent<LineRenderer> ().SetPositions (new Vector3[] {transform.position,neighbourNodes[i].transform.position});
		}
	}

	void Update()
	{
		if (activeNode)
			for (int i = 0; i < neighbourNodes.Count; i++) {
				pathsArr [i].GetComponent<LineRenderer> ().enabled = true;
			}
		else
			for (int i = 0; i < neighbourNodes.Count; i++) {
				if(!neighbourNodes[i].GetComponent<ConnectNodes>().activeNode)
					pathsArr [i].GetComponent<LineRenderer> ().enabled = false;
				else
					pathsArr [i].GetComponent<LineRenderer> ().enabled = true;				
			}
	}
}
