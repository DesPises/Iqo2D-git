using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public GameObject InfAmmoGO;
    public GameObject InfHPGO;
    public GameObject DDGO;
    public GameObject timerGO;
    public GameObject STARGO;
    public GameObject soundContrGO;
    public Text timeText;
    public BoxCollider2D boxcollider;
    public SpriteRenderer sr;
    public static int sHadAmmo;
    public static bool isDDOn, isInfAmmoOn, isInfHPOn;

    void Start()
    {
        isInfHPOn = false;
        isDDOn = false;
        isInfAmmoOn = false;
    }

    void Update()
    {
        if (isInfAmmoOn && GameManager.Instance.sIsDead)
        {
            timerGO.SetActive(false);
            InfAmmoGO.SetActive(false);
            CharacterChangeCode.canChange = true;
            GameManager.Instance.bulletsSAtAllInt = sHadAmmo + (GameManager.Instance.bulletsSAtAllInt - 999);
            Destroy(STARGO);
        }
        if (isDDOn && GameManager.Instance.rIsDead)
        {
            timerGO.SetActive(false);
            DDGO.SetActive(false);
            CharacterChangeCode.canChange = true;
            GameManager.Instance.damageRInt = 2;
            GameManager.Instance.damageRIntHS = 3;
            Destroy(STARGO);
        }
        if (isInfHPOn && GameManager.Instance.siIsDead)
        {
            timerGO.SetActive(false);
            InfHPGO.SetActive(false);
            CharacterChangeCode.canChange = true;
            GameManager.HPSiInt = 140;
            Destroy(STARGO);
        }

        if (isDDOn && GameManager.Instance.bulletsRAtAllInt == 0 && GameManager.Instance.inMagRInt == 0)
        {
            GameManager.Instance.bulletsRAtAllInt += 30;
        }

        if (isInfAmmoOn)
        {
            GameManager.Instance.bulletsSAtAllInt = 999;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (PlayerMovement.character == "Rifler" && !isDDOn)
            {
                GameManager.HPRInt = 100;
                soundContrGO.GetComponent<SoundController>().bonusS();
                boxcollider.enabled = false;
                sr.color = new Color(0, 0, 0, 0);
                DoubleDamage();
            }
            if (PlayerMovement.character == "Sniper" && !isInfAmmoOn)
            {
                GameManager.HPSInt = 60;
                GameManager.Instance.canAttackS = true;
                soundContrGO.GetComponent<SoundController>().bonusS();
                boxcollider.enabled = false;
                sr.color = new Color(0, 0, 0, 0);
                InfiniteAmmo();
            }
            if (PlayerMovement.character == "Sickler" && !isInfHPOn)
            {
                GameManager.HPSiInt = 140;
                soundContrGO.GetComponent<SoundController>().bonusS();
                boxcollider.enabled = false;
                sr.color = new Color(0, 0, 0, 0);
                Immortality();
            }
        }
    }

    void DoubleDamage()
    {
        StartCoroutine(Timer());
        StartCoroutine(DDOff());
        DDGO.SetActive(true);
        CharacterChangeCode.canChange = false;
        GameManager.Instance.damageRInt = 5;
        GameManager.Instance.damageRIntHS = 7;
    }

    void InfiniteAmmo()
    {
        sHadAmmo = GameManager.Instance.bulletsSAtAllInt;
        StartCoroutine(Timer());
        StartCoroutine(InfAmmoOff());
        InfAmmoGO.SetActive(true);
        CharacterChangeCode.canChange = false;
        GameManager.Instance.bulletsSAtAllInt = 999;
    }

    void Immortality()
    {
        StartCoroutine(Timer());
        StartCoroutine(ImmortalityOff());
        InfHPGO.SetActive(true);
        CharacterChangeCode.canChange = false;
        GameManager.HPSiInt = 9999999;
    }

    IEnumerator Timer()
    {
        timerGO.SetActive(true);
        for (int i = 15; i >= 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1);
            if (i == 0)
            {
                timerGO.SetActive(false);
                InfAmmoGO.SetActive(false);
                InfHPGO.SetActive(false);
                DDGO.SetActive(false);
                CharacterChangeCode.canChange = true;
                Destroy(STARGO);
            }
        }
    }

    IEnumerator ImmortalityOff()
    {
        isInfHPOn = true;
        yield return new WaitForSeconds(15);
        GameManager.HPSiInt = 140;
        isInfHPOn = false;
    }

    IEnumerator InfAmmoOff()
    {
        isInfAmmoOn = true;
        yield return new WaitForSeconds(15);
        GameManager.Instance.bulletsSAtAllInt = sHadAmmo + (GameManager.Instance.bulletsSAtAllInt - 999);
        isInfAmmoOn = false;
    }

    IEnumerator DDOff()
    {
        isDDOn = true;
        yield return new WaitForSeconds(15);
        GameManager.Instance.damageRInt = 2;
        GameManager.Instance.damageRIntHS = 3;
        isDDOn = false;
    }
}
