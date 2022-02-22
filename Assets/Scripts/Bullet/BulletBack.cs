using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBack : MonoBehaviour
{
    public Rigidbody2D pulyaBack;
    public static bool doesBulletHit = false;

    void Start()
    {
        pulyaBack.velocity = new Vector2(-45, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            doesBulletHit = true;
        }
    }
}
