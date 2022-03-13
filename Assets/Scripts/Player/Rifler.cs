using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifler : Player
{
    public static Rifler Instance { get; private set; }

    void Start()
    {
        Instance = this;
        HP = 100;
    }

    void Update()
    {
        GameManager.Instance.HPBarFill(HP, 0.01f);

        if (HP <= 0)
        {
            Death();
        }
        else
        {
            riflerIsDead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HPBonus"))
        {
            Destroy(col.gameObject);
            HPBonus();
            HPlimit(100);
        }
    }

    public override void Death()
    {
        base.Death();
        riflerIsDead = true;
    }
}
