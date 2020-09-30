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
    private float gravityValue;
    [SerializeField]
    private LayerMask groundMask;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 impactVelocity;

    private bool onGround;
    private float xInput, yInput;

    private void Start()
    {
        playerData = GetComponent<PlayerDataScript>();
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        // Test si le Player est au sol
        onGround = controller.isGrounded;
        if (onGround && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Mouvement Horizontal du Player
        xInput = Input.GetAxis(playerData.HorizontalAxis);
        yInput = Input.GetAxis(playerData.VerticalAxis);
        Vector3 move = new Vector3(xInput, 0, 0);
        controller.Move(move * Time.deltaTime * speed);

        // Application de la valeur pour le jump
        if (Input.GetKeyDown(playerData.JumpInput) && onGround)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
        }

        if (impactVelocity.magnitude > 0.2)
        {
            playerVelocity = impactVelocity;
        }
        impactVelocity = Vector3.Lerp(impactVelocity, Vector3.zero, 5 * Time.deltaTime);

        // Jump du Player
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public override void Freeze()
    {
        throw new System.NotImplementedException();
    }
    public void KnockbackPlayer(BallControllerScript ballHit)
    {
        impactVelocity += ballHit.Speed * ballHit.Direction.normalized * 10;
    }

    public void RespawnPlayer()
    {
        Debug.Log("Order :" + this.name);
        transform.position = playerData.PlayerSpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;
        }
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
}
