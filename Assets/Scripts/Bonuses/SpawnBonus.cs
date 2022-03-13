using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    private bool cooldown, starCooldown;
    public GameObject HP, HPCopy, rB, rBCopy, sB, sBCopy, Star, StarCopy;
    private int num;

    void Start()
    {
        StarCopy = null;
        StartCoroutine(BonusSpawn());
        StartCoroutine(StarSpawn());
    }

    void Update()
    {
        if (!cooldown)
        {
            StartCoroutine(BonusSpawn());
        }

        if (!starCooldown)
        {
            StartCoroutine(StarSpawn());
        }
    }

    IEnumerator BonusSpawn()
    {
        cooldown = true;
        num = Random.Range(0, 3);
        yield return new WaitForSeconds(Random.Range(12.5f, 20f));
        cooldown = false;
        if (num == 0)
        {
            HPCopy = Instantiate(HP, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
            yield return new WaitForSeconds(10);
            if (HPCopy != null)
                Destroy(HPCopy);
        }

        if (num == 1)
        {
            rBCopy = Instantiate(rB, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
            yield return new WaitForSeconds(10);
            if (rBCopy != null)
                Destroy(rBCopy);
        }

        if (num == 2)
        {
            sBCopy = Instantiate(sB, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
            yield return new WaitForSeconds(10);
            if (sBCopy != null)
                Destroy(sBCopy);
        }
    }

    IEnumerator StarSpawn()
    {
        starCooldown = true;
        yield return new WaitForSeconds(10);
        if (StarCopy != null && !global::Star.isDDOn && !global::Star.isInfAmmoOn && !global::Star.isInfHPOn)
            Destroy(StarCopy);
        yield return new WaitForSeconds(Random.Range(20f, 35f));
        starCooldown = false;
        StarCopy = Instantiate(Star, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
    }
}
