using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEd : GroupIA {

    private int currentLine = 1;
    public bool bWantsToAtaack = false;

    public GroupEd groupEd;

    public bool isDead;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override ActionType SelectAction(List<Fighter> Enemies, List<Fighter> Allies)
    {
        ActionType action;

        if (bWantsToAtaack)
            action = ActionType.ATTACK;
        else
            action = ActionType.TALK;
        bWantsToAtaack = !bWantsToAtaack;

        return action;

    }

    public void Talk()
    {
        GameObject g = GameObject.FindGameObjectWithTag("CombatManager");
        if (g != null && g.GetComponent<CombatManager>())
        {

            TalkManager sm = g.GetComponent<CombatManager>().talkManager;
            sm.customTalk.NewTalkED(currentLine);
            currentLine++;

            if (currentLine == 10)
                currentLine = 0;
        }
    }

    public void EndCombat()
    {

        if(groupEd.lFighters.Count == 0 || groupEd.lFighters[0].IsDead())
        {
            isDead = true;
        }

    }

}
