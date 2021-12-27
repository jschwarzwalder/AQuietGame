using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float slowSpeed = 3f;    
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;


    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    [SerializeField] AudioClip crouching;
    [SerializeField] AudioClip walking;
    [SerializeField] AudioClip running;
    [SerializeField] AudioClip walkIntoWall;
    public bool hasKey;

    public float soundEmitted;

    Vector3 velocity;
    bool isGrounded;
    AudioSource footsteps;
    float speed;

    int intoWall;

    void Start(){
        footsteps = GetComponent<AudioSource>();
        speed = walkSpeed;
        soundEmitted = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if((Input.GetAxis("Vertical") != 0.0f) || (Input.GetAxis("Horizontal") != 0.0f)){
            
            // Crouching
            if (intoWall > 0){
                footsteps.clip = walkIntoWall;
                soundEmitted = 1;
                speed = slowSpeed/2;
            }
            else if (
                (Input.GetButtonDown("crouch") || Input.GetAxis("Vertical") < 0f)  )         
            {
                speed = slowSpeed;
                footsteps.clip = crouching;
                soundEmitted = 0;

            } // Running
            else if ((Input.GetAxis("Vertical") > .5f) || (Input.GetButtonDown("shift") && !Input.GetButtonDown("crouch"))
            ) {
                speed = runSpeed;
                footsteps.clip = running;
                soundEmitted = 2;

                // Walking
            } else {
                speed = walkSpeed;
                footsteps.clip = walking;
                soundEmitted = 1;
            }



            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
            
            
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
            
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Wall"){
           intoWall += 1;
           Debug.Log("Collided with Wall");
        }
        Debug.Log("Hi");
 
    }
    
   void  OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Wall"){
           intoWall -= 1;
        }

    }
}
