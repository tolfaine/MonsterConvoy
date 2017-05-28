using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Transform rootMonsters;

    // Use this for initialization
    void Start () {
        creaturePrefabManager = GameObject.FindGameObjectWithTag("CreaturePrefabManager").GetComponent<CreaturePrefabManager>();
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();

        foreach (Transform child in slots.transform)
        {
            Destroy(child.gameObject);
        }

        InitialiseRecruts();
        InitialiseSlots();
        FillSlots();
    }
	
    
	// Update is called once per frame
	void Update () {

        if (monsterToRecruteSelected != null)
        {
            if (monsterSelected.bIsImportant)
            {
                if(monsterToTradeSelected != null)
                {
                    int index = 0;
                    ReplaceSlot(monsterSelected, monsterToTradeSelected);
                    monsterToRecruteSelected.SetActive(false);

                    slotSelected = null;
                    monsterToTradeSelected = null;
                    monsterToRecruteSelected = null;
                }

            }else
            {
                AddSlot(monsterSelected, -1);
                monsterToRecruteSelected.SetActive(false);

                monsterToRecruteSelected = null;
            }
            

            

        }
		//If slot left
        //If monster selected est un impportant one
        // If tout est bon, flip monsters ou faire diparaitre monstre 

	}
    void InitialiseRecruts()
    {
        CreaturesData creatureData = GameObject.FindGameObjectWithTag("CreaturesData").GetComponent<CreaturesData>();

        for(int i = 0; i< nbRecrute; i++)
        {
            Monster monster = creatureData.GetRandomMonsterWithImportance(canFindImportant).GetMonster();
            InstantiateMonsterAtPosition(availablePosition[i].position, monster);

        }

    }
    void InstantiateMonsterAtPosition(Vector3 position, Monster monster)
    {
        GameObject model = creaturePrefabManager.GetMonster(monster.nID);

        GameObject monsterInstantiated = Instantiate(monsterPrefab, position, monsterPrefab.transform.localRotation) as GameObject;
        GameObject monsterModel = Instantiate(model, Vector3.zero, monsterPrefab.transform.localRotation) as GameObject;

        monsterModel.transform.parent = monsterInstantiated.transform;

        monsterInstantiated.transform.parent = rootMonsters;

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

    void ReplaceSlot(Monster monsterToAdd, Monster monsterToTrade)
    {
        int indexM = caravane.lFighters.IndexOf(monsterToTrade);

        Monster replacedMonster = caravane.lFighters[indexM];

        DeleteSlot(replacedMonster, indexM);

        Vector3 position = monsterToRecruteSelected.transform.position;
        InstantiateMonsterAtPosition(position, replacedMonster);

        AddSlot(monsterToAdd, indexM);







        // Get position du monster a remplacer 

    }

    void AddSlot(Monster monster, int index)
    {
        GameObject sl = Instantiate(slotPrefab, slotPrefab.transform.position, slotPrefab.transform.localRotation) as GameObject;
        sl.transform.parent = slots.transform;
        Slot scriptSlot = sl.GetComponent<Slot>();
        scriptSlot.monster = monster;

        sl.transform.SetSiblingIndex(index);

     //   scriptSlot.nIdCreature = monster.nID;

        if (index == -1)
        {
            caravane.lFighters.Add(monster);
        //    scriptSlot.nIndexInCaravane = caravane.lFighters.Count-1;
        }
        else
        {
            caravane.lFighters[index] = monster;
            //    scriptSlot.nIndexInCaravane = index;
        }

        scriptSlot.SetText(scriptSlot.monster.sName);
    }

    void DeleteSlot(Monster monster, int index)
    {
        slotSelected.Delete();
        caravane.lFighters[index] = null;
    }

}
