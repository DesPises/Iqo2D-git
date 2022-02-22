using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFarZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInFarAttackZone = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInFarAttackZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Boss.playerInFarAttackZone = false;
        }
    }
}
