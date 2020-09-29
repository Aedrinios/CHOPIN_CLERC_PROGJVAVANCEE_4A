using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 9.0f; // Vitesse du Player
    [SerializeField]
    private float jumpHeight = 4.0f; // Puissance de saut
    [SerializeField]
    private float gravityValue = -9.81f;
    public LayerMask groundMask;
    public string axis;
    public string Button_jump;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        // Test si le Player est au sol
        groundedPlayer = controller.isGrounded; 
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // Mouvement Horizontal du Player
        Vector3 move = new Vector3(Input.GetAxis(axis), 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed); 


        // Application de la valeur pour le jump
        if (Input.GetButton(Button_jump) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); 
        }
        // Jump du Player
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;

        }
    }
}
