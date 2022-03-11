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

    [SerializeField] private Rigidbody2D rb;

    // HP
    [SerializeField] private Image HPbarImage;
    // Bullets HUD
    [SerializeField] private Text brtext;
    [SerializeField] private Text bstext;
    [SerializeField] private Text bralltext; 
    [SerializeField] private Text bsalltext;
    public static int riflerDamageInt;
    public static int sniperDamageInt;
    public static int sicklerDamageInt;
    public static int sEnemyHPInt;
    public static int mEnemyHPInt;
    public static int bEnemyHPInt;
    public static int bossHPInt;
    public static int HPRInt;
    public static int HPSInt;
    public static int HPSiInt;

    // KeyCodes
    private KeyCode reloadKey;
    private KeyCode attackKey;
    private KeyCode crouchKey;

    //Variables
    public static float x;
    public static float y;
    public static float z;
    public static bool rCanReload;
    public static bool sCanReload;
    public static bool canAttackR;
    public static bool canAttackS;
    public static bool canAttackSi;
    public static bool canAttackSiAnim;
    public static bool rReloadCooldown;
    public static bool sReloadCooldown;
    public static bool canRAttackAfterReload;
    public static bool canSAttackAfterReload;
    public static bool rDavayReload;
    public static bool sDavayReload;
    public static bool canWalkSi;
    public static bool doesSiAttack;
    public static bool rIsDead;
    public static bool sIsDead;
    public static bool siIsDead;
    public static bool emptySoundCooldown;
    public static int inMagRInt;
    public static int inMagSInt;
    public static int bulletsRAtAllInt;
    public static int bulletsSAtAllInt;
    public static int lastRBulletsInt;
    public static int lastSBulletsInt;
    public static int leftInMagIntR;
    public static int leftInMagIntS;
    public static int comboSi;
    public static int comboSiDelivery;
    public static int damageRInt = 2;
    public static int damageSInt = 18;
    public static int damageSiInt = 12;
    public static int damageRIntHS = 3;
    public static int damageSIntHS = 25;

    void Start()
    {
        Instance = this;

        reloadKey = InputManager.IM.reloadKey;
        attackKey = InputManager.IM.attackKey;
        crouchKey = InputManager.IM.crouchKey;
        x = 0;
        y = 0;
        z = 8;
        HPRInt = 100;
        HPSInt = 60;
        HPSiInt = 140;
        inMagRInt = 30;
        inMagSInt = 5;
        bulletsRAtAllInt = 75;
        bulletsSAtAllInt = 17;
        rCanReload = false;
        sCanReload = false;
        canAttackR = true;
        canAttackS = true;
        canAttackSi = true;
        doesSiAttack = false;
        canAttackSiAnim = true;
        rReloadCooldown = false;
        canRAttackAfterReload = true;
        canSAttackAfterReload = true;
        rDavayReload = false;
        sDavayReload = false;
        comboSi = 0;
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
            dmgSound();
            alienHitSound();
        }
        // Death
        if (HPRInt <= 0)
        {
            rIsDead = true;
            CharacterChangeCode.CanChange = true;
        }
        else rIsDead = false;
        if (HPSInt <= 0)
        {
            sIsDead = true;
            CharacterChangeCode.CanChange = true;
        }
        else sIsDead = false;
        if (HPSiInt <= 0)
        {
            siIsDead = true;
            CharacterChangeCode.CanChange = true;
        }
        else siIsDead = false;

        if (rIsDead && sIsDead && siIsDead)
        {
            DeathMenu.SetActive(true);
            StartCoroutine(TimeStop());
        }

        // Rifler
        if (plMovement.character == "Rifler")
        {
            // HUD, HP and Bullets
            HPbarImage.fillAmount = HPRInt * 0.01f;
            brtext.text = inMagRInt.ToString();
            bralltext.text = "/" + bulletsRAtAllInt.ToString();


            // Reload
            if (Input.GetKeyDown(reloadKey) && rCanReload && !rReloadCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(magFadeRifler());
                StartCoroutine(ReloadCooldownRifler());
            }
            if (inMagRInt == 0 && rCanReload)
            {
                StartCoroutine(magFadeRifler());
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

            if (Input.GetKey(attackKey) && canAttackR && canRAttackAfterReload && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageRInt;
                Enemy.DamageIntHS = damageRIntHS;
                akShootSound();
                StartCoroutine(BulletRCounter());
                if (!Input.GetKey(crouchKey) || (Input.GetKey(crouchKey) && !plMovement.onGround))
                {
                    StartCoroutine(RiflerFire());
                    if (plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRGO, playerRGO.transform.position + new Vector3(0.5f, 4.75f, 0), Quaternion.identity);
                    if (!plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRBackGO, playerRGO.transform.position + new Vector3(-0.5f, 4.75f, 0), Quaternion.identity);
                    if (Input.GetKey(crouchKey))
                    {
                        rFireBigSRCrouch.color = new Color(255, 255, 255, 0);
                        rFireMedSRCrouch.color = new Color(255, 255, 255, 0);
                        rFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
                    }
                }
                if (Input.GetKey(crouchKey) && plMovement.onGround)
                {
                    StartCoroutine(RiflerFireCrouch());
                    if (plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRGO, playerRGO.transform.position + new Vector3(0.5f, 4.0f, 0), Quaternion.identity);
                    if (!plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletRBackGO, playerRGO.transform.position + new Vector3(-0.5f, 4.0f, 0), Quaternion.identity);
                    if (!Input.GetKey(crouchKey))
                    {
                        rFireBigSR.color = new Color(255, 255, 255, 0);
                        rFireMedSR.color = new Color(255, 255, 255, 0);
                        rFireSmallSR.color = new Color(255, 255, 255, 0);
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
            if (Input.GetKey(attackKey) && bulletsRAtAllInt == 0 && inMagRInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(emptyMagSound());
            }
        }




        // Sniper
        if (plMovement.character == "Sniper")
        {
            // HUD, HP and Bullets
            HPbarImage.fillAmount = HPSInt * 0.0167f;
            bstext.text = inMagSInt.ToString();
            bsalltext.text = "/" + bulletsSAtAllInt.ToString();

            // Reload
            if (Input.GetKeyDown(reloadKey) && sCanReload && !sReloadCooldown && bulletsSAtAllInt > 0 && !PauseMenu.isPaused)
            {
                StartCoroutine(magFadeSniper());
                StartCoroutine(ReloadCooldownSniper());
            }
            if (inMagSInt == 0 && sCanReload)
            {
                StartCoroutine(magFadeSniper());
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

            if (Input.GetKeyDown(attackKey) && canAttackS && canSAttackAfterReload && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSInt;
                Enemy.DamageIntHS = damageSIntHS;
                svdShootSound();
                StartCoroutine(BulletSCounter());
                if (!Input.GetKey(crouchKey) || (Input.GetKey(crouchKey) && !plMovement.onGround))
                {
                    StartCoroutine(SniperFire());
                    if (plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.67f, 0), Quaternion.identity);
                    if (!plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSBackGO, playerSGO.transform.position + new Vector3(-0.5f, 4.67f, 0), Quaternion.identity);
                }
                if (Input.GetKey(crouchKey) && plMovement.onGround)
                {
                    StartCoroutine(SniperFireCrouch());
                    if (plMovement.isMovingFW)
                        bcloneGO = Instantiate(bulletSGO, playerSGO.transform.position + new Vector3(0.5f, 4.0f, 0), Quaternion.identity);
                    if (!plMovement.isMovingFW)
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

            if (Input.GetKey(attackKey) && bulletsSAtAllInt == 0 && inMagSInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(emptyMagSound());
            }
        }




        // Sickler
        if (plMovement.character == "Sickler")
        {
            // HUD and HP
            HPbarImage.fillAmount = HPSiInt * 0.007f;

            // Attack
            
            if (Input.GetKeyDown(attackKey) && canAttackSi && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSiInt;
                Enemy.DamageIntHS = damageSiInt;
                StartCoroutine(siAttack());
                StartCoroutine(canWalk());
            }
        }
    }

    // Sounds functions
    IEnumerator emptyMagSound()
    {
        emptySoundCooldown = true;
        soundContrGO.GetComponent<SoundController>().emptyMagS();
        yield return new WaitForSeconds(0.4f);
        emptySoundCooldown = false;
    }

    public void dmgSound()
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
        rFireBigSR.color = new Color(255, 255, 255, 190);
        rFireMedSR.color = new Color(255, 255, 255, 0);
        rFireSmallSR.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        rFireBigSR.color = new Color(255, 255, 255, 0);
        rFireMedSR.color = new Color(255, 255, 255, 190);
        rFireSmallSR.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        rFireBigSR.color = new Color(255, 255, 255, 0);
        rFireMedSR.color = new Color(255, 255, 255, 0);
        rFireSmallSR.color = new Color(255, 255, 255, 190);
        yield return new WaitForSeconds(0.03f);
        rFireSmallSR.color = new Color(255, 255, 255, 0);
    }
    IEnumerator RiflerFireCrouch()
    {
        rFireBigSRCrouch.color = new Color(255, 255, 255, 190);
        rFireMedSRCrouch.color = new Color(255, 255, 255, 0);
        rFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        rFireBigSRCrouch.color = new Color(255, 255, 255, 0);
        rFireMedSRCrouch.color = new Color(255, 255, 255, 190);
        rFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        rFireBigSRCrouch.color = new Color(255, 255, 255, 0);
        rFireMedSRCrouch.color = new Color(255, 255, 255, 0);
        rFireSmallSRCrouch.color = new Color(255, 255, 255, 190);
        yield return new WaitForSeconds(0.03f);
        rFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
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
        if (bulletsRAtAllInt > 29)
        {
            leftInMagIntR = inMagRInt;
            inMagRInt = 30;
            bulletsRAtAllInt -= (30 - leftInMagIntR);
        }
        else
        {
            if (inMagRInt == 0)
            {
                inMagRInt = bulletsRAtAllInt;
                bulletsRAtAllInt = 0;
            }
            else
            {
                leftInMagIntR = inMagRInt;
                if (inMagRInt + bulletsRAtAllInt > 30)
                {
                    inMagRInt = 30;
                    bulletsRAtAllInt -= (30 - leftInMagIntR);
                }
                else
                {
                    inMagRInt += bulletsRAtAllInt;
                    bulletsRAtAllInt = 0;
                }
            }
        }
        yield return new WaitForSeconds(0.01f);
        rReloadCooldown = true;
        yield return new WaitForSeconds(0.6f);
        canRAttackAfterReload = true;
        yield return new WaitForSeconds(2f);
        rReloadCooldown = false;
    }
    IEnumerator magFadeRifler()
    {
        if (plMovement.isMovingFW)
        {
            cloneGO = Instantiate(magRGO, playerRGO.transform.position + new Vector3(0.6f, 2.9f, 0), Quaternion.Euler(x, y, z));
            cloneGO.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (!plMovement.isMovingFW)
        {
            cloneGO = Instantiate(magRGO, playerRGO.transform.position + new Vector3(-0.6f, 2.9f, 0), Quaternion.Euler(x, y, z));
            cloneGO.gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        yield return new WaitForSeconds(0.01f);
        rCanReload = false;
        yield return new WaitForSeconds(1);
        Destroy(cloneGO);
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
        sFireBigSR.color = new Color(255, 255, 255, 190);
        sFireMedSR.color = new Color(255, 255, 255, 0);
        sFireSmallSR.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        sFireBigSR.color = new Color(255, 255, 255, 0);
        sFireMedSR.color = new Color(255, 255, 255, 190);
        sFireSmallSR.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        sFireBigSR.color = new Color(255, 255, 255, 0);
        sFireMedSR.color = new Color(255, 255, 255, 0);
        sFireSmallSR.color = new Color(255, 255, 255, 190);
        yield return new WaitForSeconds(0.03f);
        sFireSmallSR.color = new Color(255, 255, 255, 0);

    }
    IEnumerator SniperFireCrouch()
    {
        sFireBigSRCrouch.color = new Color(255, 255, 255, 190);
        sFireMedSRCrouch.color = new Color(255, 255, 255, 0);
        sFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        sFireBigSRCrouch.color = new Color(255, 255, 255, 0);
        sFireMedSRCrouch.color = new Color(255, 255, 255, 190);
        sFireSmallSRCrouch.color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(0.03f);
        sFireBigSRCrouch.color = new Color(255, 255, 255, 0);
        sFireMedSRCrouch.color = new Color(255, 255, 255, 0);
        sFireSmallSRCrouch.color = new Color(255, 255, 255, 190);
        yield return new WaitForSeconds(0.03f);
        sFireSmallSRCrouch.color = new Color(255, 255, 255, 0);

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
        if (bulletsSAtAllInt > 4)
        {
            leftInMagIntS = inMagSInt;
            inMagSInt = 5;
            bulletsSAtAllInt -= (5 - leftInMagIntS);
        }
        else
        {
            if (inMagSInt == 0)
            {
                inMagSInt = bulletsSAtAllInt;
                bulletsSAtAllInt = 0;
            }
            else
            {
                leftInMagIntS = inMagSInt;
                if (inMagSInt + bulletsSAtAllInt > 5)
                {
                    inMagSInt = 5;
                    bulletsSAtAllInt -= (5 - leftInMagIntS);
                }
                else
                {
                    inMagSInt += bulletsSAtAllInt;
                    bulletsSAtAllInt = 0;
                }
            }
        }
        yield return new WaitForSeconds(0.6f);
        canSAttackAfterReload = true;
        yield return new WaitForSeconds(3f);
        sReloadCooldown = false;
    }
    IEnumerator magFadeSniper()
    {
        yield return new WaitForSeconds(0.01f);
        sCanReload = false;

        yield return new WaitForSeconds(0.15f);
        if (plMovement.isMovingFW && !Input.GetKey(crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(0.4f, 3, 0), Quaternion.identity);
        }
        if (!plMovement.isMovingFW && !Input.GetKey(crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-0.4f, 3, 0), Quaternion.identity);
        }
        if (plMovement.isMovingFW && Input.GetKey(crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(1, 2.5f, 0), Quaternion.identity);
        }
        if (!plMovement.isMovingFW && Input.GetKey(crouchKey))
        {
            cloneGO = Instantiate(magSGO, playerSGO.transform.position + new Vector3(-1, 2.5f, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(0.84f);
        Destroy(cloneGO);
    }




    // Sickler coroutines
    IEnumerator siAttack()
    {
        plMovement.secJump = false;
        if (plMovement.isMovingFW)
        {
            rb.velocity = new Vector2(2, 0);
            rb.AddForce(Vector2.down * 40 + Vector2.right * 50, ForceMode2D.Impulse);
        }
        if (!plMovement.isMovingFW)
        {
            rb.velocity = new Vector2(-2, 0);
            rb.AddForce(Vector2.down * 40 + Vector2.left * 50, ForceMode2D.Impulse);
        }
        canAttackSi = false;
        doesSiAttack = true;
        siAttackCombo();
        yield return null;
        canAttackSiAnim = false;

        comboSi = comboSiDelivery;
        yield return new WaitForSeconds(0.4f);
        doesSiAttack = false;
        canAttackSi = true;
        canAttackSiAnim = true;
    }
    IEnumerator canWalk()
    {
        canWalkSi = false;
        yield return new WaitForSeconds(0.5f);
        canWalkSi = true;
    }
    void siAttackCombo()
    {
        if (comboSi == 1)
        {
            comboSiDelivery = 0;
        }
        if (comboSi == 0)
        {
            comboSiDelivery = 0;
        }

    }

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
