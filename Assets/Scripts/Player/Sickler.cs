using UnityEngine;

public class Sickler : Player
{
    public static Sickler Instance { get; private set; }

    private readonly float fireRate = 0.27f;
    private readonly float attackRange = 2.3f;

    public Transform attackPos;
    private LayerMask enemy;

    private bool inEnemyHeadArea;
    private GameObject enemyHead;

    void Awake()
    {
        Instance = this;
        player = gameObject;
        plRB = GetComponent<Rigidbody2D>();
        floorLayer = LayerMask.GetMask("Floor");
        canMove = true;
        isMovingForward = true;

        HPMax = 140;
        HP = HPMax;
        damage = 12;
        gizmosX = -0.45f;
        gizmosY = 3.8f;

        enemy = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        // Movement

        if (!sicklerIsDead && canMove)
        {
            Move(6);
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
            sicklerIsDead = false;
        }

        // Attack Boss
        if (Boss.Instance)
        {
            if (Physics2D.OverlapCircle(attackPos.position, attackRange, LayerMask.GetMask("Boss")) && Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.sicklerCanAttack)
            {
                StartCoroutine(Boss.Instance.GetDamage(2));
            }
        }
        
        // Attack
        if (Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.sicklerCanAttack && !GameManager.Instance.isPaused)
        {
            Attack();
            if (inEnemyHeadArea)
            {
                enemyHead.GetComponentInParent<Enemy>().HeadOff();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HPBonus"))
        {
            Destroy(col.gameObject);
            HPBonus();
            HPLimit(140);
        }
        else if (col.gameObject.CompareTag("EnemyHead"))
        {
            inEnemyHeadArea = true;
            enemyHead = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyHead"))
        {
            inEnemyHeadArea = false;
        }
    }

    public override void Death()
    {
        base.Death();
        sicklerIsDead = true;
    }

    private void Attack()
    {
        GameManager.Instance.FireRateControlTransition(fireRate, 3);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Anim.Attack();
        StartCoroutine(Anim.AttackOff());

        secJump = false;
        if (isMovingForward)
        {
            rb.velocity = new Vector2(2, 0);
            rb.AddForce(Vector2.down * 40 + Vector2.right * 50, ForceMode2D.Impulse);
        }
        else
        {
            rb.velocity = new Vector2(-2, 0);
            rb.AddForce(Vector2.down * 40 + Vector2.left * 50, ForceMode2D.Impulse);
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemyScript = enemies[i].GetComponent<Enemy>();
                StartCoroutine(enemyScript.GetDamage(damage));
            }
        }
    }

    // Draw circle of attack range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
