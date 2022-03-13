using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBonus : MonoBehaviour
{
    public GameObject HPGO, vodkaGO, shawaGO, kebabGO, soundContrGO;

    void Update()
    {
        if (Player.character == "Rifler")
        {
            vodkaGO.SetActive(true);
            shawaGO.SetActive(false);
            kebabGO.SetActive(false);
        }
        if (Player.character == "Sniper")
        {
            vodkaGO.SetActive(false);
            shawaGO.SetActive(true);
            kebabGO.SetActive(false);
        }
        if (Player.character == "Sickler")
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
            
        }
    }
}
