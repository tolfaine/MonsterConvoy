﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedSelection : MonoBehaviour {

    public CustomTalk customTalk;
    public TextAsset ScriptText;

    public int iteration = 0;

    // Use this for initialization
    void Start () {
       // Invoke("EnterSelection", 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterSelection()
    {
        if (iteration == 0)
        {
            customTalk.NewTalkScripted(ScriptText, 2, 2);
        }
        if (iteration == 1)
        {
            customTalk.NewTalkScripted(ScriptText, 2, 2);
        }
        iteration++;
    }
}
