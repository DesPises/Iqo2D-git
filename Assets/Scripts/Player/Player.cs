using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected Animations Anim;

    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject magPrefab;
    [SerializeField] protected GameObject player;

    [SerializeField] protected SpriteRenderer[] fireAnimationPics;
    [SerializeField] protected SpriteRenderer[] crouchFireAnimationPics;

    private LayerMask floorLayer;

    private Rigidbody2D plRB;

    [SerializeField] private float gizmosY;
    [SerializeField] private float gizmosX;

    private readonly float jumpForce = 200;
    private readonly float floorDist = 1;

    protected int HP;
    protected int HPMax;
    protected float attackRate;
    protected int ammoMax;
    protected float reloadTime;

    private bool secJump;
    private bool isBonusActive;
    private bool canMove;

    public static string character;
    public static float plCoordinateX;

    public static bool isMovingForward = true;
    public static bool onGround;
    public static bool riflerIsDead;
    public static bool sniperIsDead;
    public static bool sicklerIsDead;

    protected bool canAttack;
    protected bool reloading;
    protected bool emptySoundCooldown;

    public int ammoInMag { get; protected set; }
    public int ammoInStock { get; protected set; }
    protected int damage;
    protected int damageHS;

    protected readonly Color invisible = new(255, 255, 255, 0);
    protected readonly Color visible = new(255, 255, 255, 190);

    private void Start()
    {
        floorLayer = LayerMask.GetMask("Floor");
        plRB = GetComponent<Rigidbody2D>();
        canMove = true;

        isMovingForward = true;
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
            else
            {
                Anim.CrouchOff();
            }

            // Characters specifications

            if (character == "Rifler" && !riflerIsDead)
            {
                Move(5);
            }

            if (character == "Sniper" && !sniperIsDead)
            {
                Move(4);
            }

            if (character == "Sickler" && !sicklerIsDead && canMove)
            {
                Move(6);
            }
        }
    }

    public virtual void Death()
    {
        CharacterChangeCode.canChange = true;
    }

    public void HPBonus()
    {
        HP += 35;
    }

    public void GetDamage(int damage)
    {
        HP -= damage;
    }

    protected void HPlimit(int limit)
    {
        if (HP > limit)
        {
            HP = limit;
        }
    }

    // Bonuses

    public void RefillHP(int full)
    {
        HP = full;
    }

    public IEnumerator AmmoBonus(int ammo)
    {
        if (Sniper.Instance.isBonusActive)
        {
            while (Sniper.Instance.isBonusActive)
            {
                yield return null;
            }
            ammoInStock += ammo;
        }
        else
        {
            ammoInStock += ammo;
        }
    }

    public IEnumerator DoubleDamage()
    {
        damage = 5;
        damageHS = 7;
        yield return new WaitForSeconds(15);
        damage = 2;
        damageHS = 3;
    }

    public IEnumerator InfiniteAmmo(int time)
    {
        isBonusActive = true;
        int oldAmmo = ammoInStock;
        ammoInStock = 999;
        yield return new WaitForSeconds(time);
        ammoInStock = oldAmmo;
        isBonusActive = false;
    }

    public IEnumerator Immortality()
    {
        HP = 9999999;
        yield return new WaitForSeconds(15);
        HP = HPMax;
    }

    // Shoot methods

    protected IEnumerator FireRateControl(float fireRate)
    {
        canAttack = false;
        yield return null;
        ammoInMag--;
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
    }

    protected IEnumerator FireAnimation()
    {
        for (int i = 0; i < fireAnimationPics.Length; i++)
        {
            fireAnimationPics[i].color = visible;
            yield return new WaitForSeconds(0.03f);
            fireAnimationPics[i].color = invisible;
        }
    }
    protected IEnumerator CrouchFireAnimation()
    {
        for (int i = 0; i < crouchFireAnimationPics.Length; i++)
        {
            crouchFireAnimationPics[i].color = visible;
            yield return new WaitForSeconds(0.03f);
            crouchFireAnimationPics[i].color = invisible;
        }
    }
    protected IEnumerator Reload(int ammoMax, float reloadTime)
    {
        int leftInMag = ammoInMag;

        if (ammoInMag + ammoInStock > ammoMax)
        {
            ammoInMag = ammoMax;
            ammoInStock -= (ammoMax - leftInMag);
        }
        else
        {
            ammoInMag += ammoInStock;
            ammoInStock = 0;
        }
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    //Movement methods

    private void Move(int moveSpeed)
    {
        if (!Input.GetKey(InputManager.IM.crouchKey))
        {
            if (Input.GetKey(InputManager.IM.fwKey) && !Input.GetKey(InputManager.IM.bwKey))
            {
                Anim.Run();
                isMovingForward = true;
                plRB.velocity = new Vector2(moveSpeed, plRB.velocity.y);
            }
            else if (Input.GetKey(InputManager.IM.bwKey) && !Input.GetKey(InputManager.IM.fwKey))
            {
                Anim.Run();
                isMovingForward = false;
                plRB.velocity = new Vector2(-moveSpeed, plRB.velocity.y);
            }
            else
            {
                Anim.RunOff();
            }
        }
        else
        {
            if (Input.GetKeyDown(InputManager.IM.fwKey))
            {
                isMovingForward = true;
            }
            else if (Input.GetKeyDown(InputManager.IM.bwKey))
            {
                isMovingForward = false;
            }
        }
    }

    void Jump()
    {
        SoundController.Instance.JumpS();

        StartCoroutine(Anim.Jump());
        secJump = true;
        plRB.velocity = new Vector2(plRB.velocity.x, 0);
        plRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void SecJump()
    {
        SoundController.Instance.JumpS();

        StartCoroutine(Anim.SecJump());
        plRB.velocity = new Vector2(plRB.velocity.x, 0);
        plRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Crouch()
    {
        Anim.Crouch();
        plRB.velocity = new Vector2(0, 0);
    }
    private void RotationControl()
    {
        if (character != "Sniper")
        {
            if (!isMovingForward)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (isMovingForward)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (!isMovingForward)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (isMovingForward)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
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

    public void SickleSound()
    {
        SoundController.Instance.siVzmahS();
    }

    public void SiHitSound()
    {
        SoundController.Instance.siHitS();
    }

    protected IEnumerator EmptyMagSound()
    {
        emptySoundCooldown = true;
        SoundController.Instance.emptyMagS();
        yield return new WaitForSeconds(0.4f);
        emptySoundCooldown = false;
    }

    public void DmgSound()
    {
        SoundController.Instance.dmgS();
    }

    public void AlienHitSound()
    {
        SoundController.Instance.alienHitS();
    }


    // Draw raycast of ground check

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    }
}
