using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private LayerMask wallMask;

    private Vector3 direction = Vector3.forward;

    private void Start()
    {
        float xDirection = UnityEngine.Random.Range(-1.0f, 1.0f); 
        float yDirection = UnityEngine.Random.Range(0.0f, 1.0f);
        direction = new Vector3(xDirection,yDirection,0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((playerMask.value & (1 << collision.gameObject.layer)) > 0)
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(2.0f, direction);
        //direction = new Vector3(direction.x, direction.y, 0.0f);


        if ((wallMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            float normalCollisionX = collision.GetContact(0).normal.x;
            float normalCollisionY = Mathf.Sign(collision.GetContact(0).normal.y);
            if (normalCollisionX != 0)
                direction.x = -direction.x;  
            if (normalCollisionY != 0)
                direction.y = -direction.y;
         //   direction = new Vector3(direction.x * Mathf.Sign(normalCollisionX), direction.y * Mathf.Sign(normalCollisionY), 0.0f);
            speed++;
        }

    }
}
