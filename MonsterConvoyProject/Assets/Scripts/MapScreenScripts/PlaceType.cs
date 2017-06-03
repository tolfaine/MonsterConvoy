using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceType : MonoBehaviour {

    public GameObject flag;
    private GameObject myFlag; 

    public enum Place {
        TERRAIN,
        DEPART,
        PORTAIL,
        DONJON,
        TAVERNE,
        CAMPEMENT
    }
    
    public enum Terrain
    {
        NULL,
        FORET,
        MONTAGNE,
        MARECAGE, 
        DESERT,
        NEIGE,
        PLAGE,
        GROTTE,
        PLAINE
    }

 
    public Place placeType = Place.TERRAIN;
    public Terrain terrainType = Terrain.NULL;

    public bool invasionStatus = false;

	// Use this for initialization
	void Start () {
        if (placeType.Equals(Place.DEPART))
            NodeConnections.activeNode = gameObject;
	}

    public void SetInvaded()
    {
        invasionStatus = true;

        flag.transform.position = transform.position + (Vector3.left * 3) + (Vector3.back);
        GameObject.Instantiate(flag, transform);
    }
}
