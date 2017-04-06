using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopulate : MonoBehaviour {

    public GameObject mapNode;
    GameObject[] mapNodeArr;
    int[] triangleIndices;

    public GameObject player;

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

        player = Instantiate(player, transform);

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
                        && !mapNodeArr[i].GetComponent<ConnectNodes>().neighbourNodes.Contains(mapNodeArr[j]))
                    {
                        //TODO ERROR if the shortest distance happens to be the first one we check. NearestNodeIndex2 will always be default(int)
                        nearestNodeIndex2 = nearestNodeIndex1;
                        nearestNodeIndex1 = j;
                        shorestDistance = Vector2.Distance(mapNodeArr[i].transform.position, mapNodeArr[j].transform.position);
                    }
                }
            }
            mapNodeArr[i].GetComponent<ConnectNodes>().AddNeighbour(mapNodeArr[nearestNodeIndex1]);
            mapNodeArr[i].GetComponent<ConnectNodes>().AddNeighbour(mapNodeArr[nearestNodeIndex2]);
        }

        ConnectNodes.activeNode = mapNodeArr[0];

        //Set player start to furthest left node. Set end point to furthest right node. 
        float furthestLeft = ConnectNodes.activeNode.transform.position.x;
        float furthestRight = ConnectNodes.activeNode.transform.position.x;
        for (int i = 0; i < nodePosArr.Length; ++i)
        {
            if (mapNodeArr[i].transform.position.x < furthestLeft)
            {
                ConnectNodes.activeNode = mapNodeArr[i];
                furthestLeft = ConnectNodes.activeNode.transform.position.x;
            }
            else if (mapNodeArr[i].transform.position.x >= furthestRight)
            {
                ConnectNodes.finalNode = mapNodeArr[i];
                furthestRight = mapNodeArr[i].transform.position.x;
            }
        }

        Color visited = new Color(0, 255, 0);
        ConnectNodes.activeNode.GetComponent<SpriteRenderer>().color = visited;

    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = ConnectNodes.activeNode.transform.position;
    }
}
