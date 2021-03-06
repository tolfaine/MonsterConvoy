﻿using UnityEngine;
using UnityEngine.UI;

public class HumandexDataHandler : MonoBehaviour
{
    public GameObject humandexIcon;
    public GameObject[] pageRibbons = new GameObject[4];

    public int pageNumber = 0;
    private const int numEntriesPerPage = 4;
    const int numEntries = 16;

    public GameObject[] humandexEntry = new GameObject[numEntriesPerPage];
    public Image[] humandexMutationIcon = new Image[4];
    public Text[] humandexMutationName = new Text[4];
    public Text[] humandexMutationDescription = new Text[4];

    bool[] viewed = new bool[numEntries];
    
    void Start()
    {
        bool newTip = false;
        for (int i = 0; i < TipsManager.Instance().tipsKnownByPlayer.Count; ++i)
        {
            if (!viewed[i])
            {
                newTip = true;
                humandexIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/newEntryIcon");
                JumpToPage((int)(i / numEntriesPerPage));
                break;
            }
        }
        if(!newTip)
            JumpToPage(0);
    }
    private void OnEnable()
    {
        Start();
    }
    public void JumpToPage(int newPageNumber)
    {
        pageNumber = newPageNumber;
        UpdatePage();
    }

    public void UpdatePage()
    {
        for (int i = 0; i < pageRibbons.Length; ++i)
        {
            if (TipsManager.Instance().tipsKnownByPlayer.Count > i * numEntriesPerPage)
                pageRibbons[i].SetActive(true);
            else
                pageRibbons[i].SetActive(false);
        }

        pageRibbons[pageNumber].SetActive(false);

        for (int i = 0; i < humandexEntry.Length; ++i)
        {
            if (TipsManager.Instance().tipsKnownByPlayer.Count > (pageNumber * numEntriesPerPage) + i)
            {
                humandexEntry[i].SetActive(true);
                humandexMutationIcon[i].sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/" + TipsManager.Instance().tipsKnownByPlayer[(pageNumber * numEntriesPerPage) + i].caracMonster.enumCaract.ToString());
                humandexMutationName[i].text = TipsManager.Instance().tipsKnownByPlayer[(pageNumber * numEntriesPerPage) + i].caracMonster.sName;
                humandexMutationDescription[i].text = CorrectTip(TipsManager.Instance().tipsKnownByPlayer[(pageNumber * numEntriesPerPage) + i]);
                viewed[(pageNumber * numEntriesPerPage) + i] = true;
            }
            else
            {
                humandexEntry[i].SetActive(false);
            }
        }


        bool newTip = false;
        for (int i = 0; i < TipsManager.Instance().tipsKnownByPlayer.Count; ++i)
        {
            if (!viewed[i])
            {
                humandexIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/newEntryIcon");
                newTip = true;
            }
            else if(!newTip)
            {
                humandexIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/HumandexIcons/humandexIcon");
            }
        }
    }

    private string CorrectTip(Tip tip)
    {
        string text = "";
        switch (tip.caracMonster.enumCaract)
        {
            case CaractMonster._enumCaractMonster.WINGS:
                text = "These won't make you fly but they";
                break;
            case CaractMonster._enumCaractMonster.SHELLS:
                text = "She may sell sea shells, but this shell";
                break;
            case CaractMonster._enumCaractMonster.HATS:
                text = "A fancy top hat";
                break;
            case CaractMonster._enumCaractMonster.HORNS:
                text = "These horns won't make any sounds but they";
                break;
            case CaractMonster._enumCaractMonster.MOUSTACHES:
                text = "Not hipster intended, but this moustache";
                break;
            case CaractMonster._enumCaractMonster.BACKPACKS:
                text = "This handy backpack";
                break;
            case CaractMonster._enumCaractMonster.EYELASHES:
                text = "Fuzzy eyebrows";
                break;
            case CaractMonster._enumCaractMonster.TENTACLES:
                text = "A handful of tentacles";
                break;
            case CaractMonster._enumCaractMonster.NONE:
                text = "Having nothing";
                break;
            default:
                text = "ERROR: " + tip.caracMonster.enumCaract.ToString();
                break;
        }

        if (tip.modroll == ModRoll.ATTACKDMG)
        {
            text += " can protect you from the attack of a human with ";
        }
        else if (tip.modroll == ModRoll.ATTACKH)
        {
            text  += " may distract the attack of pink thingies with ";
        }
        else if (tip.modroll == ModRoll.FEARM)
        {
            text += " will terrify any human with ";
        }
        else if (tip.modroll == ModRoll.TALKM)
        {
            text += " grants you crazy eloquence in the eyes of humanoids with	";
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