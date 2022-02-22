using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siHeadshotArea : MonoBehaviour
{
    public KeyCode attackKey;

    void Start()
    {
        attackKey = attackKey = InputManager.IM.attackKey; ;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "EnemyHead" && Input.GetKeyDown(attackKey) && siAttack.cdsi <= 0)
        {
            col.gameObject.GetComponent<Headshot>().siIsNearHead();
        }
    }
}
