﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    [SerializeField]
    private Transform zoneStart;
    public Transform ZoneStart
    {
        set { zoneStart = value; }
    }

    private CharacterController cc;

    public void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            gameObject.GetComponent<PlayerControllerScript>().LoseOneLife();
            cc.enabled = false;
            transform.position = zoneStart.position;
            cc.enabled = true;

        }
    }
}
