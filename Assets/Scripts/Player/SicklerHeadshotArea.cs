using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SicklerHeadshotArea : MonoBehaviour
{
    // If big enemy's head is in sickler attack area and player press attack key, chop enemy's head
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyHead") && Input.GetKeyDown(InputManager.IM.attackKey) && SicklerAttackScript.cdsi <= 0)
        {
            col.gameObject.GetComponentInParent<Enemy>().HeadOff();
        }
    }
}
