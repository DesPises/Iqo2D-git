using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject infiniteAmmoIcon;
    [SerializeField] private GameObject immortalityIcon;
    [SerializeField] private GameObject doubleDamageIcon;
    [SerializeField] private GameObject timer;
    [SerializeField] private Text timeText;

    private BoxCollider2D boxCollider;
    private SpriteRenderer sr;

    void Start()
    {
        // Get components
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If bonus is active and character dies

        if (Rifler.Instance.isBonusActive && Player.riflerIsDead)
        {
            doubleDamageIcon.SetActive(false);
            OnCharacterDeath();
        }

        if (Sniper.Instance.isBonusActive && Player.sniperIsDead)
        {
            infiniteAmmoIcon.SetActive(false);
            OnCharacterDeath();
        }

        if (Sickler.Instance.isBonusActive && Player.sicklerIsDead)
        {
            immortalityIcon.SetActive(false);
            OnCharacterDeath();
        }

        // If player is rifler, bonus is active, and player ran out of ammo, give 30 bonus rifle ammo 
        if (Rifler.Instance.isBonusActive && Rifler.Instance.ammoInStock == 0)
        {
            StartCoroutine(GameManager.Instance.AmmoBonus(30, 1));
        }
    }

    // Pick up bonus
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            CharacterChangeCode.canChange = false;
            DisableComponentsAndPlaySound();
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
    }

    private void OnCharacterDeath()
    {
        timer.SetActive(false);
        CharacterChangeCode.canChange = true;
        Destroy(gameObject);
    }

    private void DisableComponentsAndPlaySound()
    {
        boxCollider.enabled = false;
        sr.enabled = false;
        SoundController.Instance.bonusS();
        CharacterChangeCode.canChange = false;
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
                Destroy(gameObject);
            }
        }
    }
}
