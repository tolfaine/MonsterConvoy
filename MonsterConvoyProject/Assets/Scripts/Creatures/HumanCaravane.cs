using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCaravane : MonoBehaviour {

    public List<Human> lFighters = new List<Human>();
    public int nNbCreatureInCaravane = 10;

    private void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {

    }
}
