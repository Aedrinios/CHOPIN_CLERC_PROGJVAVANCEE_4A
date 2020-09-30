using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    [SerializeField]
    private Transform ZoneStart;
    private CharacterController cc;

    public void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            this.gameObject.GetComponent<PlayerController>().lostOneLife(); // Perte de vie du joueur + respawn au point de respawn
            cc.enabled = false;
            this.transform.position = ZoneStart.position;
            cc.enabled = true;

        }
    }
}
