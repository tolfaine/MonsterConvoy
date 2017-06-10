using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecrutementManager : MonoBehaviour {

    public int nbRecrute;
    public bool canFindImportant;

    private CreaturePrefabManager creaturePrefabManager;
    public GameObject monsterPrefab;

    public GameObject slots;
    public Caravane caravane;
    public GameObject slotPrefab;

    private bool teamSelection;

    private GameObject monsterToRecruteSelected = null;
    private Monster monsterSelected;
    private Monster monsterToTradeSelected = null;
    private Slot slotSelected;

    // list de positions prise
    // list positions libres
    // list de monstres a recruter

    public List<Transform> availablePosition = new List<Transform>();
    public List<Transform> filledPosition = new List<Transform>();

    public List<GameObject> allMonstersToRecrute = new List<GameObject>();

    public bool isAtCapital;

    public Transform rootMonsters;
    Scene currentScene;

    private int nbCreatureToRecrute = 1;
    private int nbrecruted = 0;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        currentScene = scene;
        /*
        if(currentScene.name == "CAPITAL" && isAtCapital)
        {
            caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
            caravane.lFighters = new List<Monster>();

        }*/
    }

    private void Awake()
    {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
        if (isAtCapital)
        {

            caravane.lFighters = new List<Monster>();
        }
        
    }
    // Use this for initialization
    void Start () {

        AkSoundEngine.PostEvent("DEPART", gameObject);

        creaturePrefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();


        foreach (Transform child in slots.transform)
        {
            Destroy(child.gameObject);
        }
        

        InitialiseRecruts();

        
        if(isAtCapital)
            InitialiseSlots();
        FillSlots();
    }
	
    
	// Update is called once per frame
	void Update () {

        if (monsterToRecruteSelected != null)
        {
            if (monsterSelected.bIsImportant)
            {
                if(monsterToTradeSelected != null && monsterToTradeSelected.nID != 0)
                {
                    if(nbrecruted <nbCreatureToRecrute)
                    {
                        int index = 0;
                        ReplaceSlot(monsterSelected, monsterToTradeSelected);
                        monsterToRecruteSelected.SetActive(false);

                        slotSelected = null;
                        monsterToTradeSelected = null;
                        monsterToRecruteSelected = null;
                        nbrecruted++;
                    }
                    
                }

                if (isAtCapital)
                {
                    AddSlot(monsterSelected, -1);
                    monsterToRecruteSelected.SetActive(false);

                    availablePosition.Add(monsterToRecruteSelected.transform.parent);
                    filledPosition.Remove(monsterToRecruteSelected.transform.parent);


                    monsterToRecruteSelected = null;
                }

            }else
            {
                if(!isAtCapital || (isAtCapital && caravane.lFighters.Count <= 3))
                AddSlot(monsterSelected, -1);
                monsterToRecruteSelected.SetActive(false);

                availablePosition.Add(monsterToRecruteSelected.transform.parent);
               filledPosition.Remove(monsterToRecruteSelected.transform.parent);


                monsterToRecruteSelected = null;
            }
        }

        if(monsterToTradeSelected != null && isAtCapital || (!isAtCapital && !monsterSelected.bIsImportant && nbrecruted ==1))
        {
            RemoveMonster(monsterToTradeSelected);

            monsterToTradeSelected = null;
            nbrecruted--;
        }
		//If slot left
        //If monster selected est un impportant one
        // If tout est bon, flip monsters ou faire diparaitre monstre 

	}
    void InitialiseRecruts()
    {
        CreaturesData creatureData = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>();

        List<MonsterData> lData = creatureData.GetAllMonsterImportance(canFindImportant);

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
        ProtoScript protoScript = null;

        for (int i = 0; i< nbRecrute; i++)
        {
            Monster monster = new Monster();




            if (g != null)
            {
                protoScript = g.GetComponent<ProtoScript>();
            }

            if (protoScript != null && isAtCapital && protoScript.selectin.iteration == 1)
            {
                monster = lData[i].GetMonster();
            }else if (isAtCapital)
            {
                Monster m1 = creatureData.GetRandomMonsterWithImportance(true).GetMonster();
                Monster m2= creatureData.GetRandomMonsterWithImportance(false).GetMonster();

                float rand = Random.Range(0f, 1f);
                if (rand < 0.5f)
                    monster = m1;
                else
                    monster = m2;

               // 
            }else
            {

                monster = creatureData.GetRandomMonsterWithImportance(canFindImportant).GetMonster();
            }

            InstantiateMonsterAtPosition(availablePosition[0], monster);

            filledPosition.Add(availablePosition[0]);

            availablePosition.RemoveAt(0);

        }

    }
    void InstantiateMonsterAtPosition(Transform trans, Monster monster)
    {
        GameObject model = creaturePrefabManager.GetMonster(monster.nID);

        GameObject monsterInstantiated = Instantiate(monsterPrefab, Vector3.zero, monsterPrefab.transform.localRotation) as GameObject;
        GameObject monsterModel = Instantiate(model, Vector3.zero, monsterPrefab.transform.localRotation) as GameObject;

        monsterInstantiated.transform.parent = trans;
        monsterInstantiated.transform.localPosition = Vector3.zero;

        monsterModel.transform.parent = monsterInstantiated.transform;

       // monsterInstantiated.transform.parent = rootMonsters;

        monsterInstantiated.transform.localScale = new Vector3(1f, 1f, 1f);

        monsterModel.transform.localPosition = Vector3.zero;

        MouseOverRecrute scripMouseOver = monsterModel.transform.GetChild(0).gameObject.AddComponent<MouseOverRecrute>();

        monsterModel.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

        scripMouseOver.fightherObj = monsterInstantiated;
        scripMouseOver.fighterUI = monsterInstantiated.GetComponent<FighterUI>();
        scripMouseOver.fighterUI.fighter = monster;



        // Get Model monster of type id
        // Set script monster
    }

    public void MonsterSelected(GameObject monster)
    {
        monsterToRecruteSelected = monster;
        monsterSelected = new Monster(monsterToRecruteSelected.GetComponent<FighterUI>().fighter);
    }

    public void SlotSelected(Slot slot)
    {
        monsterToTradeSelected = slot.monster;
        slotSelected = slot;
    }

    void InitialiseSlots()
    {
        int i = 0;
        foreach(Monster monster in caravane.lFighters)
        {
            AddSlot(monster, i);
            i++;
        }
    }

    void FillSlots()
    {

    }

    void RemoveMonster(Monster monsterToTrade)
    {
        int indexM = caravane.lFighters.IndexOf(monsterToTrade);

        Monster replacedMonster = caravane.lFighters[indexM];

        DeleteSlot(replacedMonster, indexM);


        Vector3 position = availablePosition[0].position;  /////
        InstantiateMonsterAtPosition(availablePosition[0], monsterToTrade);

        filledPosition.Add(availablePosition[0]);
        availablePosition.RemoveAt(0);


        GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if (g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }



    }
    void ReplaceSlot(Monster monsterToAdd, Monster monsterToTrade)
    {
        int indexM = caravane.lFighters.IndexOf(monsterToTrade);

        Monster replacedMonster = caravane.lFighters[indexM];

        caravane.lFighters[indexM] = null;

        caravane.lFighters.RemoveAt(indexM);

        //DeleteSlot(replacedMonster, indexM);

        //Vector3 position = monsterToRecruteSelected.transform.position;
        InstantiateMonsterAtPosition(monsterToRecruteSelected.transform.parent, replacedMonster);

        caravane.lFighters.Insert(indexM, monsterToAdd);

        GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if (g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }

        // AddSlot(monsterToAdd, indexM);
    }

    void AddSlot(Monster monster, int index)
    {
        GameObject sl = Instantiate(slotPrefab, slotPrefab.transform.position, slotPrefab.transform.localRotation) as GameObject;
        sl.transform.parent = slots.transform;
        Slot scriptSlot = sl.GetComponent<Slot>();
        scriptSlot.monster = monster;

        sl.transform.SetSiblingIndex(index);

        GameObject g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if (g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }
        //   scriptSlot.nIdCreature = monster.nID;

        if (index == -1)
        {
            caravane.lFighters.Add(monster);
        //    scriptSlot.nIndexInCaravane = caravane.lFighters.Count-1;
        }
        else
        {
            caravane.lFighters.Insert(index,monster);
            //    scriptSlot.nIndexInCaravane = index;
        }

        scriptSlot.SetText(scriptSlot.monster.sName);



        g = GameObject.FindGameObjectWithTag("CaravaneUI");
        if (g != null)
        {
            g.GetComponent<CaravaneUI>().UpdateUI();
        }

    }

    void DeleteSlot(Monster monster, int index)
    {
        slotSelected.Delete();
        caravane.lFighters[index] = null;
        caravane.lFighters.RemoveAt(index);
    }

}
