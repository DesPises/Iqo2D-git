using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Player
{
    public static Sniper Instance { get; private set; }

    void Start()
    {
        Instance = this;
        HP = 60;
    }

    void Update()
    {
        GameManager.Instance.HPBarFill(HP, 0.0167f);

        if (HP <= 0)
        {
            Death();
        }
        else
        {
            sniperIsDead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("HPBonus"))
        {
            Destroy(col.gameObject);
            HPBonus();
            HPlimit(60);
        }
    }

    public override void Death()
    {
        base.Death();
        sniperIsDead = true;
    }
}
