using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFighter : MonoBehaviour {

    public int idMonster;
    public Sprite sprite;

    // Use this for initialization
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
    
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
