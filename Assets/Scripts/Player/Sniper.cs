using System.Collections;
using UnityEngine;

public class Sniper : Player
{
    public static Sniper Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        plRB = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.GetMask("Floor");

        canMove = true;
        isMovingForward = true;

        HPMax = 60;
        HP = HPMax;
        ammoInMag = 5;
        ammoInStock = 17;
        damage = 18;
        damageHS = 25;
        attackRate = 1f;
        ammoMax = 5;
        reloadTime = 1;
        gizmosX = 0f;
        gizmosY = 2f;
    }

    void Update()
    {
        // Movement

        if (!sniperIsDead)
        {
            Move(4);
            Movement();
        }

        // HP display
        GameManager.Instance.HPBarFill(HP, 1f / HPMax);

        // Death
        if (HP <= 0)
        {
            Death();
        }
        else
        {
            sniperIsDead = false;
        }

        // Reload
        if (ammoInStock > 0 && ammoInMag < ammoMax)
        {
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && !GameManager.Instance.isPaused || ammoInMag <= 0)
            {
                StartCoroutine(EmptyMagDrop());
                StartCoroutine(Reload(ammoMax, reloadTime));
            }
        }

        // Attack

        if (ammoInMag > 0)
        {
            if (Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.sniperCanAttack && !reloading && !GameManager.Instance.isPaused)
            {
                SoundController.Instance.SvdShoot();
                Anim.Attack();
                StartCoroutine(Anim.AttackOff());

                GameManager.Instance.FireRateControlTransition(attackRate, 1);
                ammoInMag--;

                // Set bullet spawn position and direction

                Vector3 pos = Vector3.zero;
                float direction = 1f;
                float angle = 0f;

                if (Input.GetKey(InputManager.IM.crouchKey) && Player.onGround)
                {
                    StartCoroutine(CrouchFireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.0f, 0);
                        direction = 1f;
                        angle = 0f;
                    }
                    else
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.0f, 0);
                        direction = -1f;
                        angle = 180f;
                    }
                }
                else
                {
                    StartCoroutine(FireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.67f, 0);
                        direction = 1f;
                        angle = 0f;
                    }
                    else
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.67f, 0);
                        direction = -1f;
                        angle = 180f;
                    }
                }

                // Spawn bullet
                GameObject bulletClone = Instantiate(bulletPrefab, pos, Quaternion.Euler(0f, angle, 0f));
                Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
                bulletRb.velocity = 45 * direction * Vector3.right;
            }
        }

        // Empty

        if (Input.GetKey(InputManager.IM.attackKey) && ammoInStock <= 0 && ammoInMag <= 0 && !emptySoundCooldown && !GameManager.Instance.isPaused)
        {
            StartCoroutine(EmptyMagSound());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HPBonus"))
        {
            Destroy(col.gameObject);
            HPBonus();
            HPLimit(60);
        }
    }

    public override void Death()
    {
        base.Death();
        sniperIsDead = true;
    }


    private IEnumerator EmptyMagDrop()
    {
        yield return new WaitForSeconds(0.15f);

        GameObject cloneGO = null;
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        if (Player.isMovingForward)
        {
            rot = Quaternion.Euler(0, 0, 0);

            if (Input.GetKey(InputManager.IM.crouchKey))
            {
                pos = player.transform.position + new Vector3(1.8f, 2.5f);
            }
            else
            {
                pos = player.transform.position + new Vector3(1.1f, 3);
            }
        }
        else
        {
            rot = Quaternion.Euler(0, 180, 0);
            if (Input.GetKey(InputManager.IM.crouchKey))
            {
                pos = player.transform.position + new Vector3(-1.8f, 2.5f);
            }
            else
            {
                pos = player.transform.position + new Vector3(-1.1f, 3);
            }
        }

        cloneGO = Instantiate(magPrefab, pos, rot);
        Destroy(cloneGO, 0.85f);
    }
}
