using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public static Boss Instance { get; private set; }

    private Animator anim;
    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Image HPbar;
    [SerializeField] private SpriteRenderer bodysr;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private ParticleSystem particles;

    private int HPmax;
    private int HP;
    private int attackIndex;

    private bool cooldown;
    private bool immunity;

    [Header("Audio")]
    [SerializeField] private AudioSource growl;
    [SerializeField] private AudioSource stepOne;
    [SerializeField] private AudioSource stepTwo;
    [SerializeField] private AudioSource closeAttack;
    [SerializeField] private AudioSource upperAttack;
    [SerializeField] private AudioSource farAttack;
    [SerializeField] private AudioSource spawnClones;
    [SerializeField] private AudioSource getDamage;

    // Variables for checking if player is in attack zone

    private bool playerInUpperAttackZone;
    private bool playerInCloseAttackZone;
    private bool playerInFarAttackZone;

    private readonly Vector3 posUpperAttackGizmos = new(3f, 4f);
    private readonly Vector3 sizeUpperAttackGizmos = new(15f, 5f);
    private readonly Vector3 posCloseAttackGizmos = new(6.5f, -1.7f);
    private readonly Vector3 sizeCloseAttackGizmos = new(6.2f, 6.2f);
    private readonly Vector3 posFarAttackGizmos = new(-1f, -1.7f);
    private readonly Vector3 sizeFarAttackGizmos = new(6.2f, 6.2f);

    void Start()
    {
        Instance = this;
        Rifler.Instance.ammoInStock = 200;
        Sniper.Instance.ammoInStock = 50;

        anim = GetComponent<Animator>();

        HPmax = 250;
        HP = HPmax;

        if (PlayerPrefs.HasKey("volume"))
        {
            growl.volume = PlayerPrefs.GetFloat("volume");
            stepOne.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
            stepTwo.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
            closeAttack.volume = PlayerPrefs.GetFloat("volume");
            upperAttack.volume = PlayerPrefs.GetFloat("volume") * 3;
            farAttack.volume = PlayerPrefs.GetFloat("volume") * 2;
            spawnClones.volume = PlayerPrefs.GetFloat("volume") * 3;
            getDamage.volume = PlayerPrefs.GetFloat("volume");
        }
    }

    void Update()
    {
        // Cast box to check if player is in attack zone
        playerInUpperAttackZone = Physics2D.BoxCast(posUpperAttackGizmos, sizeUpperAttackGizmos, 0f, Vector2.one, 0f, LayerMask.GetMask("Player"));
        playerInCloseAttackZone = Physics2D.BoxCast(posCloseAttackGizmos, sizeCloseAttackGizmos, 0f, Vector2.one, 0f, LayerMask.GetMask("Player"));
        playerInFarAttackZone = Physics2D.BoxCast(posFarAttackGizmos, sizeFarAttackGizmos, 0f, Vector2.one, 0f, LayerMask.GetMask("Player"));

        // HP control and attack phase depending of HP

        HPbar.fillAmount = HP * 0.004f;

        if (HP > HPmax / 2 && !cooldown)
        {
            StartCoroutine(AttackPhaseOne());
        }

        if (HP <= HPmax / 2 && HP > 0 && !cooldown)
        {
            StartCoroutine(AttackPhaseTwo());
        }

        if (HP <= 0)
        {
            StartCoroutine(Death());
        }
    }

    //Attack Coroutines

    private IEnumerator AttackPhaseOne()
    {
        cooldown = true;

        switch (attackIndex)
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
        if (attackIndex < 3)
        {
            attackIndex++;
        }
        else attackIndex = 0;

        yield return new WaitForSeconds(3.4f);
        cooldown = false;
    }

    private IEnumerator AttackPhaseTwo()
    {
        cooldown = true;

        switch (attackIndex)
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
        if (attackIndex < 3)
        {
            attackIndex++;
        }
        else attackIndex = 0;

        yield return new WaitForSeconds(2.3f);
        cooldown = false;
    }

    private IEnumerator CloseAttack()
    {
        anim.SetBool("close", true);
        yield return null;
        anim.SetBool("close", false);
    }

    private IEnumerator FarAttack()
    {
        anim.SetBool("far", true);
        yield return null;
        anim.SetBool("far", false);
    }

    private IEnumerator UpAttack()
    {
        anim.SetBool("up", true);
        yield return null;
        anim.SetBool("up", false);
    }

    private IEnumerator Spawn()
    {
        anim.SetBool("spawn", true);
        yield return null;
        anim.SetBool("spawn", false);
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            Instantiate(enemyPrefab, new Vector2(22, -4.8f), Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }

    public void FarAttackDamageCheck()
    {
        if (playerInFarAttackZone)
        {
            DealDamage();
        }
    }

    public void CloseAttackDamageCheck()
    {
        if (playerInCloseAttackZone)
        {
            DealDamage();
        }
    }

    public void UpperAttackDamageCheck()
    {
        if (playerInUpperAttackZone)
        {
            DealDamage();
        }
    }

    private void DealDamage()
    {
        if (Player.character == "Rifler")
        {
            Rifler.Instance.GetDamage(40);
        }
        else if (Player.character == "Sniper")
        {
            Sniper.Instance.GetDamage(40);
        }
        else if (Player.character == "Sickler")
        {
            Sickler.Instance.GetDamage(40);
        }
    }

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
        growl.Play();
    }

    public void StepOneSound()
    {
        stepOne.Play();
    }

    public void StepTwoSound()
    {
        stepTwo.Play();
    }

    public void UpAttackSound()
    {
        upperAttack.Play();
    }

    public void FarAttackSound()
    {
        farAttack.Play();
    }

    public void CloseAttackSound()
    {
        closeAttack.Play();
    }

    public void SpawnSound()
    {
        spawnClones.Play();
    }

    public void DamagedSound()
    {
        getDamage.Play();
    }

    //Immunity

    public void ImmunityOn()
    {
        immunity = true;
    }

    public void ImmunityOff()
    {
        immunity = false;
    }


    //Get damage

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GetDamage(1));
            Destroy(col.gameObject);
        }
        else if (col.gameObject.CompareTag("SniperBullet"))
        {
            StartCoroutine(GetDamage(5));
            Destroy(col.gameObject);
        }
    }

    public IEnumerator GetDamage(int damage)
    {
        if (!immunity)
        {
            HP -= damage;

            DamagedSound();

            // "Animation" of getting damage

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
    }

    private IEnumerator Death()
    {
        growl.volume = PlayerPrefs.GetFloat("volume") * 2;
        anim.SetBool("dead", true);
        immunity = true;
        yield return new WaitForSeconds(1.5f);
        bodysr.color = new Color(255, 140, 140, 155);
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(7);
    }

    // Draw boxes of attack zones
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(posUpperAttackGizmos, sizeUpperAttackGizmos);
        Gizmos.DrawWireCube(posCloseAttackGizmos, sizeCloseAttackGizmos);
        Gizmos.DrawWireCube(posFarAttackGizmos, sizeFarAttackGizmos);
    }
}
