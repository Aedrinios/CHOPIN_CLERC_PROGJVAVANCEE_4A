using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collide = collision.gameObject;
        if (collide.CompareTag("Player")) 
            collide.GetComponent<PlayerLifeSystem>().onPlayerLoseLife?.Invoke();
        
         //   collide.GetComponent<PlayerLifeSystem>().LoseOneLife();
    }
}
