using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using LitJson;

public enum MenuAction {
    NoAction = -1,
    NewGame = 0,
    LoadGame = 1,
    Credits = 2,
    Quit = 3
};

public class Menu_LoadOnClick : MonoBehaviour {
    //bool enable_loadGame = true;
    private Button target_Loadbtn;
    string sPath;
    JsonData jsFileSave;

    void Awake()
    {
        if (GameObject.Find("LoadGame Button")) { 
            target_Loadbtn = GameObject.Find("LoadGame Button").GetComponent<Button>();
            string filePath = Application.dataPath + "/Ressources/Save/saveFil.json";
            if (File.Exists(filePath))
            {
                sPath = File.ReadAllText(filePath);
                jsFileSave = JsonMapper.ToObject(sPath);
            }
            else {
                target_Loadbtn.interactable = false;
                //enable_loadGame = false;
                //target_Loadbtn.GetComponentInChildren<Image>().color = Color.grey;
            }
            int index_scene = Random.Range(22, 33);
            //SceneManager.LoadScene("DONJON", LoadSceneMode.Additive);
            SceneManager.LoadScene(index_scene, LoadSceneMode.Additive);
            //GameObject combatPrefab = GameObject.Find("CombatPrefab").GetComponent<GameObject>();
            //combatPrefab.SetActive(false);
        }
    }
    /*public void Update()
    {
        GameObject.Find("LoadGame Button").GetComponent<Button>().interactable = enable_loadGame;
    }*/

    public void loadScene(int action) {
        switch (action) {
            case 0:
                SceneManager.LoadScene("CAPITAL");
                break;
            case 1:
                //Load data before
                SceneManager.LoadScene("Menu");  //la carte
                break;
            case 2:
                SceneManager.LoadScene("Credits");
                break;
            case 3:
                confirmQuit();
                break;
            default:
                SceneManager.LoadScene("Main_Menu");
                break;
        }
    }
    public void confirmQuit() {
       // bool yesWasClicked = EditorUtility.DisplayDialog("Quit the game", "Are you sure to quit the game?", "Yes", "No");
      //  if(yesWasClicked)
            Application.Quit();
    }
}
