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
    [SerializeField] private GameObject bulletRGO;
    [SerializeField] private GameObject bulletRBackGO;
    [SerializeField] private GameObject bulletSGO;
    [SerializeField] private GameObject bulletSBackGO;
    [SerializeField] private GameObject magRGO;
    [SerializeField] private GameObject magSGO;
    [SerializeField] private GameObject playerRGO;
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

    [SerializeField] private SpriteRenderer rFireBigSR;
    [SerializeField] private SpriteRenderer rFireMedSR;
    [SerializeField] private SpriteRenderer rFireSmallSR;
    [SerializeField] private SpriteRenderer rFireBigSRCrouch;
    [SerializeField] private SpriteRenderer rFireMedSRCrouch;
    [SerializeField] private SpriteRenderer rFireSmallSRCrouch;
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
    public static int HPRInt;
    public static int HPSInt;
    public static int HPSiInt;

    public bool rCanReload;
    public bool sCanReload;
    public bool canAttackR;
    public bool canAttackS;
    public bool canAttackSi;
    public bool canAttackSiAnim;

    public bool rReloadCooldown;
    public bool sReloadCooldown;
    public bool canRAttackAfterReload;
    public bool canSAttackAfterReload;

    public bool rDavayReload { get; private set; }
    public bool sDavayReload { get; private set; }

    public bool canWalkSi { get; private set; }
    public bool doesSiAttack { get; private set; }

    public bool rIsDead;
    public bool sIsDead;
    public bool siIsDead;

    public bool emptySoundCooldown;
    public int inMagRInt;
    public int inMagSInt;

    public int bulletsRAtAllInt;
    public int bulletsSAtAllInt;
    public int lastRBulletsInt;
    public int lastSBulletsInt;

    public int damageRInt = 2;
    public int damageRIntHS = 3;

    private readonly int damageSInt = 18;
    private readonly int damageSiInt = 12;
    private readonly int damageSIntHS = 25;

    void Start()
    {
        Instance = this;

        // Set default HP for every character
        HPRInt = 100;
        HPSInt = 60;
        HPSiInt = 140;
        // Set default ammo for rifler and sniper
        inMagRInt = 30;
        inMagSInt = 5;
        bulletsRAtAllInt = 75;
        bulletsSAtAllInt = 17;

        canAttackR = true;
        canAttackS = true;
        canAttackSi = true;
        canAttackSiAnim = true;
        canRAttackAfterReload = true;
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
        // Death
        if (HPRInt <= 0)
        {
            rIsDead = true;
            CharacterChangeCode.canChange = true;
        }
        else rIsDead = false;
        if (HPSInt <= 0)
        {
            sIsDead = true;
            CharacterChangeCode.canChange = true;
        }
        else sIsDead = false;
        if (HPSiInt <= 0)
        {
            siIsDead = true;
            CharacterChangeCode.canChange = true;
        }
        else siIsDead = false;

        if (rIsDead && sIsDead && siIsDead)
        {
            DeathMenu.SetActive(true);
            StartCoroutine(TimeStop());
        }

        // Rifler
        if (PlayerMovement.character == "Rifler")
        {
            // HP bar and ammo
            HPbarImage.fillAmount = HPRInt * 0.01f;
            brtext.text = inMagRInt.ToString();
            bralltext.text = "/" + bulletsRAtAllInt.ToString();


            // Reload
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && rCanReload && !rReloadCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(MagFadeRifler());
                StartCoroutine(ReloadCooldownRifler());
            }
            if (inMagRInt == 0 && rCanReload)
            {
                StartCoroutine(MagFadeRifler());
                StartCoroutine(ReloadCooldownRifler());
                StartCoroutine(ReloadRAnim());

            }

            if (inMagRInt < 30 && bulletsRAtAllInt > 0)
                rCanReload = true;

            if (bulletsRAtAllInt <= 0)
                rCanReload = false;

            if (inMagRInt <= 0 && bulletsRAtAllInt <= 0)
                canAttackR = false;

            // Attack

            if (Input.GetKey(InputManager.IM.attackKey) && canAttackR && canRAttackAfterReload && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageRInt;
                Enemy.DamageIntHS = damageRIntHS;
                akShootSound();
                StartCoroutine(BulletRCounter());
                if (!Input.GetKey(InputManager.IM.crouchKey) || (Input.GetKey(InputManager.IM.crouchKey) && !PlayerMovement.onGround))
                {
                    StartCoroutine(RiflerFire());
                    if (PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRGO, playerRGO.transform.position + new Vector3(0.5f, 4.75f, 0), Quaternion.identity);
                    if (!PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRBackGO, playerRGO.transform.position + new Vector3(-0.5f, 4.75f, 0), Quaternion.identity);
                    if (Input.GetKey(InputManager.IM.crouchKey))
                    {
                        rFireBigSRCrouch.color = invisible;
                        rFireMedSRCrouch.color = invisible;
                        rFireSmallSRCrouch.color = invisible;
                    }
                }
                if (Input.GetKey(InputManager.IM.crouchKey) && PlayerMovement.onGround)
                {
                    StartCoroutine(RiflerFireCrouch());
                    if (PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRGO, playerRGO.transform.position + new Vector3(0.5f, 4.0f, 0), Quaternion.identity);
                    if (!PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRBackGO, playerRGO.transform.position + new Vector3(-0.5f, 4.0f, 0), Quaternion.identity);
                    if (!Input.GetKey(InputManager.IM.crouchKey))
                    {
                        rFireBigSR.color = invisible;
                        rFireMedSR.color = invisible;
                        rFireSmallSR.color = invisible;
                    }
                }

            }
            if (Bullet.doesBulletHit || BulletBack.doesBulletHit)
            {
                Bullet.doesBulletHit = false;
                BulletBack.doesBulletHit = false;
                Destroy(bcloneGO);
            }

            // Empty
            if (Input.GetKey(InputManager.IM.attackKey) && bulletsRAtAllInt == 0 && inMagRInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(EmptyMagSound());
            }
        }




        // Sniper
        if (PlayerMovement.character == "Sniper")
        {
            // HP bar and ammo
            HPbarImage.fillAmount = HPSInt * 0.0167f;
            bstext.text = inMagSInt.ToString();
            bsalltext.text = "/" + bulletsSAtAllInt.ToString();

            // Reload
            if (Input.GetKeyDown(InputManager.IM.reloadKey) && sCanReload && !sReloadCooldown && bulletsSAtAllInt > 0 && !PauseMenu.isPaused)
            {
                StartCoroutine(MagFadeSniper());
                StartCoroutine(ReloadCooldownSniper());
            }
            else if (inMagSInt == 0 && sCanReload)
            {
                StartCoroutine(MagFadeSniper());
                StartCoroutine(ReloadCooldownSniper());
                sReloadCooldown = false;
                StartCoroutine(ReloadSAnim());
            }
            
            if (inMagSInt < 5 && bulletsSAtAllInt > 0)
                sCanReload = true;

            if (bulletsSAtAllInt <= 0)
                sCanReload = false;

            if (inMagSInt <= 0 && bulletsSAtAllInt <= 0)
                canAttackS = false;

            // Attack

            if (Input.GetKeyDown(InputManager.IM.attackKey) && canAttackS && canSAttackAfterReload && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSInt;
                Enemy.DamageIntHS = damageSIntHS;
                svdShootSound();
                StartCoroutine(BulletSCounter());
                if (!Input.GetKey(InputManager.IM.crouchKey) || (Input.GetKey(InputManager.IM.crouchKey) && !PlayerMovement.onGround))
                {
                    StartCoroutine(SniperFire());
                    if (PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.67f, 0), Quaternion.identity);
                    if (!PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSBackGO, playerSGO.transform.position + new Vector3(-0.5f, 4.67f, 0), Quaternion.identity);
                }
                if (Input.GetKey(InputManager.IM.crouchKey) && PlayerMovement.onGround)
                {
                    StartCoroutine(SniperFireCrouch());
                    if (PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.0f, 0), Quaternion.identity);
                    if (!PlayerMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSBackGO, playerSGO.transform.position + new Vector3(-0.5f, 4.0f, 0), Quaternion.identity);
                }
            }
            if (Bullet.doesBulletHit || BulletBack.doesBulletHit)
            {
                Bullet.doesBulletHit = false;
                BulletBack.doesBulletHit = false;
                Destroy(bcloneGO);
            }
            // Empty

            if (Input.GetKey(InputManager.IM.attackKey) && bulletsSAtAllInt == 0 && inMagSInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(EmptyMagSound());
            }
        }




        // Sickler
        if (PlayerMovement.character == "Sickler")
        {
            // HP bar
            HPbarImage.fillAmount = HPSiInt * 0.007f;

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
    public void akShootSound()
    {
        soundContrGO.GetComponent<SoundController>().akShootS();
    }


    // Rifler coroutines
    IEnumerator BulletRCounter()
    {
        canAttackR = false;
        yield return new WaitForSeconds(0.01f);
        inMagRInt--;
        yield return new WaitForSeconds(0.36f);
        Destroy(bcloneGO);
        canAttackR = true;
    }
    IEnumerator RiflerFire()
    {
        rFireBigSR.color = visible;
        rFireMedSR.color = invisible;
        rFireSmallSR.color = invisible;
        yield return new WaitForSeconds(0.03f);
        rFireBigSR.color = invisible;
        rFireMedSR.color = visible;
        rFireSmallSR.color = invisible;
        yield return new WaitForSeconds(0.03f);
        rFireBigSR.color = invisible;
        rFireMedSR.color = invisible;
        rFireSmallSR.color = visible;
        yield return new WaitForSeconds(0.03f);
        rFireSmallSR.color = invisible;
    }
    IEnumerator RiflerFireCrouch()
    {
        rFireBigSRCrouch.color = visible;
        rFireMedSRCrouch.color = invisible;
        rFireSmallSRCrouch.color = invisible;
        yield return new WaitForSeconds(0.03f);
        rFireBigSRCrouch.color = invisible;
        rFireMedSRCrouch.color = visible;
        rFireSmallSRCrouch.color = invisible;
        yield return new WaitForSeconds(0.03f);
        rFireBigSRCrouch.color = invisible;
        rFireMedSRCrouch.color = invisible;
        rFireSmallSRCrouch.color = visible;
        yield return new WaitForSeconds(0.03f);
        rFireSmallSRCrouch.color = invisible;
    }
    IEnumerator ReloadRAnim()
    {
        rDavayReload = true;
        yield return null;
        rDavayReload = false;
    }
    IEnumerator ReloadCooldownRifler()
    {
        canRAttackAfterReload = false;
        int leftInMag = inMagRInt;

        if (inMagRInt + bulletsRAtAllInt > 30)
        {
            inMagRInt = 30;
            bulletsRAtAllInt -= (30 - leftInMag);
        }
        else
        {
            inMagRInt += bulletsRAtAllInt;
            bulletsRAtAllInt = 0;
        }
        yield return null;
        rReloadCooldown = true;
        yield return new WaitForSeconds(0.6f);
        canRAttackAfterReload = true;
        yield return new WaitForSeconds(2f);
        rReloadCooldown = false;
    }
    IEnumerator MagFadeRifler()
    {
        if (PlayerMovement.isMovingFW)
        {
            cloneGO = Instantiate(magRGO, playerRGO.transform.position + new Vector3(0.6f, 2.9f, 0), Quaternion.Euler(0, 0, 8));
            cloneGO.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (!PlayerMovement.isMovingFW)
        {
            cloneGO = Instantiate(magRGO, playerRGO.transform.position + new Vector3(-0.6f, 2.9f, 0), Quaternion.Euler(0, 0, 8));
            cloneGO.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        yield return new WaitForSeconds(0.01f);
        rCanReload = false;
        Destroy(cloneGO, 1f);
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

        sCanReload = false;
        yield return new WaitForSeconds(0.15f);

        if (PlayerMovement.isMovingFW && !Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(0.4f, 3), Quaternion.identity);
        }
        else if (!PlayerMovement.isMovingFW && !Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-0.4f, 3), Quaternion.identity);
        }
        else if (PlayerMovement.isMovingFW && Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(1, 2.5f), Quaternion.identity);
        }
        else if (!PlayerMovement.isMovingFW && Input.GetKey(InputManager.IM.crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-1, 2.5f), Quaternion.identity);
        }

        Destroy(cloneGO, 0.85f);
    }




    // Sickler coroutines
    IEnumerator SiAttack()
    {
        PlayerMovement.secJump = false;
        if (PlayerMovement.isMovingFW)
        {
            rb.velocity = new Vector2(2, 0);
        }
        if (!PlayerMovement.isMovingFW)
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
