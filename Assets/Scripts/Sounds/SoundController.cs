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
    public AudioSource run;
    public AudioSource jump;
    public AudioSource death;
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
            run.volume = 0.2f;
            jump.volume = 0.2f;
            death.volume = 0.2f;
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
        run.volume = PlayerPrefs.GetFloat("volume");
        jump.volume = PlayerPrefs.GetFloat("volume");
        death.volume = PlayerPrefs.GetFloat("volume");
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

    public void AkReload()
    {
        akReload.Play();
    }

    public void BulletHit()
    {
        bulletHit.Play();
    }

    public void AkShoot()
    {
        akShoot.Play();
    }

    public void SvdReload()
    {
        svdReload.Play();
    }

    public void SvdShoot()
    {
        svdShoot.Play();
    }

    public void Sickle()
    {
        siVzmah.Play();
    }

    public void SickleHit()
    {
        siHit.Play();
    }

    public void Run()
    {
        run.Play();
    }

    public void JumpS()
    {
        jump.Play();
    }

    public void DeathS()
    {
        death.Play();
    }

    public void GetDamage()
    {
        dmg.Play();
    }

    public void EmptyMag()
    {
        emptyMag.Play();
    }

    public void AmmoSniper()
    {
        ammo.Play();
    }

    public void HPBonus()
    {
        HP.Play();
    }

    public void StarBonus()
    {
        bonus.Play();
    }

    public void AlienShoot()
    {
        alienShoot.Play();
    }

    public void AlienDeath()
    {
        alienDeath.Play();
    }

    public void Explosion()
    {
        explosion.Play();
    }

    public void Laser()
    {
        laser.Play();
    }

    public void AlienHit()
    {
        alienHit.Play();
    }
}
