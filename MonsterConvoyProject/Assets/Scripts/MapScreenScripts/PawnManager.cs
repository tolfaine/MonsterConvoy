using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour {

    static PawnManager instance;

    Dictionary<PlaceType.Terrain, int> numTerrainUsed = new Dictionary<PlaceType.Terrain, int>();
    
    public List<GameObject> pawnPrefabs = new List<GameObject>();
    private GameObject[] pawns;

    public static PawnManager Instance()
    {
        return instance;
    }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Awake()
    {
        //Assign terrain at runtime
        pawns = GameObject.FindGameObjectsWithTag("MapNode");

        for (int i = 1; i < System.Enum.GetNames(typeof(PlaceType.Terrain)).Length; ++i)
        {
            numTerrainUsed.Add((PlaceType.Terrain)i, 0);
        }

        for (int i = 0; i < pawns.Length; ++i)
        {
            if (pawns[i].GetComponent<PlaceType>().placeType == PlaceType.Place.TERRAIN && pawns[i].GetComponent<PlaceType>().terrainType == PlaceType.Terrain.NULL)
            {
                int randTerrainInt = Random.Range(1, System.Enum.GetNames(typeof(PlaceType.Terrain)).Length + 1);
                //TODO is this what's causing long compile time? 
                while (!numTerrainUsed.ContainsKey((PlaceType.Terrain) randTerrainInt) && numTerrainUsed.Count > 0)
                    randTerrainInt = Random.Range(1, System.Enum.GetNames(typeof(PlaceType.Terrain)).Length + 1);

                PlaceType.Terrain terrainType = (PlaceType.Terrain)randTerrainInt;

                if (numTerrainUsed[terrainType] < 3)
                    numTerrainUsed[terrainType] += 1;
                else
                    numTerrainUsed.Remove(terrainType);
                
                //If we place more than we should allow
                if (numTerrainUsed.Count == 0)
                {
                    for (int j = 1; j <= System.Enum.GetNames(typeof(PlaceType.Terrain)).Length; ++j)
                    {
                        numTerrainUsed.Add((PlaceType.Terrain)j, 0);
                    }
                    print("Too many terrain pawns. Refilling usable pawns.");
                }

                for (int j = 0; j < pawnPrefabs.Count; ++j)
                {
                    if (pawnPrefabs[j].name == terrainType.ToString() + " PION")
                    {
                        List<GameObject> preserveNodeConnections = pawns[i].GetComponent<NodeConnections>().neighbourNodes;
                        int preserveId = pawns[i].GetComponent<Id>().id;

                        var newPawn = Instantiate(pawnPrefabs[j], pawns[i].transform.position, pawns[i].transform.rotation);

                        //Remove Terrain pawn from all it's neighbours. 
                        for (int k = 0; k < preserveNodeConnections.Count; ++k)
                        {
                            preserveNodeConnections[k].GetComponent<NodeConnections>().RemoveNeighbour(pawns[i]);
                        }

                        Destroy(pawns[i]);

                        pawns[i] = newPawn;
                        pawns[i].GetComponent<Id>().id = preserveId;

                        //Read all neighbours to the new pawn.
                        for (int k = 0; k < preserveNodeConnections.Count; ++k)
                        {
                            pawns[i].GetComponent<NodeConnections>().AddNeighbour(preserveNodeConnections[k]);
                        }
                    }
                }
            }
        }
    }
    
    public void RegenerateMap()
    {
        numTerrainUsed.Clear();
        for (int i = 1; i < System.Enum.GetNames(typeof(PlaceType.Terrain)).Length; ++i)
        {
            numTerrainUsed.Add((PlaceType.Terrain)i, 0);
        }

        for (int i = 0; i < pawns.Length; ++i)
        {
            if (pawns[i].GetComponent<PlaceType>().placeType == PlaceType.Place.TERRAIN)
            {
                int randTerrainInt = Random.Range(1, System.Enum.GetNames(typeof(PlaceType.Terrain)).Length + 1);
                while (!numTerrainUsed.ContainsKey((PlaceType.Terrain)randTerrainInt) && numTerrainUsed.Count > 0)
                    randTerrainInt = Random.Range(1, System.Enum.GetNames(typeof(PlaceType.Terrain)).Length + 1);

                PlaceType.Terrain terrainType = (PlaceType.Terrain)randTerrainInt;

                if (numTerrainUsed[terrainType] < 3)
                    numTerrainUsed[terrainType] += 1;
                else
                    numTerrainUsed.Remove(terrainType);

                //If we place more than we should allow
                if (numTerrainUsed.Count == 0)
                {
                    for (int j = 1; j <= System.Enum.GetNames(typeof(PlaceType.Terrain)).Length; ++j)
                    {
                        numTerrainUsed.Add((PlaceType.Terrain)j, 0);
                    }
                    print("Too many terrain pawns. Refilling usbale pawns.");
                }

                for (int j = 0; j < pawnPrefabs.Count; ++j)
                {
                    if (pawnPrefabs[j].name == terrainType.ToString() + " PION")
                    {
                        List<GameObject> preserveNodeConnections = pawns[i].GetComponent<NodeConnections>().neighbourNodes;
                        int preserveId = pawns[i].GetComponent<Id>().id;

                        var newPawn = Instantiate(pawnPrefabs[j], pawns[i].transform.position, pawns[i].transform.rotation);

                        //Remove Terrain pawn from all it's neighbours. 
                        for (int k = 0; k < preserveNodeConnections.Count; ++k)
                        {
                            preserveNodeConnections[k].GetComponent<NodeConnections>().RemoveNeighbour(pawns[i]);
                        }

                        Destroy(pawns[i]);

                        pawns[i] = newPawn;
                        pawns[i].GetComponent<Id>().id = preserveId;

                        //Read all neighbours to the new pawn.
                        for (int k = 0; k < preserveNodeConnections.Count; ++k)
                        {
                            pawns[i].GetComponent<NodeConnections>().AddNeighbour(preserveNodeConnections[k]);
                        }
                    }
                }
            }

            if (pawns[i].GetComponent<OnClick>().visited)
                pawns[i].GetComponent<OnClick>().visited = false; 

            if (pawns[i].GetComponent<PlaceType>().placeType == PlaceType.Place.DEPART)
                NodeConnections.activeNode = pawns[i];
        }
    }

    

    public void CreatePawn (PlaceType.Place placeType, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < pawnPrefabs.Count; ++i)
        {
            if (pawnPrefabs[i].name == placeType.ToString() + " PION")
            {
                if (placeType.Equals(PlaceType.Place.DEPART))
                {
                    NodeConnections.activeNode = Instantiate(pawnPrefabs[i], position, rotation); 
                }
                else {
                    Instantiate(pawnPrefabs[i], position, rotation);
                }
                return;
            }
        }
        print("Failed to find pawn type");
    }

    public void CreatePawn(PlaceType.Terrain terrainType, Vector3 position, Quaternion rotation, NodeConnections connections)
    {
        for (int i = 0; i < pawnPrefabs.Count; ++i)
        {
            if (pawnPrefabs[i].name == terrainType.ToString() + " PION")
            {
                var pawn = Instantiate(pawnPrefabs[i], position, rotation) as GameObject;
                return;
            }
        }
        print("Failed to find pawn terrain type");
    }
}