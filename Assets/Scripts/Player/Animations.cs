using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Update()
    {
        anim.SetBool("grounded", Player .onGround);
        anim.SetBool("change", CharacterChangeCode.change);
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

    public IEnumerator AttackOff()
    {
        yield return null;
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
