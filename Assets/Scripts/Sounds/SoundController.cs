using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private AudioSource bulletHit;
    [SerializeField] private AudioSource akReload;
    [SerializeField] private AudioSource akShoot;
    [SerializeField] private AudioSource svdReload;
    [SerializeField] private AudioSource svdShoot;
    [SerializeField] private AudioSource siVzmah;
    [SerializeField] private AudioSource siHit;
    [SerializeField] private AudioSource run;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource death;
    [SerializeField] private AudioSource dmg;
    [SerializeField] private AudioSource emptyMag;

    [Header("Bonuses")]
    [SerializeField] private AudioSource ammo;
    [SerializeField] private AudioSource HP;
    [SerializeField] private AudioSource bonus;

    [Header("Aliens")]
    [SerializeField] private AudioSource alienShoot;
    [SerializeField] private AudioSource alienDeath;
    [SerializeField] private AudioSource explosion;
    [SerializeField] private AudioSource laser;
    [SerializeField] private AudioSource alienHit;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("volume"))
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
        else
        {
            PlayerPrefs.SetFloat("volume", 0.2f);
        }
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
