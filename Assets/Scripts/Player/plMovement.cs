using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plMovement : MonoBehaviour
{
    //Objects
    public Rigidbody2D plRB;
    public GameObject plGO, soundContrGO;
    public static Transform plPos;

    //Variables
    public float moveSpeed, jumpForce, floorDist, gizmosY, gizmosX;
    [SerializeField] public static bool secJump = false, isMovingFW = true, onGround = false, isNearEnemy = false;
    [SerializeField] public static string character;
    public static float plCoordinateX;

    public KeyCode jumpKey, fwKey, bwKey, crouchKey;

    //Raycast
    public Vector3 colliderOffset;
    public LayerMask floorLayer;

    void Awake()
    {
        floorDist = 1;
        jumpForce = 200;
    }

    void Start()
    {
        isMovingFW = true;
        fwKey = InputManager.IM.fwKey;
        bwKey = InputManager.IM.bwKey;
        crouchKey = InputManager.IM.crouchKey;
        jumpKey = InputManager.IM.jumpKey;
    }


    void Update()
    {
        plPos = plGO.transform;
        plCoordinateX = plPos.position.x;

        //Input and Animations

        //Move
        if (character != "Sickler" && (!GameManager.rIsDead || !GameManager.sIsDead) && !PauseMenu.isPaused)
        {
            if (Input.GetKey(fwKey) && !Input.GetKey(bwKey))
            {
                isMovingFW = true;
                if (!Input.GetKey(crouchKey))
                    plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
            }
            if (Input.GetKey(bwKey) && !Input.GetKey(fwKey))
            {
                isMovingFW = false;
                if (!Input.GetKey(crouchKey))
                    plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
            }
            if (Input.GetKey(fwKey) && isMovingFW && !Input.GetKey(crouchKey))
            {
                if (Input.GetKey(bwKey))
                {
                    plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
                }
            }
            if (Input.GetKey(bwKey) && !isMovingFW && !Input.GetKey(crouchKey))
            {
                if (Input.GetKey(fwKey))
                {
                    plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
                }
            }
        }





        //Jump
        if (Input.GetKeyDown(jumpKey) && onGround)
        {
            Jump();
        }
        if (Input.GetKeyDown(jumpKey) && secJump && !onGround)
        {
            SecJump();
        }


        //Crouch
        if (Input.GetKey(crouchKey) && onGround)
        {
            Crouch();

        }


        //Characters specifications

        if (character == "Rifler")
        {
            moveSpeed = 5;

            onGround = Physics2D.Raycast(transform.position + colliderOffset + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
                Physics2D.Raycast(transform.position - colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
                Physics2D.Raycast(transform.position - colliderOffset * 0.5f + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer);
            if (!isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (character == "Sniper")
        {
            moveSpeed = 4;

            onGround = Physics2D.Raycast(transform.position - colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
                Physics2D.Raycast(transform.position + colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
                Physics2D.Raycast(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer);

            if (!isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        if (character == "Sickler")
        {
            //Move
            moveSpeed = 6;
            if (!GameManager.siIsDead && !PauseMenu.isPaused)
            {
                if (Input.GetKey(fwKey) && !Input.GetKey(bwKey) && GameManager.canWalkSi)
                {
                    isMovingFW = true;
                    if (!Input.GetKey(crouchKey))
                        plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
                }
                if (Input.GetKey(bwKey) && !Input.GetKey(fwKey) && GameManager.canWalkSi)
                {
                    isMovingFW = false;
                    if (!Input.GetKey(crouchKey))
                        plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
                }
                if (Input.GetKey(fwKey) && isMovingFW && GameManager.canWalkSi && !Input.GetKey(crouchKey))
                {
                    if (Input.GetKey(bwKey))
                    {
                        plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
                    }
                }
                if (Input.GetKey(bwKey) && !isMovingFW && GameManager.canWalkSi && !Input.GetKey(crouchKey))
                {
                    if (Input.GetKey(fwKey))
                    {
                        plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
                    }
                }
            }
            if (!isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (isMovingFW)
            {
                plGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }


            //Raycast

            onGround = Physics2D.Raycast(transform.position + colliderOffset + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
Physics2D.Raycast(transform.position + colliderOffset * 3.4f + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer) ||
Physics2D.Raycast(transform.position - colliderOffset * 1.5f + Vector3.up * gizmosY + Vector3.right * gizmosX, Vector2.down, floorDist, floorLayer);

        }
    }



    //Movement functions

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isNearEnemy = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isNearEnemy = false;
        }
    }


    //Sounds functions

    public void akReloadSound()
    {
        soundContrGO.GetComponent<SoundController>().akReloadS();
    }

    public void runSound()
    {
        soundContrGO.GetComponent<SoundController>().RunS();
    }

    public void jumpSound()
    {
        soundContrGO.GetComponent<SoundController>().JumpS();
    }

    public void siVzmahSound()
    {
        soundContrGO.GetComponent<SoundController>().siVzmahS();
    }

    public void siHitSound()
    {
        soundContrGO.GetComponent<SoundController>().siHitS();
    }





    //Gizmos
    //RiflerGizmos
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + colliderOffset + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + colliderOffset + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position - colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position - colliderOffset * 2 + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position - colliderOffset * 0.5f + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position - colliderOffset * 0.5f + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //}

    //SniperGizmos
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + colliderOffset * 2 + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position - colliderOffset * 2 + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position - colliderOffset * 2 + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //}

    //SicklerGizmos
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + colliderOffset + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + colliderOffset + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position + colliderOffset * 3.4f + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + colliderOffset * 3.4f + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //    Gizmos.DrawLine(transform.position - colliderOffset * 1.5f + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position - colliderOffset * 1.5f + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    //}
}
