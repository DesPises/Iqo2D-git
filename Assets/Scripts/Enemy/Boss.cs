using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public Animator cameraAnim;

    public SpriteRenderer bodysr;
    public Image HPbar;
    public GameObject enemyClone;

    private int HPfull;
    private int HP;
    private int attackNumber;

    private bool cooldown, immunity;
    public static bool playerInFarAttackZone, playerInUpAttackZone, playerInCloseAttackZone;
    public Vector3 farpos, closepos, uppos, farsides, closesides, upsides;

    public AudioSource entrygrowl, stepone, steptwo, cattack, uattack, fattack, spawnsound, damaged;

    public LayerMask PlayerLayer;
    RaycastHit hit;

    void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
            entrygrowl.volume = PlayerPrefs.GetFloat("volume");
        else entrygrowl.volume = 0.2f;

        if (PlayerPrefs.HasKey("volume"))
            stepone.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        else stepone.volume = 0.1f;

        if (PlayerPrefs.HasKey("volume"))
            steptwo.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        else steptwo.volume = 0.1f;

        if (PlayerPrefs.HasKey("volume"))
            cattack.volume = PlayerPrefs.GetFloat("volume");
        else cattack.volume = 0.2f;

        if (PlayerPrefs.HasKey("volume"))
            uattack.volume = PlayerPrefs.GetFloat("volume") * 3;
        else uattack.volume = 0.6f;

        if (PlayerPrefs.HasKey("volume"))
            fattack.volume = PlayerPrefs.GetFloat("volume") * 2;
        else fattack.volume = 0.4f;

        if (PlayerPrefs.HasKey("volume"))
            spawnsound.volume = PlayerPrefs.GetFloat("volume") * 3;
        else spawnsound.volume = 0.6f;

        if (PlayerPrefs.HasKey("volume"))
            damaged.volume = PlayerPrefs.GetFloat("volume");
        else damaged.volume = 0.2f;
    }

    void Start()
    {
        HPfull = 250;
        HP = HPfull;
        attackNumber = 0;
        cooldown = false;
    }

    void Update()
    {
        GameManager.instance.bulletsRAtAllInt = 999;
        GameManager.instance.bulletsSAtAllInt = 999;

        HPbar.fillAmount = HP * 0.004f;

        if (HP > HPfull / 2 && !cooldown)
        {
            StartCoroutine(AttackPartOne());
        }

        if (HP <= HPfull / 2 && HP > 0 && !cooldown)
        {
            StartCoroutine(AttackPartTwo());
        }

        if (HP <= 0)
            StartCoroutine(Death());
    }

    //Attack Coroutines

    IEnumerator AttackPartOne()
    {
        cooldown = true;

        switch (attackNumber)
        {
            case 0:
                StartCoroutine(CloseAttack());
                break;
            case 1:
                StartCoroutine(FarAttack());
                break;
            case 2:
                StartCoroutine(UpAttack());
                break;
            case 3:
                StartCoroutine(Spawn());
                break;
        }
        if (attackNumber < 3)
        {
            attackNumber++;
        }
        else attackNumber = 0;

        yield return new WaitForSeconds(3.4f);
        cooldown = false;
    }

    IEnumerator AttackPartTwo()
    {
        cooldown = true;

        switch (attackNumber)
        {
            case 0:
                StartCoroutine(CloseAttack());
                break;
            case 1:
                StartCoroutine(FarAttack());
                break;
            case 2:
                StartCoroutine(UpAttack());
                break;
            case 3:
                StartCoroutine(Spawn());
                break;
        }
        if (attackNumber < 3)
        {
            attackNumber++;
        }
        else attackNumber = 0;

        yield return new WaitForSeconds(2.3f);
        cooldown = false;
    }

    IEnumerator CloseAttack()
    {
        anim.SetBool("close", true);
        yield return null;
        anim.SetBool("close", false);
    }

    IEnumerator FarAttack()
    {
        anim.SetBool("far", true);
        yield return null;
        anim.SetBool("far", false);
    }

    IEnumerator UpAttack()
    {
        anim.SetBool("up", true);
        yield return null;
        anim.SetBool("up", false);
    }

    IEnumerator Spawn()
    {
        anim.SetBool("spawn", true);
        yield return null;
        anim.SetBool("spawn", false);
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Instantiate(enemyClone, new Vector2(22, -4.8f), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    public void FarDamageCheck()
    {
        if (playerInFarAttackZone)
        {
            DealDamage();
        }
    }

    public void CloseDamageCheck()
    {
        if (playerInCloseAttackZone)
        {
            DealDamage();
        }
    }

    public void UpDamageCheck()
    {
        if (playerInUpAttackZone)
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        if (plMovement.character == "Rifler")
            GameManager.HPRInt -= 40;
        if (plMovement.character == "Sniper")
            GameManager.HPSInt -= 40;
        if (plMovement.character == "Sickler")
            GameManager.HPSiInt -= 40;
    }

    //Gizmos
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(farpos, farsides);
    //}
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(closepos, closesides);
    //}
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(uppos, upsides);
    //}

    //Camera Shake

    public IEnumerator CameraShake()
    {
        cameraAnim.SetBool("shake", true);
        yield return new WaitForSeconds(0.1f);
        cameraAnim.SetBool("shake", false);
    }


    //Sounds

    public void EntrySound()
    {
        entrygrowl.Play();
    }

    public void StepOneSound()
    {
        stepone.Play();
    }

    public void StepTwoSound()
    {
        steptwo.Play();
    }

    public void UpAttackSound()
    {
        uattack.Play();
    }

    public void FarAttackSound()
    {
        fattack.Play();
    }

    public void CloseAttackSound()
    {
        cattack.Play();
    }

    public void SpawnSound()
    {
        spawnsound.Play();
    }

    public void DamagedSound()
    {
        damaged.Play();
    }

    //Immunity

    void ImmunityOn()
    {
        immunity = true;
    }

    void ImmunityOff()
    {
        immunity = false;
    }


    //Get damage

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet" && !immunity)
        {
            HP -= 1;
            StartCoroutine(DamageColorChange());
        }
        if (col.gameObject.tag == "BulletS" && !immunity)
        {
            HP -= 5;
            StartCoroutine(DamageColorChange());
        }
    }

    public void DamageFromSickler()
    {
        if (!immunity)
        {
            HP -= 2;
            StartCoroutine(DamageColorChange());
        }
    }

    IEnumerator DamageColorChange()
    {
        DamagedSound();

        for (float f = 1; f >= 0.5f; f -= 0.05f)
        {
            Color color = bodysr.material.color;
            color.g = f;
            color.b = f;
            bodysr.material.color = color;
            yield return new WaitForSeconds(0.02f);
        }

        for (float f = 0.5f; f <= 1; f += 0.05f)
        {
            Color color = bodysr.material.color;
            color.g = f;
            color.b = f;
            bodysr.material.color = color;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Death()
    {
        entrygrowl.volume = PlayerPrefs.GetFloat("volume") * 2;
        GameManager.HPRInt = 100;
        GameManager.HPSInt = 100;
        GameManager.HPSiInt = 200;
        anim.SetBool("dead", true);
        immunity = true;
        yield return new WaitForSeconds(1.5f);
        bodysr.color = new Color(255, 140, 140, 155);
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(7);
    }
}
