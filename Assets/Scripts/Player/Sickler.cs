using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickler : Player
{
    public static Sickler Instance { get; private set; }

    private float cooldownF;
    private readonly float defCooldownF = 0.27f;
    private readonly float rangeF = 2.3f;
    public static float cdsi;

    public Transform attackPos;
    private LayerMask enemy;
    private LayerMask enemyHead;
    private LayerMask BossLayer;
    public GameObject BossGO;

    void Start()
    {
        Instance = this;
        enemy = LayerMask.GetMask("Enemy");
        enemyHead = LayerMask.GetMask("EnemyHead");
        BossLayer = LayerMask.GetMask("Boss");
    }

    public void SicklerAttack()
    {
        if (cooldownF <= 0)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, rangeF, enemy);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage();
            }
            cooldownF = defCooldownF;
        }
    }

    void Update()
    {
        GameManager.Instance.HPBarFill(HP, 0.007f);

        if (HP <= 0)
        {
            Death();
        }
        else
        {
            sicklerIsDead = false;
        }

        // Decrease cooldown by time
        if (cooldownF > 0)
        {
            cooldownF -= Time.deltaTime;
            cdsi = cooldownF;
        }

        // Attack Boss
        if (BossGO != null && Physics2D.OverlapCircle(attackPos.position, rangeF, BossLayer) && Input.GetKeyDown(InputManager.IM.attackKey) && cooldownF <= 0)
        {
            BossGO.GetComponent<Boss>().DamageFromSickler();
        }
    }

    public override void Death()
    {
        base.Death();
        sicklerIsDead = true;
    }

    public void Immortality()
    {
        HP = 99999999;
    }

    // Draw circle of attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, rangeF);
    }
}
