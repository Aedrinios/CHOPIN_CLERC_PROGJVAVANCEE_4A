using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MovingEntityScript
{
    [SerializeField]
    private float timeBeforeRandomInput;
    private float currentTimeBeforeRandomInput;

    private PlayerDataScript playerData;

    [Header("Physics Parameters")]
    [SerializeField]
    private float jumpForce; // Puissance de saut
    [SerializeField]
    private float gravityValue  = 20.0f;
    [SerializeField]
    private LayerMask groundMask;

    private Vector3 desiredMoveDirection;
    private Rigidbody rb;
    private Vector3 playerVelocity;
       
    public bool onGround;

    private Vector3 rbVelocity;
    private Animator animator;
    float maxVelocityChange = 10.0f;
    
    float jumpHeight = 20.0f;
    
    //

    private void Start()
    {
        onGround = true;
        playerData = GetComponent<PlayerDataScript>();
        animator = GetComponentInChildren<Animator>();
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
                Debug.Log("run !");
                animator.SetTrigger("Run");
            }
            else
            {
                Debug.Log("Idle");
                animator.SetTrigger("Idle");

            }
        }
        else
        {
            Debug.Log("Jump");
            animator.SetTrigger("Jump");
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

      /*  desiredMoveDirection = new Vector3(Input.GetAxis(playerData.HorizontalAxis), 0, 0);
        desiredMoveDirection.Normalize();
        if(desiredMoveDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 0.1f);
      */    
    }

    private void MoveRandom()
    {
        if(currentTimeBeforeRandomInput > 0)
        {
            currentTimeBeforeRandomInput -= Time.deltaTime;
        }
        else
        {
            int newRandomInput = Random.Range(0, 4);
            switch (newRandomInput)
            {
                case 0:
                    MoveToDirection(-1.0f);
                    currentTimeBeforeRandomInput = timeBeforeRandomInput;
                    break;
                case 1:
                    MoveToDirection(1.0f);
                    currentTimeBeforeRandomInput = timeBeforeRandomInput;
                    break;
                case 2:
                    Jump();
                    break;
                case 3:
                    Hit();
                    break;
            }
        }
        rb.AddForce(new Vector3(0, -gravityValue * rb.mass, 0));
    }

    private void MoveToDirection(float direction)
    {
        playerVelocity = new Vector3(direction, 0, 0);
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
       // transform.GetChild(0).GetComponent<PlayerAttackSystem>().HitBall(x, y);
    }

    void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Floor")
            onGround = true;
    }


    public override void Freeze()
    {
        throw new System.NotImplementedException();
    }

    public void KnockbackPlayer(BallControllerScript ballHit)
    {
 
       rb.AddForce(ballHit.Direction.normalized *5 * ballHit.Speed, (ForceMode.Impulse)) ;

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
