using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnections : MonoBehaviour {

	public List<GameObject> neighbourNodes = new List<GameObject>();

	public GameObject path;
    int numPaths = 0;
    List<GameObject> pathList = new List<GameObject>();

    public static GameObject activeNode;

    private void Start()
    {
        for (int i = 0; i < neighbourNodes.Count; ++i)
        {
            neighbourNodes[i].GetComponent<NodeConnections>().AddNeighbour(gameObject); //Add ourselves to the neighbour. 
            CreatePath();
        }
    }

    public void CreatePath()
	{
        for (; numPaths < neighbourNodes.Count; ++numPaths)
        {
            pathList.Add(Instantiate(path, transform));
            pathList[numPaths].GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, neighbourNodes[numPaths].transform.position });
        }
	}

    public void DestroyPath(int pathIndex)
    {
        --numPaths;
        Destroy(pathList[pathIndex]);
        pathList.RemoveAt(pathIndex);
        pathList.TrimExcess();
    }
    
    //Adds a new node to our list of neighbour nodes
    public void AddNeighbour(GameObject newNeighbour)
    {
        if (!neighbourNodes.Contains(newNeighbour))
        {
            neighbourNodes.Add(newNeighbour);
            newNeighbour.GetComponent<NodeConnections>().AddNeighbour(gameObject); //Add ourselves to the neighbour. 
            CreatePath();
        }
    }

    //Remove a node from your list of neighbour nodes
    public void RemoveNeighbour(GameObject removedNeighbour)
    {
        if (neighbourNodes.Contains(removedNeighbour))
        {
           // DestroyPath(neighbourNodes.LastIndexOf(removedNeighbour));
           // removedNeighbour.GetComponent<NodeConnections>().RemoveNeighbour(gameObject); //Remove ourselves from the neighbour.
            neighbourNodes.Remove(removedNeighbour);
            //neighbourNodes.TrimExcess();
        }
    }
}
