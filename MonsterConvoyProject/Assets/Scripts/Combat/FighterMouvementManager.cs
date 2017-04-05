using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMouvementManager : MonoBehaviour {

    public Transform fightTransform;
    public Vector3 fightPosition;
    public GameObject fighterObj;
    public Vector3 fighterInitialPosition;

    public bool bMoveToFightPosition = false;
    public bool bIsAtFightPosition = false;
    public bool bMoveToInitialPosition = false;

    public int nCycleToWait = 5;
    public int nCurrentCycle = 0;

    public bool bWaitOneCycle = false;
    public bool bIsAtInitialPosition = true;

    public bool bAboutToMoveInitialPosition = false;

    public GameObject AllHumanObj;
    public Transform HumanRunposition;
    public bool bHumanRun;
    public bool bHumanAtRunPosition;

    public GameObject AllMonsterObj;
    public Transform MonsterRunPosition;
    public bool bMonsterRun;
    public bool bMonsterAtRunPosition;

    public bool bNotifyCombatManagerFightPosition = false;
    public bool bNotifyCombatManagerInitialPosition = false;

    public float speed;

    public CombatManager combatManager;

    // Use this for initialization
    void Start () {
        fightPosition = fightTransform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (fighterObj != null)
        {
            if (!combatManager.bDialogueInProgres)
            {
                CheckMovement();
                CheckPosition();
            }
        }
      //  NotifyCombatManager();

    }
    /*
    void NotifyCombatManager()
    {
        if (bNotifyCombatManagerFightPosition)
        {
            bNotifyCombatManagerFightPosition = false;
            combatManager.bFighterInFightPosition = true;
        }

        else if (bNotifyCombatManagerInitialPosition)
        {
            bNotifyCombatManagerInitialPosition = false;
            combatManager.bFighterInInitialPosition = true;
        }  
        
    }
    */
    void CheckPosition()
    {
        Vector3 position = fighterObj.transform.position;

        if (position == fightPosition)
        {
            bIsAtFightPosition = true;
            combatManager.bFighterInFightPosition = true;
            if (bMoveToFightPosition)
            {
                bMoveToFightPosition = false;

            }
        }
        else
        {
            combatManager.bFighterInFightPosition = false;
            bIsAtFightPosition = false;
        }

        if (position == fighterInitialPosition)
        {
            bIsAtInitialPosition = true;
            bAboutToMoveInitialPosition = false;
            combatManager.bFighterInInitialPosition = true;
            if (bMoveToInitialPosition)
            {
                bMoveToInitialPosition = false;
            }
        }
        else
        {
            combatManager.bFighterInInitialPosition = false;
            bIsAtInitialPosition = false;
        }

        if (AllHumanObj.transform.position == HumanRunposition.position)
            bHumanAtRunPosition = true;
        if (AllMonsterObj.transform.position == MonsterRunPosition.position)
            bMonsterRun = true;
    }

    void CheckMovement()
    {
        if (bMonsterRun)
        {
            float step = speed * Time.deltaTime;
            AllMonsterObj.transform.position = Vector3.MoveTowards(AllMonsterObj.transform.position, MonsterRunPosition.position, step);

        }

        if (bHumanRun) // && bIsAtInitialPosition
        {
            float step = speed * Time.deltaTime;
            AllHumanObj.transform.position = Vector3.MoveTowards(AllHumanObj.transform.position, HumanRunposition.position, step);

        }

        if (bMoveToFightPosition && !bIsAtFightPosition)
        {
            float step = speed * Time.deltaTime;
            fighterObj.transform.position = Vector3.MoveTowards(fighterObj.transform.position, fightPosition, step);
        }

        if (bMoveToInitialPosition && !bIsAtInitialPosition)
        {
            if (!bWaitOneCycle)
            {

                    float step = speed * Time.deltaTime;
                    fighterObj.transform.position = Vector3.MoveTowards(fighterObj.transform.position, fighterInitialPosition, step);
            }
            else
                bWaitOneCycle = false;
        }
        else
        {
            bWaitOneCycle = true;
        }

    }


    public void SetFighter(GameObject fighterObj)
    {
        this.fighterObj = fighterObj;
        this.fighterInitialPosition = fighterObj.transform.position;


        this.bMoveToFightPosition = false;
        this.bMoveToInitialPosition = false;

        this.bIsAtFightPosition = false;
        this.bIsAtInitialPosition = true;

    }

}
