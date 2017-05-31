using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMap : MonoBehaviour {
    public CustomTalk customTalk;

    public bool hasToCloseDex = false;

    public TextAsset ScriptText;

    public int iteration = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnterMap()
    {
        if(iteration == 0)
            customTalk.NewTalkScripted(ScriptText, 2, 2);
        if (iteration == 1)
            customTalk.NewTalkScripted(ScriptText, 5, 5);
        if (iteration == 2)
            customTalk.NewTalkScripted(ScriptText, 11,11);
        if (iteration == 3)
            customTalk.NewTalkScripted(ScriptText, 14, 14);

        iteration++;
    }

    public void OpenHumanDex()
    {

    }

    public void CloseHumanDex()
    {
        if (iteration == 2)
            customTalk.NewTalkScripted(ScriptText, 7, 8);
    }


}
