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
    /*   if (other.gameObject.CompareTag("Respawn"))
        {
            GetComponent<BallControllerScript>().ResetBallSpeed();
            this.transform.position = ZoneStart.position;
            
        }*/
        //if (other.gameObject.CompareTag("Wall"))
        //    this.transform.position += warpVector ;
    }
}
