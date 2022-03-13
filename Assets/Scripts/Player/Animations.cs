using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Update()
    {
        anim.SetBool("grounded", Player.onGround);
        anim.SetBool("change", CharacterChangeCode.change);

        //For All

        //Damage
        if (Enemy.doesHitPlayer)
        {
            StartCoroutine(Damage());
        }

        //Rifler
        if (Player.character == "Rifler")
        {
            //Attack
            if (Input.GetKey(InputManager.IM.attackKey) && GameManager.Instance.inMagRInt > 0 && GameManager.Instance.canRAttackAfterReload && !PauseMenu.isPaused)
            {
                Attack();
            }
            else AttackOff();

            //Reload
            if ((Input.GetKeyDown(InputManager.IM.reloadKey) && GameManager.Instance.rCanReload && !GameManager.Instance.rReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.Instance.rDavayReload)
            {
                StartCoroutine(Reload());
            }
        }

        //Sniper
        if (Player.character == "Sniper")
        {
            //Attack
            if (Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.Instance.canAttackS && GameManager.Instance.canSAttackAfterReload && !PauseMenu.isPaused)
            {
                Attack();
            }
            else AttackOff();

            //Reload
            if ((Input.GetKeyDown(InputManager.IM.reloadKey) && GameManager.Instance.sCanReload && !GameManager.Instance.sReloadCooldown && !PauseMenu.isPaused) ||
                  GameManager.Instance.sDavayReload)
            {
                StartCoroutine(Reload());
            }
        }

        //Sickler
        if (Player.character == "Sickler")
        {
            //Attack
            if (Input.GetKeyDown(InputManager.IM.attackKey) && GameManager.Instance.canAttackSiAnim && !PauseMenu.isPaused)
            {
                Attack();
            }
            if (!Input.GetKeyDown(InputManager.IM.attackKey) || !GameManager.Instance.canAttackSiAnim)
            {
                AttackOff();
            }
        }
    }

    public void Run()
    {
        anim.SetBool("run", true);
    }

    public void RunOff()
    {
        anim.SetBool("run", false);
    }

    public void Crouch()
    {
        anim.SetBool("crouch", true);
    }

    public void CrouchOff()
    {
        anim.SetBool("crouch", false);
    }

    public void Attack()
    {
        anim.SetBool("attack", true);
    }

    public void AttackOff()
    {
        anim.SetBool("attack", false);
    }

    public IEnumerator Jump()
    {
        anim.SetBool("jump", true);
        yield return null;
        anim.SetBool("jump", false);
    }
    public IEnumerator SecJump()
    {
        anim.SetBool("2jump", true);
        yield return null;
        anim.SetBool("2jump", false);
        Player.secJump = false;
    }
    public IEnumerator Damage()
    {
        anim.SetBool("dmg", true);
        yield return null;
        anim.SetBool("dmg", false);
    }
    public IEnumerator Reload()
    {
        anim.SetBool("rr", true);
        yield return null;
        anim.SetBool("rr", false);
    }
}
