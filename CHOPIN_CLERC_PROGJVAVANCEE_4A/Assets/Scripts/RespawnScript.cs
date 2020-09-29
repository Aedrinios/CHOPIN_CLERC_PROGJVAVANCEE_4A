using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    public Transform ZoneStart;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {

            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = ZoneStart.position;
            
        }
    }
}
