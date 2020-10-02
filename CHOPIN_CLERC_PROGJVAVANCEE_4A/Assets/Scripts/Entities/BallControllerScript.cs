using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerScript : MovingEntityScript
{
    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private LayerMask wallMask;

    private float initSpeed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        float xDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
        float yDirection = UnityEngine.Random.Range(0.0f, 1.0f);
        direction = new Vector3(xDirection,yDirection,0.0f);
        initSpeed = speed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
            Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        if ((playerMask.value & (1 << collision.gameObject.layer)) > 0)
            collision.gameObject.GetComponent<PlayerLifeSystem>().RaiseOnPlayerDamageEvent(this);

        if ((wallMask.value & (1 << collision.gameObject.layer)) > 0)
        {


            ContactPoint[] contacts = collision.contacts;
            Vector3 sumNormal = Vector3.zero;
            foreach (ContactPoint contact in contacts)
                sumNormal += contact.normal;
            direction = Vector3.Reflect(direction, sumNormal);
            if((direction.y >= 0.8f || direction.y <= -0.8f) && direction.x <= 0.3f && direction.x >= -0.3)
                direction.x = UnityEngine.Random.Range(-1.0f, 1.0f);

            if ((direction.x >= 0.8f || direction.x <= -0.8f) && direction.y <= 0.3f && direction.y >= -0.3)
                direction.y = UnityEngine.Random.Range(-1.0f, 1.0f);

                speed++;
        }
    }

    public void ResetBallSpeed()
    {
        speed = initSpeed;

    }

    public void ReflectBallDirection(float xDirection, float yDirection)
    {
        if (xDirection != 0 && yDirection != 0)
            direction = new Vector3(xDirection, yDirection, 0.0f).normalized;
        else
            direction = -direction;
        speed = speed * 1.2f;
    }

    public override void Move()
    {
        rb.velocity = direction.normalized * speed * Time.deltaTime *100;
        
       // transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
