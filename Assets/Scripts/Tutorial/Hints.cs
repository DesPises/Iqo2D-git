using System.Collections;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public GameObject smallTarget, medTarget, bigTarget;
    public static int hint = 0;
    public int hpST = 3, hpMT = 12, hpBT = 25;
    public static bool doesBulletHitTarget;
    public Collider2D BoxCollider;

    void Start()
    {
        smallTarget.SetActive(false);
        medTarget.SetActive(false);
        bigTarget.SetActive(false);
    }


    void Update()
    {
        if (Tutorial.hint == 1)
        {
            hint = 1;
        }
        if (hint == 1)
        {
            smallTarget.SetActive(true);
        }

        if (hpST < 1 && smallTarget.activeSelf == true)
        {
            hint = 2;
        }
        if (hint == 2)
        {
            smallTarget.SetActive(false);
            BoxCollider.enabled = false;
        }

        if (hint == 3)
        {
            medTarget.SetActive(true);
            BoxCollider.enabled = true;
            BoxCollider.offset = new Vector2(8, -2);
        }

        if (Tutorial.hint == 4)
        {
            hint = 4;
        }
        if (hint == 4)
        {
            StartCoroutine(medTargetDies());
        }

        if (Tutorial.hint == 5)
        {
            hint = 5;
        }
        if (hint == 5)
        {
            bigTarget.SetActive(true);
            BoxCollider.enabled = true;
            BoxCollider.offset = new Vector2(10, 2.8f);
        }

        if (bigTarget.activeSelf == true && doesBulletHitTarget)
        {
            hint = 6;
        }
        if (hint == 6)
        {
            bigTarget.SetActive(false);
        }
        

        if (smallTarget.activeSelf == true && doesBulletHitTarget)
        {
            StartCoroutine(hpSTminus());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            doesBulletHitTarget = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            doesBulletHitTarget = false;
        }
    }

    IEnumerator hpSTminus()
    {
        hpST -= 2;
        yield return null;
    }

    IEnumerator medTargetDies()
    {
        yield return new WaitForSeconds(0.25f);
        medTarget.SetActive(false);
        BoxCollider.enabled = false;
    }
}
