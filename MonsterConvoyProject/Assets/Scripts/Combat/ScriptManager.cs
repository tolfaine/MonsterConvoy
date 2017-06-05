using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


[System.Serializable]
public class ScriptTurnInfo
{
    public int indexLineStart;
    public int indexLineEnd;

    public int ActionCode;
    public ActionType actionType;
    public float fRoll;


    public ScriptTurnInfo() { }
}

[System.Serializable]
public class ScriptOrder
{
    public int nId;
    public int nRoll;

    public ScriptOrder() { }
}


public class ScriptManager : MonoBehaviour {

    public List<ScriptTurnInfo> lTurnInfo = new List<ScriptTurnInfo>();
    public List<ScriptOrder> lOrder = new List<ScriptOrder>();

    public ScriptTurnInfo currentTurn;

    public RPGTalk rpgTalk;


    public int indexLine = 1;


    string sPath;
    JsonData jsScript;

    private void Awake()
    {
        sPath = File.ReadAllText(Application.dataPath + "/Ressources/Script_Proto.json");
        jsScript = JsonMapper.ToObject(sPath);
        LoadScript();

    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadScript()
    {
        

        string categorie = "Draft";
        JsonData turn;

        for (int i = 0; i < jsScript[categorie].Count; i++)
        {
            turn = jsScript[categorie][i];

            ScriptTurnInfo turnInfo = new ScriptTurnInfo();
            turnInfo.indexLineStart = int.Parse(turn["indexLineStart"].ToString());

            string action = turn["action"].ToString();

            if (action == ActionType.ATTACK.sName)
                turnInfo.actionType = ActionType.ATTACK;
            if (action == ActionType.FEAR.sName)
                turnInfo.actionType = ActionType.FEAR;
            if (action == ActionType.TALK.sName)
                turnInfo.actionType = ActionType.TALK;

            turnInfo.fRoll = float.Parse(turn["result"].ToString());

            lTurnInfo.Add(turnInfo);
        }


        categorie = "Fighters";
        JsonData order;

        for (int i = 0; i < jsScript[categorie].Count; i++)
        {
            order = jsScript[categorie][i];

            ScriptOrder scriptOrder = new ScriptOrder();

            scriptOrder.nId = int.Parse(order["id"].ToString());
            scriptOrder.nRoll = int.Parse(order["initiative"].ToString());

            lOrder.Add(scriptOrder);
        }


    }

    public void NextTurn()
    {
        if (indexLine < lTurnInfo.Count+1)
        {
            currentTurn = lTurnInfo[indexLine - 1];
            indexLine++;
        }else
        {
            currentTurn = null;
        }

    }
}
