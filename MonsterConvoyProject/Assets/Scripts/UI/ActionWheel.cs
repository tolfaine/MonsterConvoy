using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWheel : MonoBehaviour {

    public MouseOverAction talk;
    public MouseOverAction fear;
    public MouseOverAction attack;
    public MouseOverAction escape;

    public Fighter currentFighter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public void SetFighter(Fighter fighter)
    {
        currentFighter = fighter;

        if (currentFighter.eCreatureType == CreatureType.Monster)
        {
            if (currentFighter.bHasbeenAttcked)
                attack.SetActive(true);
            else
                attack.SetActive(false);
        }

    }

}
