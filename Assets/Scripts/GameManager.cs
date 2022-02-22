using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Objects
    public GameObject bulletRGO, bulletRBackGO, bulletSGO, bulletSBackGO, magRGO, magSGO, playerRGO, playerSGO, playerSiGO, cloneGO, bcloneGO, HUDrMagGO, HUDsMagGO, HUDrAmmoGO, HUDsAmmoGO, rIconGO, sIconGO, siIconGO,
        sAnimGO, rAnimGO, siAnimGO, soundContrGO, DeathMenu, ostGO;
    public SpriteRenderer rFireBigSR, rFireMedSR, rFireSmallSR, rFireBigSRCrouch, rFireMedSRCrouch, rFireSmallSRCrouch, sFireBigSR, sFireMedSR, sFireSmallSR, sFireBigSRCrouch, sFireMedSRCrouch, sFireSmallSRCrouch;
    public Rigidbody2D rb;

    //HP
    [SerializeField] private Image HPbarImage;
    public static int riflerDamageInt, sniperDamageInt, sicklerDamageInt, sEnemyHPInt, mEnemyHPInt, bEnemyHPInt, bossHPInt, HPRInt, HPSInt, HPSiInt;
    public Text brtext, bstext, bralltext, bsalltext;

    //KeyCodes
    public KeyCode reloadKey, attackKey, crouchKey;

    //Variables
    public static float x, y, z;
    public static bool rCanReload, sCanReload, canAttackR, canAttackS, canAttackSi, canAttackSiAnim, rReloadCooldown, sReloadCooldown, canRAttackAfterReload, canSAttackAfterReload, rDavayReload, sDavayReload, canWalkSi, doesSiAttack,
        rIsDead, sIsDead, siIsDead, emptySoundCooldown;
    public static int inMagRInt, inMagSInt, bulletsRAtAllInt, bulletsSAtAllInt, lastRBulletsInt, lastSBulletsInt, leftInMagIntR, leftInMagIntS, comboSi, comboSiDelivery;
    public static int damageRInt = 2, damageSInt = 18, damageSiInt = 12, damageRIntHS = 3, damageSIntHS = 25;

    void Start()
    {
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
        //Damage sound
        if (Enemy.doesHitPlayer)
        {
            dmgSound();
            alienHitSound();
        }
        //Death
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

        //Rifler
        if (plMovement.character == "Rifler")
        {
            //HUD, HP and Bullets
            HPbarImage.fillAmount = HPRInt * 0.01f;
            brtext.text = inMagRInt.ToString();
            bralltext.text = "/" + bulletsRAtAllInt.ToString();
            HUDrMagGO.gameObject.SetActive(true);
            HUDsMagGO.gameObject.SetActive(false);
            HUDrAmmoGO.gameObject.SetActive(true);
            HUDsAmmoGO.gameObject.SetActive(false);
            rIconGO.gameObject.SetActive(true);
            sIconGO.gameObject.SetActive(false);
            siIconGO.gameObject.SetActive(false);
            rAnimGO.gameObject.SetActive(true);
            sAnimGO.gameObject.SetActive(false);
            siAnimGO.gameObject.SetActive(false);

            //Reload
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

            //Attack

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

            //Empty
            if (Input.GetKey(attackKey) && bulletsRAtAllInt == 0 && inMagRInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(emptyMagSound());
            }
        }




        //Sniper
        if (plMovement.character == "Sniper")
        {
            //HUD, HP and Bullets
            HPbarImage.fillAmount = HPSInt * 0.0167f;
            bstext.text = inMagSInt.ToString();
            bsalltext.text = "/" + bulletsSAtAllInt.ToString();
            HUDrMagGO.gameObject.SetActive(false);
            HUDsMagGO.gameObject.SetActive(true);
            HUDrAmmoGO.gameObject.SetActive(false);
            HUDsAmmoGO.gameObject.SetActive(true);
            rIconGO.gameObject.SetActive(false);
            sIconGO.gameObject.SetActive(true);
            siIconGO.gameObject.SetActive(false);
            rAnimGO.gameObject.SetActive(false);
            sAnimGO.gameObject.SetActive(true);
            siAnimGO.gameObject.SetActive(false);

            //Reload
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

            //Attack

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
            //Empty

            if (Input.GetKey(attackKey) && bulletsSAtAllInt == 0 && inMagSInt == 0 && !emptySoundCooldown && !PauseMenu.isPaused)
            {
                StartCoroutine(emptyMagSound());
            }
        }




        //Sickler
        if (plMovement.character == "Sickler")
        {
            //HUD and HP
            HPbarImage.fillAmount = HPSiInt * 0.007f;
            HUDrMagGO.gameObject.SetActive(false);
            HUDsMagGO.gameObject.SetActive(false);
            HUDrAmmoGO.gameObject.SetActive(false);
            HUDsAmmoGO.gameObject.SetActive(false);
            rIconGO.gameObject.SetActive(false);
            sIconGO.gameObject.SetActive(false);
            siIconGO.gameObject.SetActive(true);
            rAnimGO.gameObject.SetActive(false);
            sAnimGO.gameObject.SetActive(false);
            siAnimGO.gameObject.SetActive(true);


            //Attack
            
            if (Input.GetKeyDown(attackKey) && canAttackSi && !PauseMenu.isPaused)
            {
                Enemy.DamageInt = damageSiInt;
                Enemy.DamageIntHS = damageSiInt;
                StartCoroutine(siAttack());
                StartCoroutine(canWalk());
            }
        }
    }

    //Sounds functions
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


    //Rifler coroutines
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



    //Sniper coroutines
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




    //Sickler coroutines
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
}
