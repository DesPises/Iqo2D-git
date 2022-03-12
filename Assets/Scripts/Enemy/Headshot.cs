using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    // Get increased headshot damage from bullet
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            enemy.GetComponent<Enemy>().HeadDMG();
            Destroy(col.gameObject);
        }
    }
}
