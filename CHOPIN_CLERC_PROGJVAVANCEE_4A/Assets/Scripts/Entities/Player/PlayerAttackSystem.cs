using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float attackOffsetTimer;
    private float currentAttackOffsetTimer;

    private PlayerDataScript playerInput;
    private BallControllerScript ballToHit;

    private bool isBallCaught;

    private void Start()
    {
        playerInput = transform.parent.GetComponent<PlayerDataScript>();
        currentAttackOffsetTimer = attackOffsetTimer;
    }

    private void Update()
    {
        if (isBallCaught)
        {
            currentAttackOffsetTimer -= Time.deltaTime;
            if (currentAttackOffsetTimer <= 0)
            {
                isBallCaught = false;
                currentAttackOffsetTimer = attackOffsetTimer;
            }
        }

        if (Input.GetKeyDown(playerInput.HitBallInput) && ballToHit != null)
            HitBall();
    }

    private void HitBall()
    {
        isBallCaught = true;
        ballToHit.ReflectBallDirection();
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
