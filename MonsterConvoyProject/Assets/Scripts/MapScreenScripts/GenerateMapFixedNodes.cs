using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapFixedNodes : MonoBehaviour
{
    private GameObject[] nodeArr;

    public float searchRadius = 1.0f;
    public float searchIncrement = 1.0f;

    private void Start()
    {
        nodeArr = GameObject.FindGameObjectsWithTag("MapNode");

        #region Error Checking
        if (nodeArr.Length <= 0)
        {
            print("\nNo nodes found in scene. \n");
            return;
        }

        int totalWeight = 0;
        foreach (GameObject node in nodeArr)
        {
            totalWeight += node.GetComponent<NodeWeight>().weight;
        }
        if (totalWeight % 2 != 0)
        {
            print("\nTotal weight of all nodes must be an even number. \n");
            return;
        }
        #endregion

        for (int i = 0; i < nodeArr.Length; ++i)
        {
            float sR = searchRadius;
            List<GameObject> neighboursFound = new List<GameObject>(); //The nodes we find which we can connect to.

            //Error checking. 
            if (nodeArr[i].GetComponent<NodeWeight>().weight >= nodeArr.Length)
            {
                print("\nNode " + nodeArr[i].gameObject.name + " has too large a weight.\n");
                return;
            }

            //Add the nodes that have already added us to the neighhbours found list. 
            neighboursFound.AddRange(nodeArr[i].GetComponent<NodeConnections>().neighbourNodes);

            //This can currently get stuck if there are not enough places left to connect. 
            while (neighboursFound.Count < nodeArr[i].GetComponent<NodeWeight>().weight)
            {
                if (sR >= 100) //Prevents us from getting stuck in an infinite loop when the mapping doesn't work. TODO fix this. 
                    break;

                sR += searchIncrement; //Note this means that the first search will always be the starting search radius + the increment.
                for (int j = 0; j < nodeArr.Length; ++j)
                {
                    if (i != j) //We cannot connect to ourselves.
                    {
                        if (Vector2.Distance(nodeArr[i].transform.position, nodeArr[j].transform.position) < sR
                                && !neighboursFound.Contains(nodeArr[j]) && nodeArr[j].GetComponent<NodeWeight>().weight > nodeArr[j].GetComponent<NodeConnections>().neighbourNodes.Count) //TODO Order these for efficiency. 
                        {
                            neighboursFound.Add(nodeArr[j]);
                        }
                    }
                }
            }

            for (int j = nodeArr[i].GetComponent<NodeConnections>().neighbourNodes.Count; j < nodeArr[i].GetComponent<NodeWeight>().weight; ++j)
            {
                int newNeighbourIndex = Random.Range(nodeArr[i].GetComponent<NodeConnections>().neighbourNodes.Count, neighboursFound.Count);
                nodeArr[i].GetComponent<NodeConnections>().AddNeighbour(neighboursFound[newNeighbourIndex]);
            }
        }

        NodeConnections.activeNode = nodeArr[0];

        //Set player start to furthest left node. Set end point to furthest right node. 
        float furthestLeft = NodeConnections.activeNode.transform.position.x;
        float furthestRight = NodeConnections.activeNode.transform.position.x;

        for (int i = 0; i < nodeArr.Length; ++i)
        {
            if (nodeArr[i].transform.position.x < furthestLeft)
            {
                NodeConnections.activeNode = nodeArr[i];
                furthestLeft = NodeConnections.activeNode.transform.position.x;
            }
            else if (nodeArr[i].transform.position.x >= furthestRight)
            {
                NodeConnections.finalNode = nodeArr[i];
                furthestRight = nodeArr[i].transform.position.x;
            }
        }

        Color visited = new Color(0, 255, 0);
        NodeConnections.activeNode.GetComponent<SpriteRenderer>().color = visited;
    }
}