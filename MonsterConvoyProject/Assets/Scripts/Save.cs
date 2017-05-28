using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;

public class Save : MonoBehaviour {
    JsonData _tips;
    JsonData _mutations;
    JsonData _wave;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void save()
    {
        string filePath = Application.dataPath + "/Ressources/Save/saveFile.json";
        List<Tip> tipsToSave = GameObject.FindGameObjectWithTag("TipManager").GetComponent<TipsManager>().tipsKnownByPlayer;
        List<MutationManager.MutationData> mutationsToSave = GameObject.FindGameObjectWithTag("MutationManager").GetComponent<MutationManager>().allMutations;

        _tips = JsonMapper.ToJson(tipsToSave);
        _mutations = JsonMapper.ToJson(mutationsToSave);
        //_wave = JsonMapper.ToJson(tipsToSave);

        string tmp_file = _tips.ToString() + _mutations.ToString() + _wave.ToString();
        
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
