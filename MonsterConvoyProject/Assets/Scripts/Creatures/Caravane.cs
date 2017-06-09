using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caravane : MonoBehaviour {

    public static bool created = false;
    public List<Monster> lFighters = new List<Monster>(1);
    public int nNbCreatureInCaravane = 10;
    Scene currentScene;

    public bool bEnteringInvadedZone = false;




    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;

    }

    void AddNew()
    {
        lFighters.Add(new Monster());

        GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if (g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }

    }

    void Remove(int index)
    {
        lFighters.RemoveAt(index);

        GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if(g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }

            

    }

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);

        if (bEnteringInvadedZone)
        {

        }

    }
    
    void Start () {
      //  Monster monster;

        for (int i = 0; i < nNbCreatureInCaravane; i++){
        //    monster = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>().GetRandomFighter<Monster>(CreatureType.Monster);
        //    lFighters.Add(monster);
        }
	}

    public void CheckMonsterDead()
    {

        foreach (Monster monster in lFighters)
        {
            if (monster.IsDead())
            {
                lFighters.Remove(monster);
                GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
                if (g != null)
                {
                    g.GetComponent<CaravaneUI>().UpdateUI();
                }

            }
        }
    }
}
