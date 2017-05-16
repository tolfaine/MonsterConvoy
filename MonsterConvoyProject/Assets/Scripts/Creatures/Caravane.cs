using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caravane : MonoBehaviour {

    public List<Monster> lFighters = new List<Monster>(1);
    public int nNbCreatureInCaravane = 10;


    void AddNew()
    {
        lFighters.Add(new Monster());
    }

    void Remove(int index)
    {
        lFighters.RemoveAt(index);
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    void Start () {
      //  Monster monster;

        for (int i = 0; i < nNbCreatureInCaravane; i++){
        //    monster = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetRandomFighter<Monster>(CreatureType.Monster);
        //    lFighters.Add(monster);
        }
	}
}
