using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject bigEnemyPrefab;
    [SerializeField] private GameObject medEnemyPrefab;
    [SerializeField] private GameObject smallEnemyPrefab;

    private Vector2 spawnPosRight;
    private Vector2 spawnPosLeft;

    private bool bigSpawnCooldown;
    private bool medSpawnCooldown;
    private bool smallSpawnCooldown;
    private bool levelInProcess;

    void Start()
    {
        StartCoroutine(LevelTimer());
        StartCoroutine(SpawnProgressTimer());
        spawnPosLeft = new Vector2(50, -5f);
        spawnPosRight = new Vector2(-19, -5f);
    }

    void Update()
    {
        if (levelInProcess)
        {
            if (!bigSpawnCooldown)
            {
                StartCoroutine(SpawnBig());
            }
            if (!medSpawnCooldown)
            {
                StartCoroutine(SpawnMed());
            }
            if (!smallSpawnCooldown)
            {
                StartCoroutine(SpawnSmall());
            }
        }
    }

    private IEnumerator SpawnProgressTimer()
    {
        medSpawnCooldown = true;
        bigSpawnCooldown = true;
        yield return new WaitForSeconds(10f);
        medSpawnCooldown = false;
        yield return new WaitForSeconds(10f);
        bigSpawnCooldown = false;
    }

    IEnumerator SpawnBig()
    {
        int side = Random.Range(0, 2);
        bigSpawnCooldown = true;
        if (side == 0)
        {
            Instantiate(bigEnemyPrefab, spawnPosLeft, Quaternion.identity);
        }
        else
        {
            Instantiate(bigEnemyPrefab, spawnPosRight, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(10f, 12f));
        bigSpawnCooldown = false;
    }

    IEnumerator SpawnMed()
    {
        int side = Random.Range(0, 2);
        medSpawnCooldown = true;
        if (side == 0)
        {
            Instantiate(medEnemyPrefab, spawnPosLeft, Quaternion.identity);
        }
        else
        {
            Instantiate(medEnemyPrefab, spawnPosRight, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(1f, 8f));
        medSpawnCooldown = false;
    }

    IEnumerator SpawnSmall()
    {
        int side = Random.Range(0, 2);
        smallSpawnCooldown = true;
        if (side == 0)
        {
            Instantiate(smallEnemyPrefab, spawnPosLeft, Quaternion.identity);
        }
        else
        {
            Instantiate(smallEnemyPrefab, spawnPosRight, Quaternion.identity);
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        smallSpawnCooldown = false;
    }

    IEnumerator LevelTimer()
    {
        levelInProcess = true;
        yield return new WaitForSeconds(87f);
        levelInProcess = false;
    }
}
