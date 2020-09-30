using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private BallControllerScript ballToHit;

    private void Update()
    {
        if (Input.GetButtonDown("Grab") && ballToHit != null)
        {
            HitBall();
        }
    }

    private void HitBall()
    {
        ballToHit.ReflectBallDirection();
        Debug.Log("Attack");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballToHit = other.gameObject.GetComponent<BallControllerScript>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
            ballToHit = null;

    }



}

