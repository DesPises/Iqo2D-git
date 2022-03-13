using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public CapsuleCollider2D capsCollider;
    public GameObject enemyGO, bloodGO, explosionGO, soundContrGO;
    public GameObject Bloodclone;
    public KeyCode attackKey;
    public Transform pos;
    public static bool doesHitPlayer, isDamaged, isNearPlayer;
    public bool reverseRotation = false;
    public int HPInt, speed;
    public static int DamageInt, DamageIntHS; //Receive damage
    public int DamageHitInt; //Deal damage
    public Animator nmeAnim;

    public Vector2 size; //Near player check
    public Transform posBox;
    public LayerMask playerLayer;

    public static bool siNearHead; //Sickler headshot


    void Start()
    {
        isNearPlayer = false;
        attackKey = InputManager.IM.attackKey;
    }

    void Update()
    {       
        //Move
        if (!reverseRotation)
        {
            if (transform.position.x > Player.plCoordinateX && !isNearPlayer && HPInt > 0)
            {
                rb.velocity = new Vector2(-speed, -1);
                enemyGO.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (transform.position.x < Player.plCoordinateX && !isNearPlayer && HPInt > 0)
            {
                rb.velocity = new Vector2(speed, -1);
                enemyGO.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        if (reverseRotation)
        {
            if (transform.position.x > Player.plCoordinateX && !isNearPlayer && HPInt > 0)
            {
                rb.velocity = new Vector2(-speed, -1);
                enemyGO.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (transform.position.x < Player.plCoordinateX && !isNearPlayer && HPInt > 0)
            {
                rb.velocity = new Vector2(speed, -1);
                enemyGO.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        //Near player check
        isNearPlayer = Physics2D.OverlapBox(posBox.position, size, 0f, playerLayer);

        //Attack
        if (isNearPlayer)
        {
            rb.velocity = new Vector2(0, 0);
            nmeAnim.SetBool("hit", true);
        }
        if (!isNearPlayer)
        {
            nmeAnim.SetBool("hit", false);
        }

        //Death
        if (HPInt <= 0)
        {
            nmeAnim.SetBool("die", true);
            StartCoroutine(Death());
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(GetDamaged());
    }

  

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "BulletS")
        {
            StartCoroutine(GetDamaged());
        }
    }

    IEnumerator GetDamaged()
    {
        DmgSound();
        BloodOn();
        nmeAnim.SetBool("dmg", true);
        yield return null;
        nmeAnim.SetBool("dmg", false);
        HPInt -= DamageInt;
    }

    public void GetHeadDamage()
    {
        HPInt -= DamageIntHS;
        StartCoroutine(HeadDMGAnim());
    }

    public IEnumerator HeadDMGAnim()
    {
        BloodOn();
        nmeAnim.SetBool("dmg", true);
        yield return null;
        nmeAnim.SetBool("dmg", false);
    }

    IEnumerator Death()
    {
        capsCollider.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(enemyGO);
    }

    public void BloodOn()
    {
        Bloodclone = Instantiate(bloodGO, bloodGO.transform.position, Quaternion.identity);
        StartCoroutine(DestroyBloodClone());
    }
    IEnumerator DestroyBloodClone()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(Bloodclone);
    }
    void Explosion()
    {
        explosionGO.SetActive(true);
        StartCoroutine(ExplosionOff());
    }
    IEnumerator ExplosionOff()
    {
        yield return new WaitForSeconds(0.3f);
        explosionGO.SetActive(false);
    }

    public void DealDamage()
    {
        if (Player.character == "Rifler")
            Rifler.Instance.GetDamage(DamageHitInt);
        if (Player.character == "Sniper")
            Sniper.Instance.GetDamage(DamageHitInt);
        if (Player.character == "Sickler")
            Sickler.Instance.GetDamage(DamageHitInt);
        StartCoroutine(SignalToPlayerGetDamageAnim());
    }

    IEnumerator SignalToPlayerGetDamageAnim()
    {
        doesHitPlayer = true;
        yield return null;
        doesHitPlayer = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(posBox.position, size);
    }

    //Sickler headshot

    public void HeadOff()
    {
        rb.velocity = new Vector2(0, 0);
        nmeAnim.SetBool("die", false);
        nmeAnim.SetBool("headOff", true);
        StartCoroutine(Death());
    }

    //Sounds

    public void DmgSound()
    {
        soundContrGO.GetComponent<SoundController>().alienHitS();
    }

    public void DeathSound()
    {
        soundContrGO.GetComponent<SoundController>().alienDeathS();
    }

    public void ExplosionSound()
    {
        soundContrGO.GetComponent<SoundController>().explosionS();
    }

    public void LaserSound()
    {
        soundContrGO.GetComponent<SoundController>().laserS();
    }

    public void DamageFromSicklerSound()
    {
        soundContrGO.GetComponent<SoundController>().siHitS();
    }
}
