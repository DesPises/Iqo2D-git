using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBonus : MonoBehaviour
{
    public GameObject HPGO, vodkaGO, shawaGO, kebabGO, soundContrGO;

    void Update()
    {
        if (plMovement.character == "Rifler")
        {
            vodkaGO.SetActive(true);
            shawaGO.SetActive(false);
            kebabGO.SetActive(false);
        }
        if (plMovement.character == "Sniper")
        {
            vodkaGO.SetActive(false);
            shawaGO.SetActive(true);
            kebabGO.SetActive(false);
        }
        if (plMovement.character == "Sickler")
        {
            vodkaGO.SetActive(false);
            shawaGO.SetActive(false);
            kebabGO.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            soundContrGO.GetComponent<SoundController>().HPS();
            Destroy(HPGO);
            if (plMovement.character == "Rifler")
            {
                GameManager.HPRInt += 35;
                if (GameManager.HPRInt > 100)
                    GameManager.HPRInt = 100;
            }
            if (plMovement.character == "Sniper")
            {
                GameManager.HPSInt += 35;
                if (GameManager.HPSInt > 60)
                    GameManager.HPSInt = 60;
            }
            if (plMovement.character == "Sickler")
            {
                GameManager.HPSiInt += 35;
                if (GameManager.HPSiInt > 140 && !STAR.isInfHPOn)
                    GameManager.HPSiInt = 140;
            }
        }
    }
}
