using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanDex : MonoBehaviour {

    public TipsManager tipManager;
    public Tip lastTip;
    public bool tipSet = false;

    public GameObject allIcone;

    public Text titre;
    public Text text;

	// Use this for initialization
	void Start () {
        GameObject g = GameObject.FindGameObjectWithTag("TipManager");

        if(g != null)
        {
           tipManager = g.GetComponent<TipsManager>();

        }

    }
	
	// Update is called once per frame
	void Update () {


        lastTip = tipManager.lastRevealedTip;

        if (lastTip != null)
        {
            tipSet = true;
            foreach (Transform trans in allIcone.transform)
            {

                bookMutation bm = trans.gameObject.GetComponent<bookMutation>();
                if (bm.caractM == lastTip.caracMonster.enumCaract)
                    allIcone.GetComponent<Image>().sprite = trans.gameObject.GetComponent<SpriteRenderer>().sprite;
            }
            titre.text = "";
            titre.text = lastTip.caracMonster.enumCaract.ToString();

            string mutationS = "";

            switch (lastTip.caracMonster.enumCaract)
            {
                case CaractMonster. _enumCaractMonster.AILES:
                    mutationS = "These won't make you fly but they";
                    break;
                case CaractMonster._enumCaractMonster.CARAPACE:
                    mutationS = "She may sell sea shell, but this shell";
                    break;
                case CaractMonster._enumCaractMonster.CHAPEAU:
                    mutationS = "A fancy top hat";
                    break;
                case CaractMonster._enumCaractMonster.CORNES:
                    mutationS = "These horns won't make any sounds but they";
                    break;
                case CaractMonster._enumCaractMonster.MOUSTACHES:
                    mutationS = "Not hipster intended, but this moustache";
                    break;
                case CaractMonster._enumCaractMonster.SAC:
                    mutationS = "This handy backpack";
                    break;
                case CaractMonster._enumCaractMonster.SOURCILS:
                    mutationS = "Fuzzy eyebrows";
                    break;
                case CaractMonster._enumCaractMonster.TENTACULES:
                    mutationS = "A handful of tentacles";
                    break;
            }
            text.text = "";
            text.text += mutationS;

            string effetS = "";

            if (lastTip.modroll == ModRoll.ATTACKH)
            {
                effetS = " can protect you from the attack of a human with ";
            }
            else if (lastTip.modroll == ModRoll.ATTACKM)
            {
                effetS = " may distract the attack of pink thingies with ";
            }
            else if (lastTip.modroll == ModRoll.FEARM)
            {
                effetS = " will terrify any human with	";
            }
            else if (lastTip.modroll == ModRoll.TALKM)
            {
                effetS = " grants you crazy eloquence in the eyes of humanoids with	";
            }

            text.text += effetS;

            string humanS = "";

            if (lastTip.caracHumain.type == CaracHumainType.Stuff)
            {
                if (lastTip.caracHumain == CaractHumainStuff.ARC)
                {
                    humanS = "a bow.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.BATON)
                {
                    humanS = "a staff.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.BOUCLIER)
                {
                    humanS = "a shield.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.CAPE)
                {
                    humanS = "a cape.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.HACHE)
                {
                    humanS = "an axe.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.JUPE)
                {
                    humanS = "white clothes.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.RICHE)
                {
                    humanS = "a gold bag.";
                }
                else if (lastTip.caracHumain == CaractHumainStuff.TOURISTE)
                {
                    humanS = "a tourist map.";
                }

            }
            else
            {
                if (lastTip.caracHumain == CaractHumainCheveux.BLANC)
                {
                    humanS = "grey hair.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.BLOND)
                {
                    humanS = "magnificient blond hair, like I once had";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.BRUN)
                {
                    humanS = "dark hair, like their soul.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.CASQUE)
                {
                    humanS = "a helmet.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.CHATAIN)
                {
                    humanS = "brown hair.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.CHAUVE)
                {
                    humanS = "a shiny head ... and no hair, well a bald one.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.COLORE)
                {
                    humanS = "blue hair, filthy punk.";
                }
                else if (lastTip.caracHumain == CaractHumainCheveux.ROUX)
                {
                    humanS = "ginger hair.";
                }
            }

            text.text += humanS;

        }
    }
}
