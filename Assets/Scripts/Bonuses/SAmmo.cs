using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAmmo : MonoBehaviour
{
    public GameObject sAmmoGO, soundContrGO;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Star.sHadAmmo += 5;
            soundContrGO.GetComponent<SoundController>().ammoS();
            GameManager.Instance.bulletsSAtAllInt += 5;
            Destroy(sAmmoGO);
        }
    }
}
