using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MonoBehaviour {

    public GameObject player;

    void Start()
    {
        player = Instantiate(player, transform);
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = NodeConnections.activeNode.transform.position;
    }

}
