using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rgd;
    public Animator animator;

    Vector2 movement;

    [SerializeField]
    public bool facingLeft, facingRight, facingUp = false;
    [SerializeField]
    public bool facingDown = true;

    [SerializeField]
    public float lastXPos, lastYPos = 0f;           // Tọa độ hướng của nhân vật


    public float activeMoveSpeed = 7f;
    public float dashSpeed = 15f;

    public float dashLength = .3f, dashCooldown = 1f;

    public float dashCounter;
    public float dashCoolCounter;

    public ParticleSystem dust;

    SavePlayerPos playerPosData;

    void Start()
    {
        playerPosData = FindObjectOfType<SavePlayerPos>();
        activeMoveSpeed = PlayerStatusController.GetInstance().playerCurrentSpeed;
        dashSpeed = activeMoveSpeed + 8;
        playerPosData.PlayerPosSave();
    }

    public float speedTime = 0f;
    public float IncreaseSpeed = 1f;

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        FacingController();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        PlayerDirection();
        animator.SetFloat("LastHorizontal", lastXPos);
        animator.SetFloat("LastVertical", lastYPos);

        DashController();
        animator.SetFloat("DashHorizontal", movement.x);
        animator.SetFloat("DashVertical", movement.y);
        if (dashCounter > 0)
        {
            animator.SetBool("IsDashing", true);
        }
        else
        {
            animator.SetBool("IsDashing", false);
        }

        if (CheckHoldPress())
        {
            rgd.velocity = movement * activeMoveSpeed;

            speedTime += Time.deltaTime;

            if (speedTime >= 4)
            {
                IncreaseSpeed += Time.deltaTime;
                rgd.velocity = movement * activeMoveSpeed * IncreaseSpeed;
                if (IncreaseSpeed >= 2)
                {
                    IncreaseSpeed = 2;
                }

            }
        }
        else
        {
            speedTime = 0;
            IncreaseSpeed = 1;
            rgd.velocity = movement * activeMoveSpeed;
        }
    }

    void FacingController()
    {
        if (movement.x == 1 && movement.y == 1)
        {
            facingUp = true;
            facingDown = false;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == 1 && movement.y == -1)
        {
            facingUp = false;
            facingDown = true;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == -1 && movement.y == 1)
        {
            facingUp = true;
            facingDown = false;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == -1 && movement.y == -1)
        {
            facingUp = false;
            facingDown = true;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == 0 && movement.y == 1)
        {
            facingUp = true;
            facingDown = false;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == 0 && movement.y == -1)
        {
            facingUp = false;
            facingDown = true;
            facingLeft = false;
            facingRight = false;
        }
        else if (movement.x == 1 && movement.y == 0)
        {
            facingUp = false;
            facingDown = false;
            facingLeft = false;
            facingRight = true;
        }
        else if (movement.x == -1 && movement.y == 0)
        {
            facingUp = false;
            facingDown = false;
            facingLeft = true;
            facingRight = false;
        }
    }

    void PlayerDirection()
    {
        if (facingLeft)
        {
            lastXPos = -1f;
            lastYPos = 0f;
        }
        else if (facingRight)
        {
            lastXPos = 1f;
            lastYPos = 0f;
        }
        else if (facingUp)
        {
            lastXPos = 0f;
            lastYPos = 1f;
        }
        else if (facingDown)
        {
            lastXPos = 0f;
            lastYPos = -1f;
        }
    }

    void DashController()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                CreateDust();
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = PlayerStatusController.GetInstance().playerCurrentSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

    }

    public void CreateDust()
    {
        dust.Play();
    }

    public bool CheckHoldPress()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            return true;
        }
        else return false;

    }

    public void EnableController()
    {
        this.gameObject.GetComponent<PlayerMovement>().enabled = true;
    }

    public void DisableController()
    {
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        rgd.velocity = Vector2.zero;
        movement = Vector2.zero;
        animator.SetFloat("Speed", 0);
        animator.SetFloat("LastHorizontal", -1);
        animator.SetFloat("LastVertical", 0);
    }
}
