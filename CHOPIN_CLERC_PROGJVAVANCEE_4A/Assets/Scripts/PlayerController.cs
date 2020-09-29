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

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 impactVelocity;
    private bool groundedPlayer;
    private GameObject playerUI;
    private float currentDamagePercentage;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void InitializeUI()
    {
        playerUI = GameObject.Find(playerIndex + "UI");
        playerUI.transform.Find(playerIndex + "Name").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>().text = currentDamagePercentage + "%";

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
        Vector3 move = new Vector3(Input.GetAxis(movementAxisInput), 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed); 


        // Application de la valeur pour le jump
        if (Input.GetButton(jumpInput) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue); 
        }
        // Jump du Player
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (impactVelocity.magnitude > 0.2)
        {
            playerVelocity = impactVelocity;
        }

        impactVelocity = Vector3.Lerp(impactVelocity, Vector3.zero, 5 * Time.deltaTime);
        controller.Move(playerVelocity * Time.deltaTime); 
    }


    public void TakeDamage(float damage, Vector3 damageDirection)
    {
        currentDamagePercentage += damage;
        playerUI.transform.Find(playerIndex + "Damage").GetComponent<TMPro.TextMeshProUGUI>().text = currentDamagePercentage + "%";
        impactVelocity += currentDamagePercentage * damageDirection.normalized * 30;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!controller.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;

        }
    }
}
