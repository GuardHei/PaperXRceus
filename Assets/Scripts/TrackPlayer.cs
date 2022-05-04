﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = transform.position + new Vector3(player.transform.position.x - transform.position.x, 0f, 0f);
        }
    }
}
