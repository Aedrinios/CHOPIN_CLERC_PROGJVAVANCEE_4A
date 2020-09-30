﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    private PlayerData playerInput;
    public PlayerData PlayerInput
    {
        set { playerInput = value; }
    }

    [Space(10)]

    [Header("Player Parameters")]
    [SerializeField]
    private string playerName;   
    public string PlayerName { get { return playerName; } }
    [SerializeField]
    private string playerIndex;
    [SerializeField]
    private float playerSpeed = 9.0f; // Vitesse du Player
    [SerializeField]
    private float jumpForce = 4.0f; // Puissance de saut
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float attackOffsetTimer;

    private TMPro.TextMeshProUGUI playerUIDamage;


    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 impactVelocity;
    [SerializeField]
    private float healthRemaining = 5;

    private GameObject playerUI;

    private float currentDamagePercentage;
    private bool isBallCaught;
    private float currentAttackOffsetTimer;
    private bool onGround;
    private float xInput, yInput;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void InitializePlayer(PlayerData input, string name, string index)
    {
        playerInput = input;
        playerName = name;
        playerIndex = index;
        currentAttackOffsetTimer = attackOffsetTimer;
        playerUI = GameObject.Find(playerIndex + "UI");
        playerUI.transform.Find(playerIndex + "Name").GetComponent<TMPro.TextMeshProUGUI>().text = playerName;
        playerUIDamage = playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>();
        playerUIDamage.text = currentDamagePercentage + "%";

    }

    void Update()
    {
        if (isBallCaught)
        {
            currentAttackOffsetTimer -= Time.deltaTime;
            if(currentAttackOffsetTimer <= 0)
            {
                isBallCaught = false;
                currentAttackOffsetTimer = attackOffsetTimer;
            }
        }

        // Test si le Player est au sol
        onGround = controller.isGrounded;
        if (onGround && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        // Mouvement Horizontal du Player



        xInput = Input.GetAxis(playerInput.horizontalAxis);
        yInput = Input.GetAxis(playerInput.verticalAxis);
        Vector3 move = new Vector3(xInput, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed); 


        // Application de la valeur pour le jump
        if (Input.GetKeyDown(playerInput.jumpInput) && onGround)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue); 
        }
        // Jump du Player
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (impactVelocity.magnitude > 0.2)
        {
            playerVelocity = impactVelocity;
        }

        if (Input.GetKeyDown(playerInput.hitBallInput))
        {
            isBallCaught = true;
        }

        impactVelocity = Vector3.Lerp(impactVelocity, Vector3.zero, 5 * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime); 
    }


    public void TakeDamage(float damage, BallControllerScript ball)
    {
        
        if (!isBallCaught)
        {
            currentDamagePercentage += damage;
            playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>().text = currentDamagePercentage + "%";
            impactVelocity += currentDamagePercentage * ball.Direction.normalized * 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;
        }
    }

    public void LoseOneLife()
    {
        if (healthRemaining >= 1)
        {
            healthRemaining--;
            currentDamagePercentage = 0;
            playerUIDamage.text = currentDamagePercentage + "%";


        }
        if (healthRemaining == 0)
        {
            GameManager.Instance.GameOver(this.gameObject);
            this.gameObject.SetActive(false);
            Debug.Log(this.name + " Has lost !");
            
        }
    }
}
