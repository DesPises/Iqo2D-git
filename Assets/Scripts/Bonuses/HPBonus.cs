using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBonus : MonoBehaviour
{
    public GameObject HPGO, vodkaGO, shawaGO, kebabGO, soundContrGO;

    void Update()
    {
        if (PlayerMovement.character == "Rifler")
        {
            vodkaGO.SetActive(true);
            shawaGO.SetActive(false);
            kebabGO.SetActive(false);
        }
        if (PlayerMovement.character == "Sniper")
        {
            vodkaGO.SetActive(false);
            shawaGO.SetActive(true);
            kebabGO.SetActive(false);
        }
        if (PlayerMovement.character == "Sickler")
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
            if (PlayerMovement.character == "Rifler")
            {
                GameManager.HPRInt += 35;
                if (GameManager.HPRInt > 100)
                    GameManager.HPRInt = 100;
            }
            if (PlayerMovement.character == "Sniper")
            {
                GameManager.HPSInt += 35;
                if (GameManager.HPSInt > 60)
                    GameManager.HPSInt = 60;
            }
            if (PlayerMovement.character == "Sickler")
            {
                GameManager.HPSiInt += 35;
                if (GameManager.HPSiInt > 140 && !Star.isInfHPOn)
                    GameManager.HPSiInt = 140;
            }
        }
    }
}
