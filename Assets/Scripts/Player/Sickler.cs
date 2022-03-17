using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickler : Player
{
    public static Sickler Instance { get; private set; }

    private float attackCooldown;
    private readonly float defaultAttackCooldown = 0.27f;
    private readonly float attackRange = 2.3f;

    public Transform attackPos;
    private LayerMask enemy;
    private LayerMask BossLayer;
    public GameObject BossGO;


    void Start()
    {
        Instance = this;

        HPMax = 140;
        HP = HPMax;
        damage = 12;

        enemy = LayerMask.GetMask("Enemy");
        BossLayer = LayerMask.GetMask("Boss");
    }

    public void SicklerAttack()
    {
        if (attackCooldown <= 0)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage();
            }
            attackCooldown = defaultAttackCooldown;
        }
    }

    void Update()
    {
        GameManager.Instance.HPBarFill(HP, 1 / HPMax);

        if (HP <= 0)
        {
            Death();
        }
        else
        {
            sicklerIsDead = false;
        }

        // Decrease cooldown by time
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        // Attack Boss
        if (BossGO != null && Physics2D.OverlapCircle(attackPos.position, attackRange, BossLayer) && Input.GetKeyDown(InputManager.IM.attackKey) && attackCooldown <= 0)
        {
            BossGO.GetComponent<Boss>().DamageFromSickler();
        }

        // Attack
        if (Input.GetKeyDown(InputManager.IM.attackKey) && canAttack && !PauseMenu.isPaused)
        {
            StartCoroutine(Attack());
        }
    }

    public override void Death()
    {
        base.Death();
        sicklerIsDead = true;
    }

    IEnumerator Attack()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        //Player.secJump = false;
        if (Player.isMovingForward)
        {
            rb.velocity = new Vector2(2, 0);
        }
        if (!Player.isMovingForward)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        rb.AddForce(Vector2.down * 40 + Vector2.right * 50, ForceMode2D.Impulse);

        canAttack = false;

        yield return new WaitForSeconds(0.4f);

        canAttack = true;
    }

    // If big enemy's head is in sickler attack area and player press attack key, chop enemy's head
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyHead") && Input.GetKeyDown(InputManager.IM.attackKey) && attackCooldown <= 0)
        {
            col.gameObject.GetComponentInParent<Enemy>().HeadOff();
        }
    }

    // Draw circle of attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
