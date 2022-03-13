using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAmmo : MonoBehaviour
{
    public GameObject rAmmoGO, soundContrGO;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Instance.canAttackR = true;
            soundContrGO.GetComponent<SoundController>().ammoS();
            GameManager.Instance.bulletsRAtAllInt += 30;
            Destroy(rAmmoGO);
        }
    }
}
