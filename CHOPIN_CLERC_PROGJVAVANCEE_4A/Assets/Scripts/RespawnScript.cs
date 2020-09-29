using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    public Transform ZoneStart;
    public Text text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            
            text.text = "20";
            this.transform.position = ZoneStart.transform.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
