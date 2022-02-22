using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject bigEnemy, medEnemy, smallEnemy;
    public bool spawnBig, spawnMed, spawnSmall, canSpawn;
    public Vector2 spawnPos, spawnPosBack;
    private int spawnSide;

    void Start()
    {
        StartCoroutine(SpawnTimer());
        spawnBig = false;
        spawnMed = false;
        spawnSmall = false;
        canSpawn = true;
    }

    void Update()
    {
        if (!spawnBig && canSpawn)
        {
            StartCoroutine(SpawnBigFunction());
        }
        if (!spawnMed && canSpawn)
        {
            StartCoroutine(SpawnMedFunction());
        }
        if (!spawnSmall && canSpawn)
        {
            StartCoroutine(SpawnSmallFunction());
        }
    }

    IEnumerator SpawnBigFunction()
    {
        spawnSide = Random.Range(0, 2);
        spawnBig = true;
        if (spawnSide == 0)
        {
            Instantiate(bigEnemy, spawnPosBack, Quaternion.identity);
        }
        if (spawnSide != 0)
        {
            Instantiate(bigEnemy, spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(10f, 12f));
        spawnBig = false;
    }

    IEnumerator SpawnMedFunction()
    {
        spawnSide = Random.Range(0, 2);
        spawnMed = true;
        if (spawnSide == 0)
        {
            Instantiate(medEnemy, spawnPosBack, Quaternion.identity);
        }
        if (spawnSide != 0)
        {
            Instantiate(medEnemy, spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(1f, 8f));
        spawnMed = false;
    }

    IEnumerator SpawnSmallFunction()
    {
        spawnSide = Random.Range(0, 2);
        spawnSmall = true;
        if (spawnSide == 0)
        {
            Instantiate(smallEnemy, spawnPosBack, Quaternion.identity);
        }
        if (spawnSide != 0)
        {
            Instantiate(smallEnemy, spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        spawnSmall = false;
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(87);
        canSpawn = false;
    }
}
