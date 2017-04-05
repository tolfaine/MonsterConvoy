using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCaravane : MonoBehaviour {

    public List<Monster> lFighters = new List<Monster>();
    public int nNbCreatureInCaravane = 10;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        //  Monster monster;

        for (int i = 0; i < nNbCreatureInCaravane; i++)
        {
            //    monster = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetRandomFighter<Monster>(CreatureType.Monster);
            //    lFighters.Add(monster);
        }
    }
}
