using System.Collections;
using UnityEngine;

public class Sniper : Player
{
    public static Sniper Instance { get; private set; }

    void Start()
    {
        Instance = this;

        HPMax = 60;
        HP = HPMax;
        ammoInMag = 5;
        ammoInStock = 17;
        canAttack = true;
        damage = 18;
        damageHS = 25;
        attackRate = 1f;
        ammoMax = 5;
        reloadTime = 1;
        canAttack = true;
    }

    void Update()
    {
        // HP display
        GameManager.Instance.HPBarFill(HP, 1 / HPMax);

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
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && !PauseMenu.isPaused || ammoInMag <= 0)
            {
                StartCoroutine(EmptyMagDrop());
                StartCoroutine(Reload(ammoMax, reloadTime));
            }
        }

        // Attack

        if (ammoInMag > 0)
        {
            if (Input.GetKeyDown(InputManager.IM.attackKey) && canAttack && !reloading && !PauseMenu.isPaused)
            {
                SoundController.Instance.svdShootS();

                StartCoroutine(FireRateControl(attackRate));

                // Set bullet spawn position and direction

                Vector3 pos = Vector3.zero;
                float direction = 1;

                if (Input.GetKey(InputManager.IM.crouchKey) && Player.onGround)
                {
                    StartCoroutine(CrouchFireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.0f, 0);
                        direction = 1;
                    }
                    if (!Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.0f, 0);
                        direction = -1;
                    }
                }
                else
                {
                    StartCoroutine(FireAnimation());
                    if (Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(0.5f, 4.67f, 0);
                        direction = 1;
                    }
                    if (!Player.isMovingForward)
                    {
                        pos = player.transform.position + new Vector3(-0.5f, 4.67f, 0);
                        direction = -1;
                    }
                }

                // Spawn bullet
                GameObject bulletClone = Instantiate(bulletPrefab, pos, Quaternion.identity);
                Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
                bulletRb.velocity = 45 * direction * Vector3.right;
            }
        }

        // Empty

        if (Input.GetKey(InputManager.IM.attackKey) && ammoInStock <= 0 && ammoInMag <= 0 && !emptySoundCooldown && !PauseMenu.isPaused)
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
            HPlimit(60);
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
                pos = player.transform.position + new Vector3(1, 2.5f);
            }
            else
            {
                pos = player.transform.position + new Vector3(0.4f, 3);
            }
        }
        else
        {
            rot = Quaternion.Euler(0, 180, 0);
            if (Input.GetKey(InputManager.IM.crouchKey))
            {
                pos = player.transform.position + new Vector3(-1, 2.5f);
            }
            else
            {
                pos = player.transform.position + new Vector3(-0.4f, 3);
            }
        }

        cloneGO = Instantiate(magPrefab, pos, rot);
        Destroy(cloneGO, 0.85f);
    }
}
