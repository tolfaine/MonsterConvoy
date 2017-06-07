using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMutation : MonoBehaviour {

    public CustomTalk customTalk;
    public TextAsset ScriptText;

    public int iteration = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterMutation()
    {
        if (iteration == 0)
        {
            customTalk.NewTalkScripted(ScriptText, 2, 2);
          //  AkSoundEngine.PostEvent("Play_TutoMage", gameObject);
        }


        iteration++;
    }
}
