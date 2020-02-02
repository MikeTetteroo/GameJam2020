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
    private float originalYScale; 
    public GameObject Player;
    private Collider ground;

    private Vector3 moveDirection = Vector3.zero;
    private bool digging = false;
    private bool crouching = false;
    public bool atDeposit;
    public bool holdingCollectible;

    private Status staminaDecrease;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalYScale = transform.localScale.y;
        staminaDecrease = GetComponent<Status>();
        UiScript.Instance.uiBar.targetStatus = staminaDecrease;
        UiScript.Instance.stamCircle.targetStatus = staminaDecrease;
        UiScript.Instance.uiBar.target = this.gameObject;
        UiScript.Instance.stamCircle.target = this.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButton("Running") && staminaDecrease.currentStamina > 0)
            {
                moveDirection *= runningSpeed;
                staminaDecrease.RemoveStamina(0.5f);
            } else
            {
                moveDirection *= speed;
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetButton("Dig") && digging == true && staminaDecrease.currentStamina > 0)
            {
                ground.gameObject.SetActive(false);
                staminaDecrease.RemoveStamina(25.0f);
            }

            if (Input.GetButton("PlaceCollectible") && atDeposit == true)
            {
                Homebase.Instance.PlaceCollectible(Homebase.Instance.playerIsHolding);
                holdingCollectible = false;
            }
        }
        if (!characterController.isGrounded)
        {
        moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void SetHomeBaseBool(bool isInTrigger)
    {
        atDeposit = isInTrigger;
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
