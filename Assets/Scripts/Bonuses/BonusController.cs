using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    public static BonusController Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject HPBonusPrefab;
    [SerializeField] private GameObject riflerAmmoBonusPrefab;
    [SerializeField] private GameObject sniperAmmoBonusPrefab;
    [SerializeField] private GameObject starBonusPrefab;
    [Header("UI")]
    [SerializeField] private GameObject infiniteAmmoIcon;
    [SerializeField] private GameObject immortalityIcon;
    [SerializeField] private GameObject doubleDamageIcon;
    [SerializeField] private GameObject timer;
    [SerializeField] private Text timeText;

    private bool cooldown;
    private bool starCooldown;

    private void Start()
    {
        Instance = this;
        StartCoroutine(StarSpawn()); // DELETE AFTER TEST
    }

    void Update()
    {
        // Call spawn bonus method if not cooldown

        if (!cooldown)
        {
            StartCoroutine(BonusSpawn());
        }

        if (!starCooldown)
        {
            //StartCoroutine(StarSpawn());
        }

        // If bonus is active and character dies
        if (Player.character == "Rifler")
        {
            if (Rifler.Instance.isBonusActive && Player.riflerIsDead)
            {
                doubleDamageIcon.SetActive(false);
                OnCharacterDeath();
            }
            // If player is rifler, bonus is active and player run out of ammo, give 30 bonus rifle ammo 
            if (Rifler.Instance.isBonusActive && Rifler.Instance.ammoInStock <= 0 && Rifler.Instance.ammoInMag <= 0)
            {
                StartCoroutine(GameManager.Instance.AmmoBonus(30, 1));
            }
        }
        else if (Player.character == "Sniper")
        {
            if (Sniper.Instance.isBonusActive && Player.sniperIsDead)
            {
                infiniteAmmoIcon.SetActive(false);
                OnCharacterDeath();
            }
        }
        else if (Player.character == "Sickler")
        {
            if (Sickler.Instance.isBonusActive && Player.sicklerIsDead)
            {
                immortalityIcon.SetActive(false);
                OnCharacterDeath();
            }
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
        //yield return new WaitForSeconds(Random.Range(25f, 35f));
        yield return new WaitForSeconds(Random.Range(1f, 2f)); // DELETE AFTER TEST
        starCooldown = false;
        
        // Create new star bonus and destroy it in 9 sec if player doesn't pick it up
        GameObject starCopy = Instantiate(starBonusPrefab, new Vector2(Random.Range(4.5f, 22f), Random.Range(-1f, 3f)), Quaternion.identity);
        yield return new WaitForSeconds(9);
        if (Player.character == "Rifler")
        {
            if (!Rifler.Instance.isBonusActive)
            {
                Destroy(starCopy);
            }
        }
        else if (Player.character == "Sniper")
        {
            if (!Sniper.Instance.isBonusActive)
            {
                Destroy(starCopy);
            }
        }
        else if (Player.character == "Sickler")
        {
            if (!Sickler.Instance.isBonusActive)
            {
                Destroy(starCopy);
            }
        }
    }

    public void StarPickUp()
    {
        SoundController.Instance.StarBonus();
        CharacterChangeCode.canChange = false;
        StartCoroutine(Timer());

        if (Player.character == "Rifler" && !Rifler.Instance.isBonusActive)
        {
            Rifler.Instance.RefillHPToFull(100);
            doubleDamageIcon.SetActive(true);
            StartCoroutine(Rifler.Instance.DoubleDamage());
        }
        if (Player.character == "Sniper" && !Sniper.Instance.isBonusActive)
        {
            Sniper.Instance.RefillHPToFull(60);
            infiniteAmmoIcon.SetActive(true);
            StartCoroutine(Sniper.Instance.InfiniteAmmo(15));
        }
        if (Player.character == "Sickler" && !Sickler.Instance.isBonusActive)
        {
            Sickler.Instance.RefillHPToFull(140);
            immortalityIcon.SetActive(true);
            StartCoroutine(Sickler.Instance.Immortality());
        }
    }

    private IEnumerator Timer()
    {
        timer.SetActive(true);
        for (int i = 15; i >= 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1);
            if (i == 0)
            {
                timer.SetActive(false);
                infiniteAmmoIcon.SetActive(false);
                immortalityIcon.SetActive(false);
                doubleDamageIcon.SetActive(false);
                CharacterChangeCode.canChange = true;
            }
        }
    }

    private void OnCharacterDeath()
    {
        timer.SetActive(false);
        CharacterChangeCode.canChange = true;
    }
}
