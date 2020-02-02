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
    public GameObject player;
    private Collider ground;
    public Animator anim;

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
            moveDirection = new Vector3(-Input.GetAxis("Vertical"), 0.0f, Input.GetAxis("Horizontal"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Debug.Log(transform.TransformDirection(moveDirection));

            if (Input.GetButton("Running") && staminaDecrease.currentStamina > 0 && characterController.velocity != Vector3.zero)
            {
                StartCoroutine(PlayAnimation("Fennek_Run", 0.5f));
                moveDirection *= runningSpeed;
                staminaDecrease.RemoveStamina(0.5f);
            }
            else if (characterController.velocity != Vector3.zero)
            {
                StartCoroutine(PlayAnimation("Fennek_Walk", 0.667f));
                moveDirection *= speed;
            }
            else
            {
                StartCoroutine(PlayAnimation("Fennek_Idle", 1.5f));
            }

            if (Input.GetButton("Jump"))
            {
                StartCoroutine(PlayAnimation("Fennek_Jump", 0.817f));
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetButton("Dig") && digging == true && staminaDecrease.currentStamina > 0)
            {
                StartCoroutine(PlayAnimation("Fennek_Dig", 1.467f));
                ground.gameObject.SetActive(false);
                staminaDecrease.RemoveStamina(25.0f);
            }

            if (Input.GetButton("PlaceCollectible") && atDeposit == true)
            {
                Homebase.Instance.PlaceCollectible(Homebase.Instance.playerIsHolding);
                holdingCollectible = false;
            }
            //if (transform.TransformDirection(moveDirection) == new Vector3(0,0,0))
            //{
            //    StartCoroutine(PlayAnimation("Fennek_Idle", 1.5f));
            //}
        }
        if (!characterController.isGrounded)
        {
        moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        player.transform.position = this.transform.position;
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

    IEnumerator PlayAnimation(string clip, float length)
    {
        anim.Play(clip);
        yield return new WaitForSeconds(length);
    }
}
