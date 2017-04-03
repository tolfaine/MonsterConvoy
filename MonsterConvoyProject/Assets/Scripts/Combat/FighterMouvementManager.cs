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
    public bool bIsAtInitialPosition = true;

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
            CheckMovement();
            CheckPosition();
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

    }

    void CheckMovement()
    {
        if(bMoveToFightPosition && !bIsAtFightPosition)
        {
            float step = speed * Time.deltaTime;
            fighterObj.transform.position = Vector3.MoveTowards(fighterObj.transform.position, fightPosition, step);
        }

        if (bMoveToInitialPosition && !bIsAtInitialPosition)
        {
            float step = speed * Time.deltaTime;
            fighterObj.transform.position = Vector3.MoveTowards(fighterObj.transform.position, fighterInitialPosition, step);
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
