using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCloseZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInCloseAttackZone = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInCloseAttackZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInCloseAttackZone = false;
        }
    }
}
