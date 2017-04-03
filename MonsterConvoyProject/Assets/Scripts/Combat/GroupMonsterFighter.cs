using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMonsterFighter : GroupFighter {

    public GroupMonsterFighter() : base() {
        this.groupLogic = new PlayerLogic();
    }

}
