using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //Objects

    [SerializeField] private GameObject bulletSGO;
    [SerializeField] private GameObject bulletSBackGO;
    [SerializeField] private GameObject magSGO;
    [SerializeField] private GameObject playerSGO;
    [SerializeField] private GameObject playerSiGO;
    [SerializeField] private GameObject cloneGO;
    [SerializeField] private GameObject bcloneGO;

    [SerializeField] private GameObject[] riflerElements;
    [SerializeField] private GameObject[] sniperElements;
    [SerializeField] private GameObject[] sicklerElements;

    [SerializeField] private GameObject DeathMenu;

    [SerializeField] private GameObject soundContrGO;
    [SerializeField] private GameObject ostGO;


    [SerializeField] private SpriteRenderer sFireBigSR;
    [SerializeField] private SpriteRenderer sFireMedSR;
    [SerializeField] private SpriteRenderer sFireSmallSR;
    [SerializeField] private SpriteRenderer sFireBigSRCrouch;
    [SerializeField] private SpriteRenderer sFireMedSRCrouch;
    [SerializeField] private SpriteRenderer sFireSmallSRCrouch;

    private readonly Color invisible = new(255, 255, 255, 0);
    private readonly Color visible = new(255, 255, 255, 190);

    [SerializeField] private Rigidbody2D rb;

    // HP
    [SerializeField] private Image HPbarImage;
    // Bullets HUD
    [SerializeField] private Text brtext;
    [SerializeField] private Text bstext;
    [SerializeField] private Text bralltext;
    [SerializeField] private Text bsalltext;

    // Other variables

    public static int sniperDamageInt;
    public static int sicklerDamageInt;
    public static int sEnemyHPInt;
    public static int mEnemyHPInt;
    public static int bEnemyHPInt;
    public static int bossHPInt;

    public bool sniperCanReload;
    public bool canAttackS;
    public bool canAttackSi;
    public bool canAttackSiAnim;

    public bool sReloadCooldown;
    public bool canSAttackAfterReload;

    public bool sDavayReload { get; private set; }

    public bool canWalkSi { get; private set; }
    public bool doesSiAttack { get; private set; }



    public bool emptySoundCooldown;
    public int inMagSInt;

    public int bulletsSAtAllInt;
    public int lastSBulletsInt;

    

    private readonly int damageSInt = 18;
    private readonly int damageSiInt = 12;
    private readonly int damageSIntHS = 25;

    void Start()
    {
        Instance = this;

        inMagSInt = 5;
        bulletsSAtAllInt = 17;

        canAttackS = true;
        canAttackSi = true;
        canAttackSiAnim = true;
        canSAttackAfterReload = true;
        canWalkSi = true;
        emptySoundCooldown = false;

        Time.timeScale = 1;
        DeathMenu.SetActive(false);
    }

    void Update()
    {
        // Damage sound
        if (Enemy.doesHitPlayer)
        {
            DmgSound();
            alienHitSound();
        }

        if (Player.riflerIsDead && Player.sniperIsDead && Player.sicklerIsDead)
        {
            DeathMenu.SetActive(true);
            StartCoroutine(TimeStop());
        }

        // Rifler
        if (Player.character == "Rifler")
        {
            brtext.text = Rifler.Instance.ammoInMag.ToString();
            bralltext.text = "/" + Rifler.Instance.ammoInStock.ToString();



            if (Bullet.doesBulletHit)
            {
                Bullet.doesBulletHit = false;
                Destroy(bcloneGO);
            }

            // Empty
            if (Input.GetKey(InputManager.IM.attackKey) && Rifler.Instance.ammoInStock == 0 && Rifler.Instance.ammoInMag == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(EmptyMagSound());
            }
        }

        // Sniper
        if (Player.character == "Sniper")
        {
            bstext.text = inMagSInt.ToString();
            bsalltext.text = "/" + bulletsSAtAllInt.ToString();

            // Reload
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && sniperCanReload && !sReloadCooldown && bulletsSAtAllInt > 0 && !PauseMenu.isPaused)
            {
                StartCoroutine(MagFadeSniper());
                StartCoroutine(ReloadCooldownSniper());
            }
            else if (inMagSInt == 0 && sniperCanReload)
            {
                StartCoroutine(MagFadeSniper());
                StartCoroutine(ReloadCooldownSniper());
                sReloadCooldown = false;
                StartCoroutine(ReloadSAnim());
            }
            
            if (inMagSInt < 5 && bulletsSAtAllInt > 0)
                sniperCanReload = true;

            if (bulletsSAtAllInt <= 0)
                sniperCanReload = false;

            if (inMagSInt <= 0 && bulletsSAtAllInt <= 0)
                canAttackS = false;

            // Attack

            if (Input.GetKeyDown(InputManager.IM.attackKey) && canAttackS && canSAttackAfterReload && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSInt;
                Enemy.DamageIntHS = damageSIntHS;
                svdShootSound();
                StartCoroutine(BulletSCounter());
                if (!Input.GetKey(InputManager.IM.crouchKey) || (Input.GetKey(InputManager.IM.crouchKey) && !Player.onGround))
                {
                    StartCoroutine(SniperFire());
                    if (Player.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.67f, 0), Quaternion.identity);
                    if (!Player.isMovingFW)
                        bcloneGO = Instantiate(bulletSBackGO, playerSGO.transform.position + new Vector3(-0.5f, 4.67f, 0), Quaternion.identity);
                }
                if (Input.GetKey(InputManager.IM.crouchKey) && Player.onGround)
                {
                    StartCoroutine(SniperFireCrouch());
                    if (Player.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.0f, 0), Quaternion.identity);
                    if (!Player.isMovingFW)
                        bcloneGO = Instantiate(bulletSBackGO, playerSGO.transform.position + new Vector3(-0.5f, 4.0f, 0), Quaternion.identity);
                }
            }
            if (Bullet.doesBulletHit)
            {
                Bullet.doesBulletHit = false;
                Destroy(bcloneGO);
            }
            // Empty

            if (Input.GetKey(InputManager.IM.attackKey) && bulletsSAtAllInt == 0 && inMagSInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(EmptyMagSound());
            }
        }

        // Sickler
        if (Player.character == "Sickler")
        {
            // Attack
            if (Input.GetKeyDown(InputManager.IM.attackKey) && canAttackSi && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSiInt;
                Enemy.DamageIntHS = damageSiInt;
                StartCoroutine(SiAttack());
                StartCoroutine(CanWalkCooldown());
            }
        }
    }

    public void HPBarFill(int HP, float multiplier)
    {
        HPbarImage.fillAmount = HP * multiplier;
    }

    // Sound methods
    IEnumerator EmptyMagSound()
    {
        emptySoundCooldown = true;
        soundContrGO.GetComponent<SoundController>().emptyMagS();
        yield return new WaitForSeconds(0.4f);
        emptySoundCooldown = false;
    }

    public void DmgSound()
    {
        soundContrGO.GetComponent<SoundController>().dmgS();
    }

    public void alienHitSound()
    {
        soundContrGO.GetComponent<SoundController>().alienHitS();
    }
    public void svdShootSound()
    {
        soundContrGO.GetComponent<SoundController>().svdShootS();
    }
    

    // Sniper coroutines
    IEnumerator BulletSCounter()
    {
        yield return new WaitForSeconds(0.01f);
        inMagSInt--;
        canAttackS = false;
        yield return new WaitForSeconds(1f);
        Destroy(bcloneGO);
        canAttackS = true;
    }
    IEnumerator SniperFire()
    {
        sFireBigSR.color = visible;
        sFireMedSR.color = invisible;
        sFireSmallSR.color = invisible;
        yield return new WaitForSeconds(0.03f);
        sFireBigSR.color = invisible;
        sFireMedSR.color = visible;
        sFireSmallSR.color = invisible;
        yield return new WaitForSeconds(0.03f);
        sFireBigSR.color = invisible;
        sFireMedSR.color = invisible;
        sFireSmallSR.color = visible;
        yield return new WaitForSeconds(0.03f);
        sFireSmallSR.color = invisible;
    }
    IEnumerator SniperFireCrouch()
    {
        sFireBigSRCrouch.color = visible;
        sFireMedSRCrouch.color = invisible;
        sFireSmallSRCrouch.color = invisible;
        yield return new WaitForSeconds(0.03f);
        sFireBigSRCrouch.color = invisible;
        sFireMedSRCrouch.color = visible;
        sFireSmallSRCrouch.color = invisible;
        yield return new WaitForSeconds(0.03f);
        sFireBigSRCrouch.color = invisible;
        sFireMedSRCrouch.color = invisible;
        sFireSmallSRCrouch.color = visible;
        yield return new WaitForSeconds(0.03f);
        sFireSmallSRCrouch.color = invisible;
    }
    IEnumerator ReloadSAnim()
    {
        sDavayReload = true;
        yield return null;
        sDavayReload = false;
    }
    IEnumerator ReloadCooldownSniper()
    {
        canSAttackAfterReload = false;
        int leftInMag = inMagSInt;

        if (inMagSInt + bulletsSAtAllInt > 5)
        {
            inMagSInt = 5;
            bulletsSAtAllInt -= (5 - leftInMag);
        }
        else
        {
            inMagSInt += bulletsSAtAllInt;
            bulletsSAtAllInt = 0;
        }

        yield return new WaitForSeconds(0.6f);
        canSAttackAfterReload = true;
        yield return new WaitForSeconds(3f);
        sReloadCooldown = false;
    }
    IEnumerator MagFadeSniper()
    {
        yield return null;

        sniperCanReload = false;
        yield return new WaitForSeconds(0.15f);

        if (Player.isMovingFW && !Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(0.4f, 3), Quaternion.identity);
        }
        else if (!Player.isMovingFW && !Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-0.4f, 3), Quaternion.identity);
        }
        else if (Player.isMovingFW && Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(1, 2.5f), Quaternion.identity);
        }
        else if (!Player.isMovingFW && Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-1, 2.5f), Quaternion.identity);
        }

        Destroy(cloneGO, 0.85f);
    }




    // Sickler coroutines
    IEnumerator SiAttack()
    {
        Player.secJump = false;
        if (Player.isMovingFW)
        {
            rb.velocity = new Vector2(2, 0);
        }
        if (!Player.isMovingFW)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        rb.AddForce(Vector2.down * 40 + Vector2.right * 50, ForceMode2D.Impulse);

        canAttackSi = false;
        doesSiAttack = true;
        yield return null;

        canAttackSiAnim = false;
        yield return new WaitForSeconds(0.4f);

        doesSiAttack = false;
        canAttackSi = true;
        canAttackSiAnim = true;
    }
    IEnumerator CanWalkCooldown()
    {
        canWalkSi = false;
        yield return new WaitForSeconds(0.5f);
        canWalkSi = true;
    }

    // Pause soundtrack, set isPaused variable to true and stop time
    IEnumerator TimeStop()
    {
        PauseMenu.isPaused = true;
        if (ostGO != null)
            ostGO.GetComponent<OST>().WhenPaused();
        yield return null;
        Time.timeScale = 0;
    }

    // Characters elements control

    public void SwitchToRifler()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(false);
        }
    }

    public void SwitchToSniper()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(true);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(false);
        }
    }

    public void SwitchToSickler()
    {
        foreach (GameObject go in riflerElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sniperElements)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in sicklerElements)
        {
            go.SetActive(true);
        }
    }
}
