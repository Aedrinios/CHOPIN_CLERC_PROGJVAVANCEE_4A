﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision hit)
    {
        //   if ((playerMask.value & (1 << hit.gameObject.layer)) > 0)
        if (hit.collider.CompareTag("Respawn"))
            GetComponent<PlayerLifeSystem>().onPlayerLoseLife?.Invoke();
        



    }

}

