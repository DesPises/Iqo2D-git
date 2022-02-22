using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    public GameObject enemyGO;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            enemyGO.GetComponent<Enemy>().HeadDMG();
            Destroy(col.gameObject);
        }
    }

    //Sickler headshot

    public void siIsNearHead()
    {
        enemyGO.GetComponent<Enemy>().siIsNearHead();
    }
}
