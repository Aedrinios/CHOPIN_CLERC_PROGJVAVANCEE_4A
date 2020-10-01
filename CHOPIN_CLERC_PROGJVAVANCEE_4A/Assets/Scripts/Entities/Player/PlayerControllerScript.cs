using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MovingEntityScript
{
    private PlayerDataScript playerData;

    [Header("Physics Parameters")]
    [SerializeField]
    private float jumpForce; // Puissance de saut
    [SerializeField]
    private float gravityValue  = 20.0f;
    [SerializeField]
    private LayerMask groundMask;

    //private CharacterController controller;
    private Rigidbody rb;
    private Vector3 playerVelocity;
    private Vector3 impactVelocity;
    [SerializeField]
    
    private bool stopJump;
    private bool onGround;
    private float xInput, yInput;
    //
   
    
    float maxVelocityChange = 10.0f;
    
    float jumpHeight = 20.0f;
    
    //

    private void Start()
    {
        onGround = true;
        playerData = GetComponent<PlayerDataScript>();
        rb = GetComponent<Rigidbody>();
       // controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {////////////////////////////////////////
        
            // Calculate how fast we should be moving
            var targetVelocity = new Vector3(Input.GetAxis(playerData.HorizontalAxis), 0, 0);
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            var velocity = rb.velocity;
            var velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        if (onGround)
        {
            // Jump
            if (Input.GetKeyDown(playerData.JumpInput))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }
        onGround = false;

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravityValue * rb.mass, 0));

    }

    void OnCollisionStay(Collision collision)
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
       
       rb.AddForce(ballHit.Direction.normalized *5 * ballHit.Speed, (ForceMode.Force)) ;
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
