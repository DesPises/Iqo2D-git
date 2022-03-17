using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifler : Player
{
    public static Rifler Instance { get; private set; }

    void Start()
    {
        Instance = this;
        player = gameObject;

        HPMax = 100;
        HP = HPMax;
        ammoInMag = 30;
        ammoInStock = 75;
        damage = 2;
        damageHS = 3;
        attackRate = 0.36f;
        ammoMax = 30;
        reloadTime = 0.75f;
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
            riflerIsDead = false;
        }

        // Reload
        if (ammoInStock > 0 && ammoInMag < 30)
        {
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && !PauseMenu.isPaused || ammoInMag <= 0)
            {
                EmptyMagDrop();
                StartCoroutine(Reload(ammoMax, reloadTime));
            }
        }

        // Attack

        if (ammoInMag > 0)
        {
            if (Input.GetKey(InputManager.IM.attackKey) && canAttack && !reloading && !PauseMenu.isPaused)
            {
                SoundController.Instance.akShootS();

                StartCoroutine(FireRateControl(attackRate));

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
            HPlimit(100);
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
