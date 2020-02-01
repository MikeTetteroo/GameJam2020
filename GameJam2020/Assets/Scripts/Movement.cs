using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float runningSpeed = 12.0f;
    public float jumpSpeed = 8.0f;
    public float digSpeed = 4.0f;
    public float gravity = 20.0f;
    public GameObject Player;
    private Collider ground;

    private Vector3 moveDirection = Vector3.zero;
    private bool digging = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButton("Running"))
            {
                moveDirection *= runningSpeed;
            } else
            {
                moveDirection *= speed;
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetButton("Dig") && digging == true)
            {
                ground.gameObject.SetActive(false);
            }
        }
            moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DiggableGround")
        {
            
            ground = other;
            digging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DiggableGround")
        {
            ground = null;
            digging = false;
        }
    }
}
