using UnityEngine;

public class Rifler : Player
{
    public static Rifler Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        player = gameObject;
        plRB = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.GetMask("Floor");

        canMove = true;
        isMovingForward = true;

        HPMax = 100;
        HP = HPMax;
        ammoInMag = 30;
        ammoInStock = 60;
        damage = 2;
        damageHS = 3;
        attackRate = 0.36f;
        ammoMax = 30;
        reloadTime = 0.75f;
        gizmosX = 0.3f;
        gizmosY = 1.95f;
    }

    void Update()
    {
        // Movement

        if (!riflerIsDead)
        {
            Move(5);
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
            riflerIsDead = false;
        }

        // Reload
        if (ammoInStock > 0 && ammoInMag < 30)
        {
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && !GameManager.Instance.isPaused || ammoInMag <= 0)
            {
                EmptyMagDrop();
                StartCoroutine(Reload(ammoMax, reloadTime));
            }
        }

        // Attack

        if (ammoInMag > 0)
        {
            if (Input.GetKey(InputManager.IM.attackKey) && GameManager.riflerCanAttack && !reloading && !GameManager.Instance.isPaused)
            {
                SoundController.Instance.AkShoot();
                Anim.Attack();

                GameManager.Instance.FireRateControlTransition(attackRate, 2);
                ammoInMag--;

                // Set bullet spawn position and direction

                Vector3 pos = Vector3.zero;
                float direction = 1;

                if (Input.GetKey(InputManager.IM.crouchKey) && Player.onGround)
                {
                    StartCoroutine(CrouchFireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.0f);
                        direction = 1;
                    }
                    if (!Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.0f);
                        direction = -1;
                    }
                }
                else
                {
                    StartCoroutine(FireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.75f);
                        direction = 1;
                    }
                    if (!Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.75f);
                        direction = -1;
                    }
                }

                // Spawn bullet
                GameObject bulletClone = Instantiate(bulletPrefab, pos, Quaternion.identity);
                Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
                bulletRb.velocity = 45 * direction * Vector3.right;
            }
        }
        if (Input.GetKeyUp(InputManager.IM.attackKey) || reloading)
        {
            StartCoroutine(Anim.AttackOff());
        }

        // Empty

        if (Input.GetKey(InputManager.IM.attackKey) && ammoInStock <= 0 && ammoInMag <= 0 && !emptySoundCooldown && !GameManager.Instance.isPaused)
        {
            StartCoroutine(EmptyMagSound());
            StartCoroutine(Anim.AttackOff());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HPBonus"))
        {
            Destroy(col.gameObject);
            HPBonus();
            HPLimit(100);
        }
    }

    public override void Death()
    {
        base.Death();
        riflerIsDead = true;
    }

    private void EmptyMagDrop()
    {
        GameObject cloneGO = null;
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        if (Player.isMovingForward)
        {
            pos = player.transform.position + new Vector3(0.6f, 2.9f, 0);
            rot = Quaternion.Euler(0, 0, 8);
        }
        else
        {
            pos = player.transform.position + new Vector3(-0.6f, 2.9f, 0);
            rot = Quaternion.Euler(0, 180, 8);
        }

        cloneGO = Instantiate(magPrefab, pos, rot);
        Destroy(cloneGO, 1f);
    }
}
