using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectNodes : MonoBehaviour {

	public List<GameObject> neighbourNodes = new List<GameObject>();

	public GameObject path;
    int numPaths = 0;
    List<GameObject> pathList = new List<GameObject>();

    public static GameObject activeNode;
    public static GameObject finalNode;

    public void CreatePath()
	{
        for (; numPaths < neighbourNodes.Count; ++numPaths)
        {
            pathList.Add(Instantiate(path, transform));
            pathList[numPaths].GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, neighbourNodes[numPaths].transform.position });
        }
	}
    
	void Update()
	{
        if (activeNode.Equals(gameObject))
            for (int i = 0; i < neighbourNodes.Count; i++)
                pathList[i].GetComponent<LineRenderer>().enabled = true;
        else
            for (int i = 0; i < neighbourNodes.Count; i++)
                 pathList[i].GetComponent<LineRenderer>().enabled = false;
    }

    //Adds a new node to our list of neighbour nodes
    public void AddNeighbour(GameObject newNeighbour)
    {
        if (!neighbourNodes.Contains(newNeighbour)) {
            neighbourNodes.Add(newNeighbour);
            newNeighbour.GetComponent<ConnectNodes>().AddNeighbour(gameObject); //Add ourselves to the neighbour. 
            CreatePath();
        }
    }
}
