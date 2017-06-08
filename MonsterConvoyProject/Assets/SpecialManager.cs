using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpecialType { Ed, Bard}

public class SpecialManager : MonoBehaviour {
    public static bool created = false;

    public IABard iaBard;
    public IAEd iaEd;

	// Use this for initialization
	void Start () {
        if (!created)
        {
            DontDestroyOnLoad(transform.gameObject);
            created = true;
        }
        else
            Destroy(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public GroupIA GetRandomIA()
    {
        float rand = Random.Range(0f, 1f);

        if (rand > 0.5f)
            return iaBard;
        else
            return iaEd;

    }
}
