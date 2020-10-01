using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    [SerializeField]
    private float stunTimer;
    private float currentStunTimer;

    private PlayerDataScript playerData;
    private BallControllerScript ballToHit;
    private bool isStun = false;

    private void Start()
    {
        playerData = transform.parent.GetComponent<PlayerDataScript>();
        currentStunTimer = stunTimer;
    }

    private void Update()
    {

        if (isStun)
        {
            currentStunTimer -= Time.deltaTime;
            if (currentStunTimer <= 0)
            {
                isStun = false;
                currentStunTimer = stunTimer;
            }
        }
    }
       /* float xInput = Input.GetAxis(playerData.HorizontalAxis);
        float yInput = Input.GetAxis(playerData.VerticalAxis);
        if (Input.GetKeyDown(playerData.HitBallInput) && !isStun)
            HitBall(xInput, yInput);
    }

    public void HitBall(float x, float y)
    {
        if (ballToHit != null)
            ballToHit.ReflectBallDirection(x, y);
    }

    /*public void StunAttack(BallControllerScript ball)
    {
        isStun = true;
    }

    private void OnEnable()
    {
        transform.parent.GetComponent<PlayerLifeSystem>().onPlayerTakeDamage += StunAttack;
    }
    private void OnDisable()
    {
        transform.parent.GetComponent<PlayerLifeSystem>().onPlayerTakeDamage -= StunAttack;
    }*/


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
