using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManagerUI : MonoBehaviour {

    public CombatManager combatManager;

   // private TextMesh textMesh;
    public Text text;

    public string sDisplayed;

    public GameObject dialogueHumanObj;
    public GameObject dialogueObj;
    public GameObject mageTalk;

    public RPGTalk rpgTalk;

    public bool bHumanFearTriggred = false;

    public GameObject actionWheel;

    public GameObject humanAnchor;

    Scene currentScene;

    bool combatEnd = false;
    // Action , target, fighter, Type 


    // Use this for initialization
    void Start () {
        //text = GetComponent<Text>();
       // mageTalk = null;
     //   mageTalk = GameObject.FindGameObjectWithTag("MageTalk");
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
      //  mageTalk = GameObject.FindGameObjectWithTag("MageTalk");
    }

    // Update is called once per frame
    void Update () {
        if(mageTalk == null)
            mageTalk = GameObject.FindGameObjectWithTag("MageTalk");
        CheckHumanFear();
        CheckCombatManager();
        text.text = sDisplayed;

        if(combatManager.fighterMouvementManager.bIsAtFightPosition && !combatManager.bActionInProgress && combatManager.currentGroupLogic.GetLogicType() == LogicType.Player 
            && (!combatManager.bActionChoosed || (combatManager.bActionChoosed && combatManager.bActionRequireTarget && !combatManager.bTargetChoosed)))
        {
            actionWheel.SetActive(true);
        }else
        {
            foreach (Transform child in actionWheel.transform)
            {
                MouseOverAction mouse = child.GetComponent<MouseOverAction>();
                if (mouse != null)
                {
                    mouse.bMouseOver = false;
                    mouse.bMouseClicking = false;
                }
            }

            actionWheel.SetActive(false);
        }
    }

    public bool DialogueInProgress()
    {
        if ((dialogueHumanObj != null && dialogueHumanObj.activeSelf) || (dialogueObj!= null && dialogueObj.activeSelf) || (mageTalk != null && mageTalk.activeSelf))
            return true;

        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");

        /*
        if (g != null)
        {
            ProtoScript ps = g.GetComponent<ProtoScript>();
            if(ps.combat.needToTalk)
                return true;
        }
        */
            return false;
    }

    void CheckHumanFear()
    {
        if(combatManager.combatEndType== CombatManager.CombatEndType.HumansFeared && !bHumanFearTriggred
            && !combatManager.fighterMouvementManager.bIsAtFightPosition)
        {
            /*
            bHumanFearTriggred = true;
            rpgTalk.lineToStart = 9;
            rpgTalk.lineToBreak = 9;
            rpgTalk.follow = humanAnchor;
            rpgTalk.NewTalk();*/
        }
    }
    void CheckCombatManager()
    {
        sDisplayed = "";

        if (!combatManager.bCombatEnded)
        {

            if (combatManager.currentGroupLogic != null)
            {
                if (combatManager.currentGroupLogic.GetLogicType() == LogicType.IA)
                    sDisplayed += "[IA TURN]\n";
                else
                    sDisplayed += "[PLAYER TURN]\n";
            }

            if (combatManager.currentFighter != null)
                sDisplayed += "fighter : " + combatManager.currentFighter.sName + "\n";

            if (combatManager.actionChoosed != null)
                sDisplayed += "action : " + combatManager.actionChoosed.sName + "\n";
            if (combatManager.actionChoosed == null)
                sDisplayed += "action :\n";


            if (combatManager.actionChoosed != null && combatManager.actionChoosed.GetTargetType() == ActionType.ActionTargetType.AllTarget)
                sDisplayed += "on all enemies \n";

            if (combatManager.actionChoosed != null && combatManager.actionChoosed.GetTargetType() == ActionType.ActionTargetType.OneTarget)
            {
                if(combatManager.targetChoosed != null)
                    sDisplayed += "target : " + combatManager.targetChoosed.sName + "\n";
                else
                    sDisplayed += "target :\n";
            }

        }else
        {


            if (combatManager.combatEndType == CombatManager.CombatEndType.HumansConvinced)
                sDisplayed += "Monsters Won , \n the humans are \n convinced :)";
            else if(combatManager.combatEndType == CombatManager.CombatEndType.HumansDead)
                sDisplayed += "Monsters Won ,\n  the humans are \n dead :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.HumansFeared)
                sDisplayed += "Monsters Won ,\n the humans are \n feared :)";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.MonstersDead)
                sDisplayed += "Humans Won , \nthe monsters are \n dead :(";
            else if (combatManager.combatEndType == CombatManager.CombatEndType.MonsterEscape)
                sDisplayed += "Monsters escape :3";

            if (combatManager.discoveredTip !=null && combatManager.discoveredTip.caracMonster.enumCaract != CaractMonster._enumCaractMonster.NONE)
            {
                Tip t = combatManager.discoveredTip;

                string sHuman = "";

                if(t.caracHumain.type == CaracHumainType.Cheveux)
                {
                    sHuman = ((CaractHumainCheveux)t.caracHumain).enumCaract.ToString();
                }else
                {
                    sHuman = ((CaractHumainStuff)t.caracHumain).enumCaract.ToString();
                }

                string sMonster = t.caracMonster.enumCaract.ToString();
                string nameMod = t.modroll.sName;

                // sDisplayed += "\n" + sHuman + "  " + nameMod + " " + sMonster;

                sDisplayed += "\n You've learn a new Tip";
            }


            if (!DialogueInProgress())
            {
                bool isScripted = false;

                if (!combatEnd)
                {
                    GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
                    ProtoScript ps = null;

                    if (g != null)
                    {
                        ps = g.GetComponent<ProtoScript>();
                        ps.combat.EndCombat();
                        isScripted = true;
                    }
                    combatEnd = true;
                }else
                {
                    if (!DialogueInProgress())
                    {
                        if (isScripted)
                            Invoke("BackToMenu", 1);
                        else
                            Invoke("BackToMenu", 4);
                    }
                }


            }


        }
    }

    void BackToMenu()
    {
        string s = "CARTE" ;

        SceneManager.UnloadScene(currentScene);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(s));
        for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
        {
            GameObject go = SceneManager.GetActiveScene().GetRootGameObjects()[i];
            if(go.tag != "Spotligth")
                SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(true);
        }


        GameObject g = GameObject.FindGameObjectWithTag("ProtoManager");
        ProtoScript ps = null;

        if (g != null)
        {
            ps = g.GetComponent<ProtoScript>();
            ps.map.EnterMap();
            ps.currentIndex++;
        }
    }

}
 