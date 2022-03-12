using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siHeadshotArea : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyHead") && Input.GetKeyDown(InputManager.IM.attackKey) && siAttack.cdsi <= 0)
        {
            col.gameObject.GetComponentInParent<Enemy>().HeadOff();
        }
    }
}
