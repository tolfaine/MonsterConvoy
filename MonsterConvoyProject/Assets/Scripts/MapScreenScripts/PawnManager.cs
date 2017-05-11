using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour {

    static PawnManager instance;

    Dictionary<PlaceType.Terrain, int> numTerrainUsed = new Dictionary<PlaceType.Terrain, int>();

    public List<GameObject> pawnPrefabs = new List<GameObject>();

    void Start()
    {
        instance = this;
    }

    private void Awake()
    {
        //Assign terrain at runtime
        GameObject[] pawns = GameObject.FindGameObjectsWithTag("MapNode");

        for (int i = 1; i < System.Enum.GetNames(typeof(PlaceType.Terrain)).Length; ++i)
        {
            numTerrainUsed.Add((PlaceType.Terrain)i, 0);
        }

        for (int i = 0; i < pawns.Length; ++i)
        {
            if (pawns[i].GetComponent<PlaceType>().placeType == PlaceType.Place.TERRAIN && pawns[i].GetComponent<PlaceType>().terrainType == PlaceType.Terrain.NULL)
            {
                int randTerrainInt = Random.Range(1, System.Enum.GetNames(typeof(PlaceType.Terrain)).Length + 1);
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
                    print("Too many terrain pawns. Refilling usbale pawns.");
                }

                for (int j = 0; j < pawnPrefabs.Count; ++j)
                {
                    if (pawnPrefabs[j].name == terrainType.ToString() + " PION")
                    {
                        NodeConnections preserveConnections = pawns[i].GetComponent<NodeConnections>();
                        var newPawn = Instantiate(pawnPrefabs[j], pawns[i].transform.position, pawns[i].transform.rotation);
                        Destroy(pawns[i]);
                        pawns[i] = newPawn;
                        pawns[i].GetComponent<NodeConnections>().neighbourNodes = preserveConnections.neighbourNodes;
                    }
                }
            }
        }
    }

    public static PawnManager Instance()
    {
        return instance;
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

    //Cleanup. Hot fix
    private void Update()
    {
        AkSoundEngine.StopAll(); 
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


    //Cleanup Delete this
    public GameObject GetPawn(PlaceType.Place placeType)
    {
        for (int i = 0; i < pawnPrefabs.Count; ++i)
        {
            if (pawnPrefabs[i].name == placeType.ToString() + " PION")
                return pawnPrefabs[i];

        }
        return null;
    }

    //Cleanup Delete this
    public GameObject GetPawn(PlaceType.Terrain terrainType)
    {
        for (int i = 0; i < pawnPrefabs.Count; ++i)
        {
            if (pawnPrefabs[i].name == terrainType.ToString() + " PION")
                return pawnPrefabs[i];
        }

        return null; 
    }
}