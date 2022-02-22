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
        if (Input.GetKeyDown(jumpKey) && plMovement.onGround && !PauseMenu.isPaused)
        {
            StartCoroutine(Jump());
        }
        if (Input.GetKeyDown(jumpKey) && !plMovement.onGround && plMovement.secJump && !PauseMenu.isPaused)
        {
            StartCoroutine(SecJump());
        }
        anim.SetBool("grounded", plMovement.onGround);

        //Crouch
        if (Input.GetKey(crouchKey) && plMovement.onGround && !PauseMenu.isPaused)
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
        anim.SetBool("change", CharacterChangeCode.Change);
        anim.SetBool("changed", CharacterChangeCode.isChanged);



        //Rifler
        if (plMovement.character == "Rifler")
        {
            //Attack
            if (Input.GetKey(attackKey) && GameManager.inMagRInt > 0 && GameManager.canRAttackAfterReload && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            else anim.SetBool("attack", false);

            //Reload
            if ((Input.GetKeyDown(reloadKey) && GameManager.rCanReload && !GameManager.rReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.rDavayReload)
            {
                StartCoroutine(Reload());
            }
        }



        //Sniper
        if (plMovement.character == "Sniper")
        {
            //Attack
            if (Input.GetKeyDown(attackKey) && GameManager.canAttackS && GameManager.canSAttackAfterReload && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            else anim.SetBool("attack", false);

            //Reload
            if ((Input.GetKeyDown(reloadKey) && GameManager.sCanReload && !GameManager.sReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.sDavayReload)
            {
                StartCoroutine(Reload());
            }
        }



        //Sickler
        if (plMovement.character == "Sickler")
        {
            //Attack
            anim.SetInteger("combo", GameManager.comboSi);
            if (Input.GetKeyDown(attackKey) && GameManager.canAttackSiAnim && !PauseMenu.isPaused)
            {
                anim.SetBool("attack", true);
            }
            if (!Input.GetKeyDown(attackKey) || !GameManager.canAttackSiAnim)
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
        plMovement.secJump = false;
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
