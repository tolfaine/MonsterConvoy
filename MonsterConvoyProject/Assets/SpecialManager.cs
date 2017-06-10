using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialType { Ed, Bard, Slip}

public class SpecialManager : MonoBehaviour {
    public static bool created = false;

    public IABard iaBard;
    public IAEd iaEd;
    public IASlip iaSlip;

    public RollProbaManager rollProbaManager;

    // Use this for initialization
    void Start () {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);

        GameObject rollObj = GameObject.FindGameObjectWithTag("RollProbaManager");
        if (rollObj != null)
        {
            rollProbaManager = rollObj.GetComponent<RollProbaManager>();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public GroupIA GetRandomIA()
    {
        
        float rand = Random.Range(0f, 1f);

        if (rand < rollProbaManager.specialProba.bard)
            return iaBard;
        else if (rand < rollProbaManager.specialProba.ed)
            return iaEd;
        else
            return iaSlip;

    }
}
