using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleManager : MonoBehaviour {
    public static bool created = false;
    [System.Serializable]
    public class ParticuleData
    {
        public int idFighter;
        public GameObject attackParticule;
        public GameObject impactParticule;
       
    }

    [System.Serializable]
    public class MonsterParticuleData
    {
        public GameObject attackFailParticule;
        public GameObject talkParticule;
        public GameObject talkCritParticule;
        public GameObject talkFailParticule;
        public GameObject fearParticule;
    }

    public MonsterParticuleData defaultParticules;
    public ParticuleData defaulAttackParticule;

    public List<ParticuleData> allParticules = new List<ParticuleData>();

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

    public ParticuleData GetData(int id)
    {
        foreach(ParticuleData data in allParticules)
        {
            if (data.idFighter == id)
                return data;
        }
        return null;
    }

    public GameObject GetParticuleAttack(Fighter fighter)
    {
        if (fighter.eCreatureType == CreatureType.Monster)
            return defaulAttackParticule.attackParticule;
        else
        {
            ParticuleData data = GetData(fighter.nID);
            if(data != null)
            {
                return data.attackParticule;
            }
            else
            {
                return defaulAttackParticule.attackParticule;
            }
        }
    }

    public GameObject GetParticuleImpact(Fighter fighter)
    {
        if (fighter.eCreatureType == CreatureType.Monster)
            return defaulAttackParticule.impactParticule;
        else
        {
            ParticuleData data = GetData(fighter.nID);
            if (data != null)
            {
                return data.impactParticule;
            }
            else
            {
                return defaulAttackParticule.impactParticule;
            }
        }
    }

    public GameObject GetParticuleOfAction(ActionType action, RollResultEnum rollResult)
    {
        if (action == ActionType.FEAR)
        {

            return defaultParticules.fearParticule;

        }else if (action == ActionType.ATTACK)
        {
            if (rollResult == RollResultEnum.Fail)
            {
                return defaultParticules.attackFailParticule;
            }
            else
                return null;
        }
        else if (action == ActionType.TALK)
        {
            if (rollResult == RollResultEnum.Normal)
            {
                return defaultParticules.talkParticule;
            }
            else if (rollResult == RollResultEnum.Crit)
            {
                return defaultParticules.talkCritParticule;
            }
            else
            {
                return defaultParticules.talkFailParticule;
            }
        }
        else
            return null;
    }
}
