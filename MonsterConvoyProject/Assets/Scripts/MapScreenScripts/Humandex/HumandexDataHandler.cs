﻿using UnityEngine;
using UnityEngine.UI;

public class HumandexDataHandler : MonoBehaviour
{
    public GameObject humandexIcon;
    public GameObject newEntryNotification;
    public GameObject[] pageRibbons = new GameObject[4];

    public int pageNumber = 0;
    private const int numEntriesPerPage = 4;
    const int numEntries = 16;

    public GameObject[] humandexEntry = new GameObject[numEntriesPerPage];
    public Image[] humandexMutationIcon = new Image[4];
    public Text[] humandexMutationName = new Text[4];
    public Text[] humandexMutationDescription = new Text[4];

    bool[] viewed = new bool[numEntries]; // todo if !viewed && discovered {display notification}
    string[] imageName = new string[numEntries]; // Take the enum name parse it. Locate the image in teh correct postion of the thing

    void Start()
    {
        for (int i = pageNumber * numEntriesPerPage; i < TipsManager.Instance().tipsKnownByPlayer.Count; ++i)
        {
            humandexMutationIcon[i].sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + TipsManager.Instance().tipsKnownByPlayer[i].caracMonster.enumCaract.ToString());
            humandexMutationName[i].text = TipsManager.Instance().tipsKnownByPlayer[i].caracMonster.sName;
            humandexMutationDescription[i].text = CorrectTip(TipsManager.Instance().tipsKnownByPlayer[i]);
        }
    }

    public void TurnPageRight()
    {
        if (pageNumber < (int)(numEntries / numEntriesPerPage))
        {
            pageNumber++;
            UpdatePage();
        }
    }

    public void TurnPageLeft()
    {
        if (pageNumber > 0)
        {
            pageNumber--;
            UpdatePage();
        }
    }

    public void JumpToPage(int newPageNumber)
    {
        pageNumber = newPageNumber;
        UpdatePage();
    }

    private void UpdatePage()
    {

        for (int i = 0; i < pageRibbons.Length; ++i)
        {
            pageRibbons[i].SetActive(true);
        }

        pageRibbons[pageNumber].SetActive(false);

        int emptyBoxes = numEntriesPerPage; 
        for (int i = pageNumber*numEntriesPerPage; i < TipsManager.Instance().tipsKnownByPlayer.Count; ++i)
        {
            humandexMutationIcon[i].sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + TipsManager.Instance().tipsKnownByPlayer[i].caracMonster.enumCaract.ToString());
            humandexMutationName[i].text = TipsManager.Instance().tipsKnownByPlayer[i].caracMonster.sName;
            humandexMutationDescription[i].text = CorrectTip(TipsManager.Instance().tipsKnownByPlayer[i]);
            emptyBoxes--;
        }
        for (int i = numEntriesPerPage; i > 0; i--)
        {
        //    humandexEntry[i].SetActive(false);
        }
    }

    private string CorrectTip(Tip tip)
    {
        string text = "";
        switch (tip.caracMonster.enumCaract)
        {
            case CaractMonster._enumCaractMonster.AILES:
                text = "These won't make you fly but they";
                break;
            case CaractMonster._enumCaractMonster.CARAPACE:
                text = "She may sell sea shell, but this shell";
                break;
            case CaractMonster._enumCaractMonster.CHAPEAU:
                text = "A fancy top hat";
                break;
            case CaractMonster._enumCaractMonster.CORNES:
                text = "These horns won't make any sounds but they";
                break;
            case CaractMonster._enumCaractMonster.MOUSTACHES:
                text = "Not hipster intended, but this moustache";
                break;
            case CaractMonster._enumCaractMonster.SAC:
                text = "This handy backpack";
                break;
            case CaractMonster._enumCaractMonster.SOURCILS:
                text = "Fuzzy eyebrows";
                break;
            case CaractMonster._enumCaractMonster.TENTACULES:
                text = "A handful of tentacles";
                break;
        }

        if (tip.modroll == ModRoll.ATTACKH)
        {
            text += " can protect you from the attack of a human with ";
        }
        else if (tip.modroll == ModRoll.ATTACKM)
        {
            text  += " may distract the attack of pink thingies with ";
        }
        else if (tip.modroll == ModRoll.FEARM)
        {
            text = " will terrify any human with	";
        }
        else if (tip.modroll == ModRoll.TALKM)
        {
            text = " grants you crazy eloquence in the eyes of humanoids with	";
        }

        if (tip.caracHumain.type == CaracHumainType.Stuff)
        {
            if (tip.caracHumain == CaractHumainStuff.ARC)
            {
                text +=  "a bow.";
            }
            else if (tip.caracHumain == CaractHumainStuff.BATON)
            {
                text +=  "a staff.";
            }
            else if (tip.caracHumain == CaractHumainStuff.BOUCLIER)
            {
                text +=  "a shield.";
            }
            else if (tip.caracHumain == CaractHumainStuff.CAPE)
            {
                text +=  "a cape.";
            }
            else if (tip.caracHumain == CaractHumainStuff.HACHE)
            {
                text +=  "an axe.";
            }
            else if (tip.caracHumain == CaractHumainStuff.JUPE)
            {
                text +=  "white clothes.";
            }
            else if (tip.caracHumain == CaractHumainStuff.RICHE)
            {
                text +=  "a bag of gold.";
            }
            else if (tip.caracHumain == CaractHumainStuff.TOURISTE)
            {
                text +=  "a tourist map.";
            }

        }
        else
        {
            if (tip.caracHumain == CaractHumainCheveux.BLANC)
            {
                text += "grey hair.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.BLOND)
            {
                text +=  "magnificient blond hair, like I once had";
            }
            else if (tip.caracHumain == CaractHumainCheveux.BRUN)
            {
                text +=  "dark hair, like their soul.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.CASQUE)
            {
                text +=  "a helmet.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.CHATAIN)
            {
                text +=  "brown hair.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.CHAUVE)
            {
                text +=  "a shiny head ... and no hair, well a bald one.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.COLORE)
            {
                text +=  "blue hair, filthy punk.";
            }
            else if (tip.caracHumain == CaractHumainCheveux.ROUX)
            {
                text +=  "ginger hair.";
            }
        }

        return text;
    }
}