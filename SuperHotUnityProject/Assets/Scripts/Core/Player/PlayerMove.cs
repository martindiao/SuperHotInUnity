﻿using UnityEngine;

namespace Digital
{
    namespace Core
    {
        namespace Player
        {
            public class PlayerMove : MonoBehaviour
            {
                [SerializeField] private float speed = 10;
                [SerializeField] private float xSens = 10, ySens = 10;
                [SerializeField] private Transform cam;

                Vector2 moveInput, lookInput;

                CharacterController cc;

                private void Start()
                {
                    cc = GetComponent<CharacterController>();

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }

                private void Update()
                {
                    CalculateInput();
                    Look();
                    Move();
                    Fall();

                    //Set time scale btw .2f and 1
                    Time.timeScale = moveInput.magnitude;
                    if (Time.timeScale <= .1f) Time.timeScale = .2f;
                }

                private void CalculateInput()
                {
                    moveInput.x = Input.GetAxis("Horizontal");
                    moveInput.y = Input.GetAxis("Vertical");
                    moveInput.Normalize();

                    lookInput.x = Input.GetAxis("Mouse X");
                    lookInput.y = Input.GetAxis("Mouse Y");
                    lookInput.Normalize();
                }
                private void Look()
                {
                    if(lookInput.magnitude > .1f)
                    {
                        transform.Rotate(0, lookInput.x * xSens * Time.unscaledDeltaTime, 0);
                        cam.Rotate(lookInput.y * ySens * Time.unscaledDeltaTime, 0, 0);
                    }
                }
                private void Move()
                {
                    if(moveInput.magnitude > .1f)
                    {
                        Vector3 velocity = (transform.right * moveInput.x + transform.forward * moveInput.y) * Time.deltaTime * speed;
                        cc.Move(velocity);
                    }
                }
                private void Fall()
                {
                    if (!cc.isGrounded)
                    {
                        cc.Move(new Vector3(0, -9.81f * Time.deltaTime, 0));
                    }
                }
            }
        }
    }
}
