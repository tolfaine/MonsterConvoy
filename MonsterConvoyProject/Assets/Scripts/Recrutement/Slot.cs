 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //  public int nIndexInCaravane;
    //   public int nIdCreature;
    private RecrutementManager recrutementManager;

    public GameObject slotUI;
    public Monster monster;
    public Text monsterText;

    public bool bMouseClicking = false;
    public bool bClickProcessed = false;

    public ImageFighterManager imageManager;


    // Use this for initialization
    void Start () {
        slotUI = gameObject;
        recrutementManager = GameObject.FindGameObjectWithTag("RecrutementManager").GetComponent<RecrutementManager>();
        imageManager.UpdateImage(monster.nID);
    }

    private void Update()
    {
        if (bMouseClicking && !bClickProcessed)
        {
            bClickProcessed = true;
            recrutementManager.SlotSelected(this);
            //  gameObject.GetComponentInChildren<Renderer>().material.color = mouseClickedColor;
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void SetText(string s)
    {
        monsterText.text = s;
    }

    /*
    public void mouseOver()
    {
        bMouseOver = true;
    }

    public void mouseExit()
    {
        bMouseOver = false;
    }
    */
    public void mouseClick()
    {
        bMouseClicking = true;

    }

    public void mouseUp()
    {
        bMouseClicking = false;
        bClickProcessed = false;
    }
}
