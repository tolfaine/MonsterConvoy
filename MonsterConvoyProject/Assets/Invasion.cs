using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invasion : MonoBehaviour {

    Vector3 invasionOrigin; //The location of the portal 
    int invasionSize = 0; //Multiplier which increases the invasion radius on every turn
    int invasionRadius = 50; //The initial radius of the invasion.
    float lerpSpeed = 0.05f; //The speed which the invasion grows at the start of each turn (Does not impact the size of the invasion).

    void Start ()
    {
        //Set the invasion origin to the position of the portal. 
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("MapNode");
        for (int i = 0; i < nodes.Length; ++i)
        {
            if (nodes[i].GetComponent<PlaceType>().placeType == PlaceType.Place.PORTAIL)
                invasionOrigin = nodes[i].transform.position;
            invasionOrigin.y = transform.position.y; //We don't want to change the Y position. I set this manually.
            transform.position = invasionOrigin;
        }

        Vector3 initialScale = new Vector3(invasionRadius, invasionRadius);
        transform.localScale = initialScale; 
    }
    
    private void OnEnable()
    {
        IncreaseInvasionArea();
    }
    private void Update()
    {
        Vector3 newScale = new Vector3(invasionRadius, invasionRadius) * invasionSize;

        newScale.x = Mathf.Lerp(newScale.x, transform.localScale.x, (1 - lerpSpeed));
        newScale.y = Mathf.Lerp(newScale.y, transform.localScale.y, (1 - lerpSpeed));
        transform.localScale = newScale;
    }

    //Set nodes which collide with invasion to invaded. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MapNode"))
        {
            other.GetComponent<PlaceType>().invasionStatus = true;
        }
    }

    private void OnCollisionStay(Collision collision) // Does this only get called when there's a physics step. 
    {
        if (collision.collider.CompareTag("MapNode"))
        {
            collision.collider.GetComponent<PlaceType>().invasionStatus = true;
        }
    }

    public void IncreaseInvasionArea()
    {
        invasionSize++;
    }
}
