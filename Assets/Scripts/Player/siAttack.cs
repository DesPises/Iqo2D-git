using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siAttack : MonoBehaviour
{
    public float cooldownF, defCooldownF, rangeF;
    public static float cdsi;
    public Transform attackPos;
    public LayerMask enemy, enemyHead, BossLayer;
    public KeyCode attackKey;
    public GameObject BossGO;

    void Start()
    {
        attackKey = InputManager.IM.attackKey;
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
            cdsi = cooldownF;
        }
    }

    void Update()
    {
        if (cooldownF > 0)
        {
            cooldownF -= Time.deltaTime;
            cdsi = cooldownF;
        }


        if (BossGO != null && Physics2D.OverlapCircle(attackPos.position, rangeF, BossLayer) && Input.GetKeyDown(attackKey) && cooldownF <= 0)
        {
            BossGO.GetComponent<Boss>().DamageFromSickler();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, rangeF);
    }
}
