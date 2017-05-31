using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceType : MonoBehaviour {

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

    void setInvaded()
    {
        invasionStatus = true; 
    }
}
