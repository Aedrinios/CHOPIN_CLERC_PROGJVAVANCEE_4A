using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    [Header("Player Physics Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * -gravity);
        }

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += -gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
