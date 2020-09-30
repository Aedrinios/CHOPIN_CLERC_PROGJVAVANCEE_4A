using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
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
        cc = GetComponent<CharacterController>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            gameObject.GetComponent<PlayerControllerScript>().LoseOneLife();
            cc.enabled = false;
            transform.position = zoneStart.position;
            cc.enabled = true;

        }
    }
}
