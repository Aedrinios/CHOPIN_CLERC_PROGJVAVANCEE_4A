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
            BallControllerScript.Instance.ResetBallSpeed();
            this.transform.position = ZoneStart.position;
            
        }
    }
}
