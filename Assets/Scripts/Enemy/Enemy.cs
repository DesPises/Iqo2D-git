using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject explosion;
    [SerializeField] private bool reverseRotation;
    [SerializeField] private int HP;
    [SerializeField] private int speed;
    [SerializeField] private int damage;

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Near Player Check")]
    [SerializeField] private Vector2 size;
    [SerializeField] private Transform pos;
    private LayerMask playerLayer;
    private bool isNearPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerLayer = LayerMask.GetMask("Player");
    }

    void Update()
    {
        //Near player check
        isNearPlayer = Physics2D.OverlapBox(pos.position, size, 0f, playerLayer);

        //Move
        if (!isNearPlayer && HP > 0)
        {
            float posX = transform.position.x;

            if (reverseRotation)
            {
                if (posX > Player.posX)
                {
                    rb.velocity = new Vector2(-speed, -1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (posX < Player.posX)
                {
                    rb.velocity = new Vector2(speed, -1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            else
            {
                if (posX > Player.posX)
                {
                    rb.velocity = new Vector2(-speed, -1);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (posX < Player.posX)
                {
                    rb.velocity = new Vector2(speed, -1);
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
        }

        //Attack
        if (isNearPlayer)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetBool("hit", true);
        }
        else
        {
            anim.SetBool("hit", false);
        }

        //Death
        if (HP <= 0)
        {
            anim.SetBool("die", true);
            StartCoroutine(Death());
            rb.velocity = new Vector2(0, 0);
        }
    }

    // Check if any bullet collides with Body
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit from rifler
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GetDamage(Rifler.Instance.damage));
            Destroy(collision.gameObject);
        }
        // Hit from sniper
        else if (collision.gameObject.CompareTag("SniperBullet"))
        {
            StartCoroutine(GetDamage(Sniper.Instance.damage));
            Destroy(collision.gameObject);
        }
    }

    // Check if any bullet enters Headshot trigger zone
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Hit from rifler
        if (col.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GetDamage(Rifler.Instance.damageHS));
            Destroy(col.gameObject);
        }
        // Hit from sniper
        else if (col.gameObject.CompareTag("SniperBullet"))
        {
            StartCoroutine(GetDamage(Sniper.Instance.damageHS));
            Destroy(col.gameObject);
        }
    }

    // Damage and death

    public IEnumerator GetDamage(int damage)
    {
        DmgSound();
        BloodOn();
        anim.SetBool("dmg", true);
        yield return null;
        anim.SetBool("dmg", false);
        HP -= damage;
    }

    private IEnumerator Death()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public void BloodOn()
    {
        GameObject bloodClone = Instantiate(blood, blood.transform.position, Quaternion.identity);
        Destroy(bloodClone, 0.25f);
    }

    public IEnumerator Explosion()
    {
        if (explosion)
        {
            StartCoroutine(Death());
            explosion.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            explosion.SetActive(false);
        }
    }

    public void HeadOff()
    {
        rb.velocity = new Vector2(0, 0);
        anim.SetBool("die", false);
        anim.SetBool("headOff", true);
        StartCoroutine(Death());
    }

    public void DealDamage()
    {
        if (Player.character == "Rifler")
        {
            Rifler.Instance.GetDamage(damage);
        }
        if (Player.character == "Sniper")
        {
            Sniper.Instance.GetDamage(damage);
        }
        if (Player.character == "Sickler")
        {
            Sickler.Instance.GetDamage(damage);
        }
    }

    // Near player check gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, size);
    }

    //Sounds

    public void DmgSound()
    {
        SoundController.Instance.AlienHit();
    }

    public void DeathSound()
    {
        SoundController.Instance.AlienDeath();
    }

    public void ExplosionSound()
    {
        SoundController.Instance.Explosion();
    }

    public void LaserSound()
    {
        SoundController.Instance.Laser();
    }

    public void DamageFromSicklerSound()
    {
        SoundController.Instance.SickleHit();
    }
}
