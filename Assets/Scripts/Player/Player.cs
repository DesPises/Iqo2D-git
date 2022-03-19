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

    protected LayerMask floorLayer;

    protected Rigidbody2D plRB;

    [SerializeField] private float gizmosY;
    [SerializeField] private float gizmosX;

    private readonly float jumpForce = 200;
    private readonly float floorDist = 1;

    protected int HP;
    protected int HPMax;
    protected float attackRate;
    protected int ammoMax;
    protected float reloadTime;

    protected bool secJump;
    public bool isBonusActive;
    protected bool canMove;

    public static string character;
    public static float coordinateX;

    public static bool isMovingForward = true;
    public static bool onGround;
    public static bool riflerIsDead;
    public static bool sniperIsDead;
    public static bool sicklerIsDead;

    protected bool canAttack;
    protected bool reloading;
    protected bool emptySoundCooldown;

    public int ammoInMag { get; protected set; }
    public int ammoInStock;
    protected int damage;
    protected int damageHS;

    protected readonly Color invisible = new(255, 255, 255, 0);
    protected readonly Color visible = new(255, 255, 255, 190);

    protected void Movement()
    {
        if (!GameManager.Instance.isPaused)
        {
            // Link player's pos to variable
            coordinateX = transform.position.x;

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
        }
    }

    public virtual void Death()
    {
        CharacterChangeCode.canChange = true;
    }

    public void HPBonus()
    {
        HP += 35;
        SoundController.Instance.HPBonus();
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

    public void RefillHPToFull(int full)
    {
        HP = full;
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

    protected void Move(int moveSpeed)
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

    protected void SecJump()
    {
        SoundController.Instance.JumpS();
        secJump = false;
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

    //Sounds methods for animations keys

    public void AkReloadSound()
    {
        SoundController.Instance.AkReload();
    }

    public void SvdReloadSound()
    {
        SoundController.Instance.SvdReload();
    }

    public void RunSound()
    {
        SoundController.Instance.Run();
    }

    public void SickleSound()
    {
        SoundController.Instance.Sickle();
    }

    public void SiHitSound()
    {
        SoundController.Instance.SickleHit();
    }

    protected IEnumerator EmptyMagSound()
    {
        emptySoundCooldown = true;
        SoundController.Instance.EmptyMag();
        yield return new WaitForSeconds(0.4f);
        emptySoundCooldown = false;
    }

    public void DmgSound()
    {
        SoundController.Instance.Damage();
    }

    public void AlienHitSound()
    {
        SoundController.Instance.AlienHit();
    }


    // Draw raycast of ground check

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * gizmosY + Vector3.right * gizmosX, transform.position + Vector3.down * floorDist + Vector3.up * gizmosY + Vector3.right * gizmosX);
    }
}
