using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator anim;
    public KeyCode jumpKey, fwKey, bwKey, crouchKey, attackKey, reloadKey, rPickKey, sPickKey, siPickKey;

    void Start()
    {
        jumpKey = InputManager.IM.jumpKey;
        fwKey = InputManager.IM.fwKey;
        bwKey = InputManager.IM.bwKey;
        attackKey = InputManager.IM.attackKey;
        crouchKey = InputManager.IM.crouchKey;
        reloadKey = InputManager.IM.reloadKey;
        rPickKey = InputManager.IM.torKey;
        sPickKey = InputManager.IM.tosKey;
        siPickKey = InputManager.IM.tosiKey;
    }

    void Update()
    {
        //For All

        //Run
        if ((Input.GetKey(fwKey) || Input.GetKey(bwKey)) && !Input.GetKey(crouchKey) && !PauseMenu.isPaused)
        {
            anim.SetBool("run", true);
        }
        if (!Input.GetKey(fwKey) && !Input.GetKey(bwKey))
        {
            anim.SetBool("run", false);
        }

        //Jump
        if (Input.GetKeyDown(jumpKey) && PlayerMovement.onGround && !PauseMenu.isPaused)
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(jumpKey) && !PlayerMovement.onGround && PlayerMovement.secJump && !PauseMenu.isPaused)
        {
            StartCoroutine(SecJump());
        }
        anim.SetBool("grounded", PlayerMovement.onGround);

        //Crouch
        if (Input.GetKey(crouchKey) && PlayerMovement.onGround && !PauseMenu.isPaused)
        {
            anim.SetBool("crouch", true);
        }
        if (!Input.GetKey(crouchKey))
        {
            anim.SetBool("crouch", false);
        }

        //Damage
        if (Enemy.doesHitPlayer)
        {
            StartCoroutine(Damage());
        }

        //Change
        anim.SetBool("change", CharacterChangeCode.change);



        //Rifler
        if (PlayerMovement.character == "Rifler")
        {
            //Attack
            if (Input.GetKey(attackKey) && GameManager.Instance.inMagRInt > 0 && GameManager.Instance.canRAttackAfterReload && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            else anim.SetBool("attack", false);

            //Reload
            if ((Input.GetKeyDown(reloadKey) && GameManager.Instance.rCanReload && !GameManager.Instance.rReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.Instance.rDavayReload)
            {
                StartCoroutine(Reload());
            }
        }



        //Sniper
        if (PlayerMovement.character == "Sniper")
        {
            //Attack
            if (Input.GetKeyDown(attackKey) && GameManager.Instance.canAttackS && GameManager.Instance.canSAttackAfterReload && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            else anim.SetBool("attack", false);

            //Reload
            if ((Input.GetKeyDown(reloadKey) && GameManager.Instance.sCanReload && !GameManager.Instance.sReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.Instance.sDavayReload)
            {
                StartCoroutine(Reload());
            }
        }



        //Sickler
        if (PlayerMovement.character == "Sickler")
        {
            //Attack
            if (Input.GetKeyDown(attackKey) && GameManager.Instance.canAttackSiAnim && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            if (!Input.GetKeyDown(attackKey) || !GameManager.Instance.canAttackSiAnim)
            {
                anim.SetBool("attack", false);
            }
        }
    }

    IEnumerator Jump()
    {
        anim.SetBool("jump", true);
        yield return null;
        anim.SetBool("jump", false);
    }
    IEnumerator SecJump()
    {
        anim.SetBool("2jump", true);
        yield return null;
        anim.SetBool("2jump", false);
        PlayerMovement.secJump = false;
    }
    IEnumerator Damage()
    {
        anim.SetBool("dmg", true);
        yield return null;
        anim.SetBool("dmg", false);
    }
    IEnumerator Reload()
    {
        anim.SetBool("rr", true);
        yield return null;
        anim.SetBool("rr", false);
    }
}
