using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Inputs")]
    [SerializeField]
    private string movementAxisInput;
    [SerializeField]
    private string jumpInput;
    [SerializeField]
    private string attackInput;

    [Space(10)]

    [Header("Player Parameters")]
    [SerializeField]
    private string name;    
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

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 impactVelocity;
    private bool groundedPlayer;

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

    public void InitializeUI()
    {
        currentAttackOffsetTimer = attackOffsetTimer;
        playerUI = GameObject.Find(playerIndex + "UI");
        playerUI.transform.Find(playerIndex + "Name").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>().text = currentDamagePercentage + "%";

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
        xInput = Input.GetAxis(movementAxisInput);
        yInput = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(xInput, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed); 


        // Application de la valeur pour le jump
        if (Input.GetButton(jumpInput) && onGround)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue); 
        }
        // Jump du Player
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (impactVelocity.magnitude > 0.2)
        {
            playerVelocity = impactVelocity;
        }

        if (Input.GetButtonDown(attackInput))
        {
            isBallCaught = true;
        }

        impactVelocity = Vector3.Lerp(impactVelocity, Vector3.zero, 5 * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime); 
    }


    public void TakeDamage(float damage, BallControllerScript ball)
    {
        Debug.Log("OH");
        if (isBallCaught)
        {
            Debug.Log("AH?");
            ball.Direction = new Vector3(xInput, yInput, 0);
        }
        else
        {
            Debug.Log("NO");
            currentDamagePercentage += damage;
            playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>().text = currentDamagePercentage + "%";
            impactVelocity += currentDamagePercentage * ball.Direction.normalized * 30;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;
        }
    }

    public void lostOneLife()
    {
        if (healthRemaining >= 1)
        {
            healthRemaining--;
            currentDamagePercentage = 0;
         }
        if(healthRemaining == 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log(this.name + " Has lost !");
        }
    }
}
