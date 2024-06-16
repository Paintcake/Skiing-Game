using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkierController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 100f;
    public float maxTurnAngle = 90f;
    public float boostMultiplier = 2f;
    public float boostDuration = 1f;
    public float boostCooldown = 5f;
    public float knockbackDuration = 1f;
    public float knockbackForce = 10f;
    public AudioClip collisionSound;

    public Rigidbody rb;
    public Animator animator;
    public AudioSource audioSource;
    public bool isGrounded = true;
    public bool isBoosting = false;
    public bool isKnockedBack = false;
    public float currentTurnAngle = 0f;
    public float boostEndTime = 0f;
    public float nextBoostTime = 0f;
    public float knockbackEndTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        PlayerCollisionEvent.OnPlayerCollision += HandlePlayerCollision;
    }

    void Update()
    {
        if (!isKnockedBack)
        {
            HandleInput();
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            MoveSkier();
        }
    }

    void HandleInput()
    {
        if (isGrounded)
        {
            float turnInput = Input.GetAxis("Horizontal");
            currentTurnAngle += turnInput * turnSpeed * Time.deltaTime;
            currentTurnAngle = Mathf.Clamp(currentTurnAngle, -maxTurnAngle, maxTurnAngle);

            transform.rotation = Quaternion.Euler(0, currentTurnAngle, 0);

            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextBoostTime)
            {
                StartCoroutine(SpeedBoost());
            }
        }
    }

    void MoveSkier()
    {
        if (isGrounded)
        {
            Vector3 moveDirection = transform.forward;
            float currentSpeed = isBoosting ? speed * boostMultiplier : speed;
            rb.velocity = moveDirection * currentSpeed;
        }
    }

    void UpdateAnimation()
    {
        float velocity = rb.velocity.magnitude;
        animator.SetFloat("Speed", velocity);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private IEnumerator SpeedBoost()
    {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        nextBoostTime = Time.time + boostCooldown;

        yield return new WaitForSeconds(boostDuration);

        isBoosting = false;
    }

    void HandlePlayerCollision()
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            knockbackEndTime = Time.time + knockbackDuration;
            rb.AddForce(-transform.forward * knockbackForce, ForceMode.Impulse);
            PlayCollisionSound();
            StartCoroutine(KnockbackRecovery());
        }
    }

    void PlayCollisionSound()
    {
        if (collisionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
    }

    private IEnumerator KnockbackRecovery()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
    
    void OnDestroy()
    {
        PlayerCollisionEvent.OnPlayerCollision -= HandlePlayerCollision;
    }
}

