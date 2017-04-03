using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogicType { Player, IA}

[System.Serializable]
public class GroupLogic : MonoBehaviour {

    protected LogicType eLogicType;

    void Start () {}
	void Update () {}

    public virtual LogicType GetLogicType() { return this.eLogicType; }
}
