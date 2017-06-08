using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* SUMMARY
 * Class for placing nodes randomly across a 2D sprite and connecting them. 
 * Requires RandomMapNode prefab to be added in the editor. 
 */


public class GenerateMapRandomNodes: MonoBehaviour
{

    public GameObject mapNode;
    GameObject[] mapNodeArr;

    int minNodes = 12;
    int maxNodes = 30;
    int numNodes;

    float left, right, top, bottom, border;

    void Start()
    {
        border = 2.0f;
        left = GetComponent<SpriteRenderer>().sprite.bounds.min.x + border;
        right = GetComponent<SpriteRenderer>().sprite.bounds.max.x - border;
        top = GetComponent<SpriteRenderer>().sprite.bounds.max.y - border;
        bottom = GetComponent<SpriteRenderer>().sprite.bounds.min.y + border;

        numNodes = Random.Range(minNodes, maxNodes);
        mapNodeArr = new GameObject[numNodes];
        
        PlaceNodesRandom();
    }

    //Completely random node placement
    void PlaceNodesRandom()
    {

        Vector2[] nodePosArr = new Vector2[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            mapNodeArr[i] = Instantiate(mapNode, transform);
            mapNodeArr[i].transform.position = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
            nodePosArr[i] = mapNodeArr[i].transform.position;
        }

        //Find nearest 2 nodes and make them our neighbours.
        float shorestDistance;
        int nearestNodeIndex1, nearestNodeIndex2;
        nearestNodeIndex1 = nearestNodeIndex2 = default(int);
        for (int i = 0; i < mapNodeArr.Length; ++i)
        {
            shorestDistance = float.MaxValue;
            for (int j = 0; j < mapNodeArr.Length; ++j)
            {
                if (i != j)
                {
                    if (Vector2.Distance(mapNodeArr[i].transform.position, mapNodeArr[j].transform.position) <= shorestDistance
                        && !mapNodeArr[i].GetComponent<NodeConnections>().neighbourNodes.Contains(mapNodeArr[j]))
                    {
                        //TODO ERROR if the shortest distance happens to be the first one we check. NearestNodeIndex2 will always be default(int)
                        nearestNodeIndex2 = nearestNodeIndex1;
                        nearestNodeIndex1 = j;
                        shorestDistance = Vector2.Distance(mapNodeArr[i].transform.position, mapNodeArr[j].transform.position);
                    }
                }
            }
            mapNodeArr[i].GetComponent<NodeConnections>().AddNeighbour(mapNodeArr[nearestNodeIndex1]);
            mapNodeArr[i].GetComponent<NodeConnections>().AddNeighbour(mapNodeArr[nearestNodeIndex2]);
        }

        NodeConnections.activeNode = mapNodeArr[0];

        //Set player start to furthest left node. Set end point to furthest right node. 
        float furthestLeft = NodeConnections.activeNode.transform.position.x;
        float furthestRight = NodeConnections.activeNode.transform.position.x;
        for (int i = 0; i < nodePosArr.Length; ++i)
        {
            if (mapNodeArr[i].transform.position.x < furthestLeft)
            {
                NodeConnections.activeNode = mapNodeArr[i];
                furthestLeft = NodeConnections.activeNode.transform.position.x;
            }
            else if (mapNodeArr[i].transform.position.x >= furthestRight)
            {
                furthestRight = mapNodeArr[i].transform.position.x;
            }
        }

        Color visited = new Color(0, 255, 0);
        NodeConnections.activeNode.GetComponent<SpriteRenderer>().color = visited;
    }
}
