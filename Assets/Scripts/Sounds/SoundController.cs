using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [Header("Player")]
    public AudioSource bulletHit;
    public AudioSource akReload;
    public AudioSource akShoot;
    public AudioSource svdReload;
    public AudioSource svdShoot;
    public AudioSource siVzmah;
    public AudioSource siHit;
    public AudioSource Run;
    public AudioSource Jump;
    public AudioSource Death;
    public AudioSource dmg;
    public AudioSource emptyMag;

    [Header("Bonuses")]
    public AudioSource ammo;
    public AudioSource HP;
    public AudioSource bonus;

    [Header("Aliens")]
    public AudioSource alienShoot;
    public AudioSource alienDeath;
    public AudioSource explosion;
    public AudioSource laser;
    public AudioSource alienHit;

    private void Start()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("volume"))
        {
            bulletHit.volume = 0.2f;
            akReload.volume = 0.2f;
            akShoot.volume = 0.2f;
            svdReload.volume = 0.2f;
            svdShoot.volume = 0.2f;
            siVzmah.volume = 0.1f;
            siHit.volume = 0.2f;
            Run.volume = 0.2f;
            Jump.volume = 0.2f;
            Death.volume = 0.2f;
            dmg.volume = 0.2f;
            emptyMag.volume = 0.2f;
            ammo.volume = 0.2f;
            HP.volume = 0.2f;
            bonus.volume = 0.2f;
            alienShoot.volume = 0.2f;
            alienDeath.volume = 0.2f;
            explosion.volume = 0.2f;
            laser.volume = 0.2f;
            alienHit.volume = 0.1f;
        }
    }

    private void Update()
    {
        bulletHit.volume = PlayerPrefs.GetFloat("volume");
        akReload.volume = PlayerPrefs.GetFloat("volume");
        akShoot.volume = PlayerPrefs.GetFloat("volume");
        svdReload.volume = PlayerPrefs.GetFloat("volume");
        svdShoot.volume = PlayerPrefs.GetFloat("volume");
        siVzmah.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        siHit.volume = PlayerPrefs.GetFloat("volume");
        Run.volume = PlayerPrefs.GetFloat("volume");
        Jump.volume = PlayerPrefs.GetFloat("volume");
        Death.volume = PlayerPrefs.GetFloat("volume");
        dmg.volume = PlayerPrefs.GetFloat("volume");
        emptyMag.volume = PlayerPrefs.GetFloat("volume");
        ammo.volume = PlayerPrefs.GetFloat("volume");
        HP.volume = PlayerPrefs.GetFloat("volume");
        bonus.volume = PlayerPrefs.GetFloat("volume");
        alienShoot.volume = PlayerPrefs.GetFloat("volume");
        alienDeath.volume = PlayerPrefs.GetFloat("volume");
        explosion.volume = PlayerPrefs.GetFloat("volume");
        laser.volume = PlayerPrefs.GetFloat("volume");
        alienHit.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
    }

    public void akReloadS()
    {
        akReload.Play();
    }

    public void bulletHitS()
    {
        bulletHit.Play();
    }

    public void akShootS()
    {
        akShoot.Play();
    }

    public void svdReloadS()
    {
        svdReload.Play();
    }

    public void svdShootS()
    {
        svdShoot.Play();
    }

    public void siVzmahS()
    {
        siVzmah.Play();
    }

    public void siHitS()
    {
        siHit.Play();
    }

    public void RunS()
    {
        Run.Play();
    }

    public void JumpS()
    {
        Jump.Play();
    }

    public void DeathS()
    {
        Death.Play();
    }

    public void dmgS()
    {
        dmg.Play();
    }

    public void emptyMagS()
    {
        emptyMag.Play();
    }

    public void ammoS()
    {
        ammo.Play();
    }

    public void HPS()
    {
        HP.Play();
    }

    public void bonusS()
    {
        bonus.Play();
    }

    public void alienShootS()
    {
        alienShoot.Play();
    }

    public void alienDeathS()
    {
        alienDeath.Play();
    }

    public void explosionS()
    {
        explosion.Play();
    }

    public void laserS()
    {
        laser.Play();
    }

    public void alienHitS()
    {
        alienHit.Play();
    }
}
