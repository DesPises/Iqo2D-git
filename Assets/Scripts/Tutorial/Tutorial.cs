using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public static KeyCode riflerKey, sniperKey, sicklerKey, fwKey, bwKey, attackKey, reloadKey;
    public GameObject riflerGO, sniperGO, sicklerGO, h1, h2, h3, h4, h5, h6, h7, h8, hr1, hr2, hr3, hr4, hr5, hr6, hr7, hr8;
    private Transform plRPosition, plSPosition, plSiPosition;
    public Vector3 plRCoordinates, plSCoordinates, plSiCoordinates;
    public static bool Change, CanChangeR, CanChangeS, CanChangeSi, isChanged;
    public static int hint = 0, hintHelper = 0;
    public Animator rAnim, sAnim, siAnim;

    void Start()
    {
        Player.character = "Rifler";
        riflerKey = InputManager.IM.torKey;
        sniperKey = InputManager.IM.tosKey;
        sicklerKey = InputManager.IM.tosiKey;
        fwKey = InputManager.IM.fwKey;
        bwKey = InputManager.IM.bwKey;
        attackKey = InputManager.IM.attackKey;
        reloadKey = InputManager.IM.reloadKey;
        Change = false;
        CanChangeR = false;
        CanChangeS = false;
        CanChangeSi = false;
        isChanged = false;
    }

    void Update()
    {
        StartCoroutine(Rifler.Instance.InfiniteAmmo(9999999));
        StartCoroutine(Sniper.Instance.InfiniteAmmo(9999999));

        //English
        if (Language.eng)
        {
            if (Input.GetKeyDown(fwKey) && hintHelper == 0)
            {
                hintHelper = 1;
            }
            if (hint == 0)
            {
                h1.GetComponent<Text>().text = InputManager.IM.bwKey.ToString() + " / " + InputManager.IM.fwKey.ToString() + " - move";
                h1.SetActive(true);
            }

            if (Input.GetKeyDown(bwKey) && hintHelper == 1)
            {
                hint = 1;
            }
            if (hint == 1)
            {
                h2.GetComponent<Text>().text = InputManager.IM.crouchKey.ToString() + " - crouch, " + InputManager.IM.attackKey.ToString() + " - attack";
                h1.SetActive(false);
                h2.SetActive(true);
                hintHelper = 2;
            }

            if (Hints.hint == 2)
            {
                hint = 2;
            }
            if (hint == 2)
            {
                h3.GetComponent<Text>().text = InputManager.IM.tosiKey.ToString() + " - change character";
                h2.SetActive(false);
                h3.SetActive(true);
                CanChangeSi = true;
            }

            if (CanChangeSi && Input.GetKeyDown(sicklerKey))
            {
                StartCoroutine(Hint3());
            }
            if (hint == 3)
            {
                h3.SetActive(false);
                h4.SetActive(true);
            }

            if (Player.character == "Sickler" && Input.GetKey(attackKey))
            {
                hint = 4;
            }
            if (hint == 4)
            {
                h5.GetComponent<Text>().text = InputManager.IM.tosKey.ToString() + " - change character";
                h4.SetActive(false);
                h5.SetActive(true);
                CanChangeS = true;
            }

            if (hint == 4 && Input.GetKeyDown(sniperKey))
            {
                hint = 5;
            }
            if (hint == 5)
            {
                h6.GetComponent<Text>().text = InputManager.IM.jumpKey.ToString() + " x2 - double jump \n Headshots deal more damage";
                h5.SetActive(false);
                h6.SetActive(true);
            }

            if (Hints.hint == 6)
            {
                hint = 6;
                CanChangeR = true;
            }
            if (hint == 6)
            {
                h7.GetComponent<Text>().text = InputManager.IM.torKey.ToString() + " - change character";
                h6.SetActive(false);
                h7.SetActive(true);
            }

            if (hint == 6 && Input.GetKeyDown(riflerKey))
            {
                hint = 7;
                Hints.hint = 7;
            }
            if (hint == 7)
            {
                h8.GetComponent<Text>().text = InputManager.IM.reloadKey.ToString() + " - reload";
                h7.SetActive(false);
                h8.SetActive(true);
            }
            if (hint == 7 && Input.GetKeyDown(reloadKey))
            {
                StartCoroutine(TutorialEnd());
            }
        }

        //Russian
        if (!Language.eng)
        {
            if (Input.GetKeyDown(fwKey) && hintHelper == 0)
            {
                hintHelper = 1;
            }
            if (hint == 0)
            {
                hr1.GetComponent<Text>().text = InputManager.IM.bwKey.ToString() + " / " + InputManager.IM.fwKey.ToString() + " - бег";
                hr1.SetActive(true);
            }

            if (Input.GetKeyDown(bwKey) && hintHelper == 1)
            {
                hint = 1;
            }
            if (hint == 1)
            {
                hr2.GetComponent<Text>().text = InputManager.IM.crouchKey.ToString() + " - присесть, " + InputManager.IM.attackKey.ToString() + " - атака";
                hr1.SetActive(false);
                hr2.SetActive(true);
                hintHelper = 2;
            }

            if (Hints.hint == 2)
            {
                hint = 2;
            }
            if (hint == 2)
            {
                hr3.GetComponent<Text>().text = InputManager.IM.tosiKey.ToString() + " - смена персонажа";
                hr2.SetActive(false);
                hr3.SetActive(true);
                CanChangeSi = true;
            }

            if (CanChangeSi && Input.GetKeyDown(sicklerKey))
            {
                StartCoroutine(Hint3());
            }
            if (hint == 3)
            {
                hr3.SetActive(false);
                hr4.SetActive(true);
            }

            if (Player.character == "Sickler" && Input.GetKey(attackKey))
            {
                hint = 4;
            }
            if (hint == 4)
            {
                hr5.GetComponent<Text>().text = InputManager.IM.tosKey.ToString() + " - смена персонажа";
                hr4.SetActive(false);
                hr5.SetActive(true);
                CanChangeS = true;
            }

            if (hint == 4 && Input.GetKeyDown(sniperKey))
            {
                hint = 5;
            }
            if (hint == 5)
            {
                hr6.GetComponent<Text>().text = InputManager.IM.jumpKey.ToString() + " x2 - двойной прыжок \n Хедшоты наносят больше урона";
                hr5.SetActive(false);
                hr6.SetActive(true);
            }

            if (Hints.hint == 6)
            {
                hint = 6;
                CanChangeR = true;
            }
            if (hint == 6)
            {
                hr7.GetComponent<Text>().text = InputManager.IM.torKey.ToString() + " - смена персонажа";
                hr6.SetActive(false);
                hr7.SetActive(true);
            }

            if (hint == 6 && Input.GetKeyDown(riflerKey))
            {
                hint = 7;
                Hints.hint = 7;
            }
            if (hint == 7)
            {
                hr8.GetComponent<Text>().text = InputManager.IM.reloadKey.ToString() + " - перезарядка";
                hr7.SetActive(false);
                hr8.SetActive(true);
            }
            if (hint == 7 && Input.GetKeyDown(reloadKey))
            {
                StartCoroutine(TutorialEnd());
            }
        }


        //Coordinates
        if (Player.character == "Rifler")
        {
            plRPosition = riflerGO.transform;
            plRCoordinates = new Vector3(plRPosition.position.x, plRPosition.position.y - 2, plRPosition.position.z);
        }
        if (Player.character == "Sniper")
        {
            plSPosition = sniperGO.transform;
            plSCoordinates = new Vector3(plSPosition.position.x, plSPosition.position.y - 2, plSPosition.position.z);
        }
        if (Player.character == "Sickler")
        {
            plSiPosition = sicklerGO.transform;
            plSiCoordinates = new Vector3(plSiPosition.position.x, plSiPosition.position.y + 2, plSiPosition.position.z);
        }


        //Characters pick

        //Pick rifler
        if (CanChangeR && Input.GetKeyDown(riflerKey) && Player.character == "Sniper")
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            Player.character = "Rifler";
            riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());

        }
        if (CanChangeR && Input.GetKeyDown(riflerKey) && Player.character == "Sickler")
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            Player.character = "Rifler";
            riflerGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
        }
        //Pick sniper
        if (CanChangeS && Input.GetKeyDown(sniperKey) && Player.character == "Rifler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            Player.character = "Sniper";
            sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());
        }
        if (CanChangeS && Input.GetKeyDown(sniperKey) && Player.character == "Sickler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            Player.character = "Sniper";
            sniperGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
        }
        //Pick sickler
        if (CanChangeSi && Input.GetKeyDown(sicklerKey) && Player.character == "Rifler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            Player.character = "Sickler";
            sicklerGO.transform.position = plRCoordinates;
            StartCoroutine(ChangeVoid());
        }
        if (CanChangeSi && Input.GetKeyDown(sicklerKey) && Player.character == "Sniper")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            Player.character = "Sickler";
            sicklerGO.transform.position = plSCoordinates;
            StartCoroutine(ChangeVoid());
        }
    }

    IEnumerator ChangeVoid()
    {
        yield return null;
        Change = true;
        //Change animations
        if (Player.character == "Rifler")
        {
            rAnim.SetBool("change", Change);
        }
        if (Player.character == "Sniper")
        {
            sAnim.SetBool("change", Change);
        }
        if (Player.character == "Sickler")
        {
            siAnim.SetBool("change", Change);
        }
        yield return null;
        Change = false;
        CanChangeR = false;
        CanChangeS = false;
        CanChangeSi = false;
    }

    IEnumerator Hint3()
    {
        Hints.hint = 3;
        yield return null;
        CanChangeSi = false;
        hint = 3;
    }

    IEnumerator TutorialEnd()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(3);
    }
}
