using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float attackOffsetTimer;
    private float currentAttackOffsetTimer;

    private PlayerDataScript playerData;
    private BallControllerScript ballToHit;

    private bool isBallCaught;

    private void Start()
    {
        playerData = transform.parent.GetComponent<PlayerDataScript>();
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

        float xInput = Input.GetAxis(playerData.HorizontalAxis);
        float yInput = Input.GetAxis(playerData.VerticalAxis);
        if (Input.GetKeyDown(playerData.HitBallInput) && ballToHit != null)
            HitBall(xInput, yInput);
    }

    private void HitBall(float x, float y)
    {

        isBallCaught = true;
        ballToHit.ReflectBallDirection(x, y);
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
