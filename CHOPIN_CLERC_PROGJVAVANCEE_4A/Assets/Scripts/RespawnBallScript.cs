using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnBallScript : MonoBehaviour
{
    [SerializeField]
    private Transform ZoneStart;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            BallControllerScript.Instance().ResetSpeed(); 
            this.transform.position = ZoneStart.position;//Reset la speed de la boule quand elle sort du terrain et la remet à son spawn
            
        }
    }
}
