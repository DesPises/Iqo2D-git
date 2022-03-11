using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plMovement : MonoBehaviour
{
    private Rigidbody2D plRB;
    private LayerMask floorLayer;

    [SerializeField] private float gizmosY;
    [SerializeField] private float gizmosX;

    private readonly float jumpForce = 200;
    private readonly float floorDist = 1;

    public static bool secJump = false;
    public static bool isMovingFW = true;
    public static bool onGround = false;
    public static bool isNearEnemy = false;
    public static string character;
    public static float plCoordinateX;

    private void Start()
    {
        plRB = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.GetMask("Floor");

        isMovingFW = true;
    }


    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            // Link player's pos to variable
            plCoordinateX = transform.position.x;

            // Ground check
            onGround = Physics2D.Raycast(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer);

            // Rotate player towards moving direction
            RotationControl();

            // Jump
            if (Input.GetKeyDown(InputManager.IM.jumpKey) && onGround)
            {
                Jump();
            }
            // Second jump
            if (Input.GetKeyDown(InputManager.IM.jumpKey) && secJump && !onGround)
            {
                SecJump();
            }

            // Crouch
            if (Input.GetKey(InputManager.IM.crouchKey) && onGround)
            {
                Crouch();
            }

            // Characters specifications

            if (character == "Rifler" && !GameManager.rIsDead)
            {
                    Move(5);
            }

            if (character == "Sniper" && !GameManager.sIsDead)
            {
                    Move(4);
            }

            if (character == "Sickler" && !GameManager.siIsDead && GameManager.canWalkSi)
            {
                    Move(6);
            }
        }
    }



    //Movement methods

    private void Move(int moveSpeed)
    {
        if (!Input.GetKey(InputManager.IM.crouchKey))
        {
            if (Input.GetKey(InputManager.IM.fwKey) && !Input.GetKey(InputManager.IM.bwKey))
            {
                isMovingFW = true;
                plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
            }
            else if (Input.GetKey(InputManager.IM.bwKey) && !Input.GetKey(InputManager.IM.fwKey))
            {
                isMovingFW = false;
                plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
            }
        }
        else
        {
            if (Input.GetKeyDown(InputManager.IM.fwKey))
            {
                isMovingFW = true;
            }
            else if (Input.GetKeyDown(InputManager.IM.bwKey))
            {
                isMovingFW = false;
            }
        }
    }

    void Jump()
    {
        secJump = true;
        plRB.velocity = new Vector2(plRB.velocity.x, 0);
        plRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void SecJump()
    {
        plRB.velocity = new Vector2(plRB.velocity.x, 0);
        plRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Crouch()
    {
        plRB.velocity = new Vector2(0, 0);

    }
    private void RotationControl()
    {
        if (character != "Sniper")
        {
            if (!isMovingFW)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (isMovingFW)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (!isMovingFW)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (isMovingFW)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isNearEnemy = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isNearEnemy = false;
        }
    }


    //Sounds methods

    public void AkReloadSound()
    {
        SoundController.Instance.akReloadS();
    }

    public void RunSound()
    {
        SoundController.Instance.RunS();
    }

    public void JumpSound()
    {
        SoundController.Instance.JumpS();
    }

    public void SiVzmahSound()
    {
        SoundController.Instance.siVzmahS();
    }

    public void SiHitSound()
    {
        SoundController.Instance.siHitS();
    }

    // Draw raycast of ground check

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    }
}
