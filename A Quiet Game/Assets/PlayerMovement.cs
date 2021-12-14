using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;


    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] AudioClip crouching;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip running;

    Vector3 velocity;
    bool isGrounded;
    AudioSource footsteps;

    void Start(){
        footsteps = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if((Input.GetAxis("Vertical") != 0.0f) || (Input.GetAxis("Horizontal") != 0.0f)){
            //play sound 
             if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
            
            Debug.Log("Footsteps Play");
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if(Input.GetButtonDown("Jump") && isGrounded){
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        } else {
            //pause sound
            footsteps.Pause();
            Debug.Log("Footsteps Pause");
        }
    }
}
