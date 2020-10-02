using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MovingEntityScript
{
    [SerializeField]
    private Transform body;

    [SerializeField]
    private float timeBeforeRandomInput;
    private float currentTimeBeforeRandomInput;
    private int randomInput = 0;

    private PlayerDataScript playerData;

    [Header("Physics Parameters")]
    [SerializeField]
    private float jumpForce; // Puissance de saut
    [SerializeField]
    private float gravityValue  = 20.0f;
    [SerializeField]
    private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 playerVelocity;
       
    public bool onGround;
    private float inputAxis;

    private Vector3 rbVelocity;
    private Animator animator;
    float maxVelocityChange = 10.0f;
    
    float jumpHeight = 20.0f;
    //

    private void Start()
    {
        onGround = true;
        playerData = GetComponent<PlayerDataScript>();
        animator = body.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        currentTimeBeforeRandomInput = timeBeforeRandomInput;

       // controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
        Animate();
    }

    public override void Move()
    {
        if(playerData.IsControlledByPlayer)
            MovePlayer();
        else
           MoveRandom();
    }

    private void Animate()
    {
        inputAxis = Input.GetAxis(playerData.HorizontalAxis);
        MoveToDirection(inputAxis);
        if (onGround)
        {
            if (Input.GetAxis(playerData.HorizontalAxis) != 0)
            {
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsJumping", false);
                animationCount = 0;
            }
            else
            {
                animator.SetBool("IsIdle", true);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsJumping", false);
            }
        }
        else
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", false);
        }
    }

    private void MovePlayer()
    {
        MoveToDirection(Input.GetAxis(playerData.HorizontalAxis));
            // Jump
        if (Input.GetKeyDown(playerData.JumpInput) && onGround)
            Jump();

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravityValue * rb.mass, 0));  
    }

    private void MoveRandom()
    {
        if (currentTimeBeforeRandomInput > 0)
        {
            currentTimeBeforeRandomInput -= Time.deltaTime;
        }
        else
        {
            currentTimeBeforeRandomInput = timeBeforeRandomInput;
            randomInput = Random.Range(0, 4);
        }
        switch (randomInput)
        {
            case 0:
                MoveToDirection(-1.0f);
                Debug.Log("WalkLeft");
                break;
            case 1:
                MoveToDirection(1.0f);
                Debug.Log("WalkRight");
                break;
            case 2:
                if(onGround)
                    Jump();
                break;
            case 3:
                Hit();
                break;
        }
        rb.AddForce(new Vector3(0, -gravityValue * rb.mass, 0));
    }

    private void MoveToDirection(float direction)
    {
        playerVelocity = new Vector3(direction, 0, 0);
        Quaternion rotation = body.rotation;
        if (direction < 0)
            rotation.y = 0;
        else
            rotation.y = 180;

        body.rotation = rotation;
        animationCount += Time.deltaTime;
        playerVelocity = transform.TransformDirection(playerVelocity);
        playerVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        rbVelocity = rb.velocity;
        Vector3 velocityChange = (playerVelocity - rbVelocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rbVelocity.x, CalculateJumpVerticalSpeed(), rbVelocity.z);
        onGround = false;
    }

    private void Hit()
    {
        float x = Random.Range(-1, 1);
        float y = Random.Range(-1, 1);
        transform.GetChild(0).GetComponent<PlayerAttackSystem>().HitBall(x, y);
    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Floor")
            onGround = true;
    }

    public void KnockbackPlayer(BallControllerScript ballHit)
    {
        Vector3 forceKnockback = ballHit.Direction;
        if (forceKnockback.normalized.y < 0 && onGround)
            forceKnockback.y = -forceKnockback.y;

        rb.AddForce(forceKnockback.normalized * 5 * ballHit.Speed, (ForceMode.Impulse));
    }

    public void RespawnPlayer()
    {
        rb.velocity = Vector3.zero;
        transform.position = playerData.PlayerSpawner;
        
        
    }
    

    private void OnEnable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage += KnockbackPlayer;
        GetComponent<PlayerLifeSystem>().onPlayerLoseLife += RespawnPlayer;
    }

    private void OnDisable()
    {
        GetComponent<PlayerLifeSystem>().onPlayerTakeDamage -= KnockbackPlayer;
        GetComponent<PlayerLifeSystem>().onPlayerLoseLife -= RespawnPlayer;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravityValue);
    }

}
