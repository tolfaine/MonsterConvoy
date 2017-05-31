using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;

public class Save : MonoBehaviour {
    JsonData _tips;
    JsonData _mutations;
    int _wave;
    // Use this for initialization
    void Start () {
        save();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void save()
    {
        string filePath = Application.dataPath + "/Ressources/Save/saveFile.json";
        List<Tip> tipsToSave = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().tipsKnownByPlayer;
        List<MutationManager.MutationData> mutationsToSave = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>().allMutations;
        GameObject _invasionFile = GameObject.Find("Invasion");
        Invasion _invasion = _invasionFile.GetComponent<Invasion>();
        int _wave = _invasion.invasionSize;

        _tips = JsonMapper.ToJson(tipsToSave);
        _mutations = JsonMapper.ToJson(mutationsToSave);

        string tmp_file = "{ \"tips\": "+_tips.ToString() +", \"mutations\" :"+ _mutations.ToString()+", \"wave\":" + _wave +" }";
        
        if (!Directory.Exists(Application.dataPath + "/Ressources/Save"))
            Directory.CreateDirectory(Application.dataPath + "/Ressources/Save");

        if (File.Exists(filePath))
        {
            bool yesWasClicked = EditorUtility.DisplayDialog("Save the game", "Are you sure to replace your previous save?", "Yes", "No");
            if (yesWasClicked)
                File.WriteAllText(Application.dataPath + "/Ressources/Save/saveFile.json", tmp_file);
        }
        else {
            File.WriteAllText(Application.dataPath + "/Ressources/Save/saveFile.json", tmp_file);
        }
    }
}
