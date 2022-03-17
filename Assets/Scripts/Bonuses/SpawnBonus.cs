using System.Collections;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] private GameObject HPBonusPrefab;
    [SerializeField] private GameObject riflerAmmoBonusPrefab;
    [SerializeField] private GameObject sniperAmmoBonusPrefab;
    [SerializeField] private GameObject starBonusPrefab;

    private bool cooldown;
    private bool starCooldown;

    void Update()
    {
        // Call spawn bonus method if not cooldown

        if (!cooldown)
        {
            StartCoroutine(BonusSpawn());
        }

        if (!starCooldown)
        {
            StartCoroutine(StarSpawn());
        }
    }

    private IEnumerator BonusSpawn()
    {
        // Set random delay (from 12.5 to 20 sec) between spawning bonuses
        cooldown = true;
        yield return new WaitForSeconds(Random.Range(12.5f, 20f));
        cooldown = false;

        // Generate random index and position
        int index = Random.Range(0, 3);
        Vector2 pos = new(Random.Range(4.5f, 22f), Random.Range(-1f, 3f));

        // Create new empty gameobject and destroy it in 10 seconds
        GameObject bonusCopy = null;
        Destroy(bonusCopy, 10);

        switch (index)
        {
            case 0:
                bonusCopy = Instantiate(HPBonusPrefab, pos, Quaternion.identity);
                break;
            case 1:
                bonusCopy = Instantiate(riflerAmmoBonusPrefab, pos, Quaternion.identity);
                break;
            case 2:
                bonusCopy = Instantiate(sniperAmmoBonusPrefab, pos, Quaternion.identity);
                break;
        }
    }

    private IEnumerator StarSpawn()
    {
        // Set random delay (from 25 to 35 sec) between spawning bonuses
        starCooldown = true;
        yield return new WaitForSeconds(Random.Range(25f, 35f));
        starCooldown = false;
        
        // Create new star bonus and destroy it in 9 sec if player doesn't pick it up
        GameObject starCopy = Instantiate(starBonusPrefab, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
        yield return new WaitForSeconds(9);
        if (!Rifler.Instance.isBonusActive && !Sniper.Instance.isBonusActive && !Sickler.Instance.isBonusActive)
        {
            Destroy(starCopy);
        }
    }
}
