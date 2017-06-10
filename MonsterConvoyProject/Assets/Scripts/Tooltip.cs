using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    Caravane caravane;
    public GameObject mutationImage;
    public Text monsterName; 
    public int caravaneIndex = 0; 

    void Awake()
    {
        caravane = GameObject.FindGameObjectWithTag("Caravane").GetComponent<Caravane>();
    }

    void OnEnable()
    {
        Monster monster = caravane.lFighters[caravaneIndex];
        gameObject.GetComponentInChildren<Text>().text = monster.sName;
        
        mutationImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + MutationManager.Instance().GetMutationWithId(monster.nID).sName); 
    }

}
