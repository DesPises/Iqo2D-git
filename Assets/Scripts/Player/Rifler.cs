using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifler : Player
{
    public static Rifler Instance { get; private set; }

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject magPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private SpriteRenderer[] fireAnimationPics;
    [SerializeField] private SpriteRenderer[] crouchFireAnimationPics;

    private readonly Color invisible = new(255, 255, 255, 0);
    private readonly Color visible = new(255, 255, 255, 190);

    private bool canAttack;
    private bool reloading;

    public int ammoInMag;
    public int ammoInStock;
    public int damage;
    public int damageHS;


    void Start()
    {
        Instance = this;

        HP = 100;
        damage = 2;
        damageHS = 3;
        ammoInMag = 30;
        ammoInStock = 75;
        canAttack = true;

    }

    void Update()
    {
        // HP display
        GameManager.Instance.HPBarFill(HP, 0.01f);

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
                StartCoroutine(Reload());
            }
        }

        // Attack

        if (ammoInMag > 0)
        {
            if (Input.GetKey(InputManager.IM.attackKey) && canAttack && !reloading && !PauseMenu.isPaused)
            {
                AkShootSound();
                StartCoroutine(FireRateControl());

                Vector3 pos = Vector3.zero;
                float direction = 1;

                // Set bullet spawn position and direction
                if (Input.GetKey(InputManager.IM.crouchKey) && Player.onGround)
                {
                    StartCoroutine(CrouchFireAnimation());
                    if (Player.isMovingFW)
                    {
                        pos = player.position + new Vector3(0.5f, 4.0f, 0);
                        direction = 1;
                    }
                    if (!Player.isMovingFW)
                    {
                        pos = player.position + new Vector3(-0.5f, 4.0f, 0);
                        direction = -1;
                    }
                }
                else
                {
                    StartCoroutine(FireAnimation());
                    if (Player.isMovingFW)
                    {
                        pos = player.position + new Vector3(0.5f, 4.75f, 0);
                        direction = 1;
                    }
                    if (!Player.isMovingFW)
                    {
                        pos = player.position + new Vector3(-0.5f, 4.75f, 0);
                        direction = -1;
                    }
                }

                // Spawn bullet
                GameObject bulletClone = Instantiate(bulletPrefab, pos, Quaternion.identity);
                Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
                bulletRb.velocity = 45 * direction * Vector3.right;
            }
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

    public void AkShootSound()
    {
        SoundController.Instance.akShootS();
    }

    IEnumerator FireRateControl()
    {
        canAttack = false;
        yield return null;
        ammoInMag--;
        yield return new WaitForSeconds(0.36f);
        canAttack = true;
    }
    IEnumerator FireAnimation()
    {
        for (int i = 0; i < fireAnimationPics.Length; i++)
        {
            fireAnimationPics[i].color = visible;
            yield return new WaitForSeconds(0.03f);
            fireAnimationPics[i].color = invisible;
        }
    }
    IEnumerator CrouchFireAnimation()
    {
        for (int i = 0; i < crouchFireAnimationPics.Length; i++)
        {
            crouchFireAnimationPics[i].color = visible;
            yield return new WaitForSeconds(0.03f);
            crouchFireAnimationPics[i].color = invisible;
        }
    }

    IEnumerator Reload()
    {
        int leftInMag = ammoInMag;

        if (ammoInMag + ammoInStock > 30)
        {
            ammoInMag = 30;
            ammoInStock -= (30 - leftInMag);
        }
        else
        {
            ammoInMag += ammoInStock;
            ammoInStock = 0;
        }
        reloading = true;
        yield return new WaitForSeconds(0.75f);
        reloading = false;
    }
    private void EmptyMagDrop()
    {
        if (Player.isMovingFW)
        {
            GameObject cloneGO = Instantiate(magPrefab, player.position + new Vector3(0.6f, 2.9f, 0), Quaternion.Euler(0, 0, 8));
            Destroy(cloneGO, 1f);
        }
        else
        {
            GameObject cloneGO = Instantiate(magPrefab, player.position + new Vector3(-0.6f, 2.9f, 0), Quaternion.Euler(0, 180, 8));
            Destroy(cloneGO, 1f);
        }
    }
}
