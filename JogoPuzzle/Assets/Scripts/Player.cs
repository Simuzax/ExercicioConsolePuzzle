﻿using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;

    public float walkSpeed = 3f;
    public float runSpeed = 8f;

    public float gravity = -12f;
    public float jumpHeight = 5f;

    [SerializeField]
    private float velocityY;

    [SerializeField]
    private bool running = false;

    [SerializeField]
    private Game game_ref;

    private float smoothRotationVelocity;

    [SerializeField]
    private float smoothRotationTime = 0.2f;

    private float smoothSpeedVelocity;

    [SerializeField]
    private float smoothSpeedTime = 0.2f;

    [SerializeField]
    private Transform cameraT;

    [SerializeField]
    private CharacterController charController;

    [SerializeField]
    private Animator animator;

    public float playerHeight = 2;

    public float slideDistance;
    public float slideDuration;
    public float slideSpeed;

    [SerializeField]
    private Vector2 input;

    // Start is called before the first frame update
    private void Start()
    {
        cameraT = Camera.main.transform;
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        walkRotating();
    }

    private void walkRotating()
    {
        Vector3 velocity = Vector3.zero;

        velocityY += gravity * Time.deltaTime;

        if (!animator.GetBool("isSliding"))
        {
            input = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized);

            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

            if (input != Vector2.zero)
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);

            running = (Input.GetKey(KeyCode.LeftShift));

            float targetSpeed = (running) ? runSpeed : walkSpeed;

            speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);

            velocity = transform.forward * speed * input.magnitude + Vector3.up * velocityY;
            charController.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity = transform.forward * slideSpeed + Vector3.up * velocityY;
            charController.Move(velocity * Time.deltaTime);
        }

        speed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        if (charController.isGrounded)
        {
            velocityY = 0;
        }

        float animationSpeedPercent = ((running) ? speed / runSpeed : speed / walkSpeed * 0.5f) * input.magnitude;

        animator.SetFloat("speedPercent", animationSpeedPercent, smoothSpeedTime, Time.deltaTime);
        animator.SetBool("isGrounded", charController.isGrounded);
        animator.SetFloat("velocityY", velocityY);
        animator.SetBool("spaceDown", Input.GetKeyDown(KeyCode.Space));

        jump();

        runningSlide();
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (charController.isGrounded)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Animation"))
                {
                    animator.Play("JumpUp");
                }

                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);

                velocityY = jumpVelocity;
            }
        }
    }

    private void runningSlide()
    {
        if (Input.GetKeyDown(KeyCode.C) && running && animator.GetBool("isSliding") == false)
        {
            if (charController.isGrounded == true)
            {
                GetComponent<CharacterController>().Move(Vector3.zero);

                transform.DOMove(transform.forward * slideDistance, slideDuration).OnComplete(() =>
                {
                    GetComponent<CharacterController>().height = playerHeight;
                    animator.SetBool("isSliding", false);
                });

                GetComponent<CharacterController>().height /= 2;

                animator.SetBool("isSliding", true);
            }
        }
    }
}