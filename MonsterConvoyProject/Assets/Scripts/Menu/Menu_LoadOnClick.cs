using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_LoadOnClick : MonoBehaviour {
    enum MenuAction {NoAction, NewGame, LoadGame, Credits, Quit};
    public void loadScene(int action) {
        switch (action) {
            case 0:
                SceneManager.LoadScene("Recrutement");
                break;
            case 1:
                SceneManager.LoadScene("Menu");
                break;
            case 2:
                //Load data before
                SceneManager.LoadScene("Credits"); //la carte
                break;
            case 3:
                confirmQuit();
                break;
            default:
                break;
        }
    }
    public void confirmQuit() {
        bool yesWasClicked = EditorUtility.DisplayDialog("Quit the game", "Are you sure to quit the game?", "Yes", "No");
        if(yesWasClicked)
            Application.Quit();
    }
}
