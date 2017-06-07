using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedMap : MonoBehaviour {
    public CustomTalk customTalk;

    public bool hasToCloseDex = false;

    public TextAsset ScriptText;

    public bool mutTrigger = false;
    public bool switchToMutation = false;

    public int iteration = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (switchToMutation && !mutTrigger)
        {
            if (!customTalk.textUI.IsActive())
            {
                mutTrigger = true;

                string sceneType = "mutations";

                //Change scene on node click
                for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
                {
                    //TODO preserve node types in file and reload from there. 
                    SceneManager.GetActiveScene().GetRootGameObjects()[i].SetActive(false);
                }
                SceneManager.LoadSceneAsync(sceneType, LoadSceneMode.Additive);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneType));

            }
        }
    }

    public bool talkInProgress()
    {
        if (customTalk.textUI.IsActive())
            return true;
        return false;
    }

    public void EnterMap()
    {
        if (iteration == 0)
        {
            customTalk.NewTalkScripted(ScriptText, 2, 2);
           // AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }
        if (iteration == 1)
        {
            customTalk.NewTalkScripted(ScriptText, 5, 5);
            //AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }
        if (iteration == 2)
        {
            customTalk.NewTalkScripted(ScriptText, 11, 11);
           // AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }
        if (iteration == 3)
        {
            customTalk.NewTalkScripted(ScriptText, 14, 14);
          //  AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }
        if (iteration == 5)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }


        iteration++;
    }

    public void OpenHumanDex()
    {

    }

    public void CloseHumanDex()
    {
        if (iteration == 2)
        {
          //  AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
            customTalk.NewTalkScripted(ScriptText, 7, 8);
        }
    }

    public void ToMutation()
    {
        customTalk.NewTalkScripted(ScriptText,11,11);
        //AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        switchToMutation = true;


    }


}
