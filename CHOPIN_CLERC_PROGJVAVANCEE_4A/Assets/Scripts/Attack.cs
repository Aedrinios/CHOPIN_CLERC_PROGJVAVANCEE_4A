using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private RaycastHit hit;
    public CharacterController cc;
    private bool grabbed = false;
    private Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    
    // Update is called once per frame
   

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //Permet de reflect la ball quand elle est proche du joueur
            if (Input.GetButtonDown("Grab"))
            {
                BallControllerScript.Instance().getReflected() ;
                Debug.Log("Attack");

            }
        }
    }
 
}

