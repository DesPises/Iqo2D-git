using System.Collections;
using System.Collections.Generic;
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
        PlayerMovement.character = "Rifler";
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

            if (PlayerMovement.character == "Sickler" && PlayerMovement.isNearEnemy && Input.GetKey(attackKey))
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
                hr1.GetComponent<Text>().text = InputManager.IM.bwKey.ToString() + " / " + InputManager.IM.fwKey.ToString() + " - ���";
                hr1.SetActive(true);
            }

            if (Input.GetKeyDown(bwKey) && hintHelper == 1)
            {
                hint = 1;
            }
            if (hint == 1)
            {
                hr2.GetComponent<Text>().text = InputManager.IM.crouchKey.ToString() + " - ��������, " + InputManager.IM.attackKey.ToString() + " - �����";
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
                hr3.GetComponent<Text>().text = InputManager.IM.tosiKey.ToString() + " - ����� ���������";
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

            if (PlayerMovement.character == "Sickler" && PlayerMovement.isNearEnemy && Input.GetKey(attackKey))
            {
                hint = 4;
            }
            if (hint == 4)
            {
                hr5.GetComponent<Text>().text = InputManager.IM.tosKey.ToString() + " - ����� ���������";
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
                hr6.GetComponent<Text>().text = InputManager.IM.jumpKey.ToString() + " x2 - ������� ������ \n ������� ������� ������ �����";
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
                hr7.GetComponent<Text>().text = InputManager.IM.torKey.ToString() + " - ����� ���������";
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
                hr8.GetComponent<Text>().text = InputManager.IM.reloadKey.ToString() + " - �����������";
                hr7.SetActive(false);
                hr8.SetActive(true);
            }
            if (hint == 7 && Input.GetKeyDown(reloadKey))
            {
                StartCoroutine(TutorialEnd());
            }
        }


        //Coordinates
        if (PlayerMovement.character == "Rifler")
        {
            plRPosition = riflerGO.transform;
            plRCoordinates = new Vector3(plRPosition.position.x, plRPosition.position.y - 2, plRPosition.position.z);
        }
        if (PlayerMovement.character == "Sniper")
        {
            plSPosition = sniperGO.transform;
            plSCoordinates = new Vector3(plSPosition.position.x, plSPosition.position.y - 2, plSPosition.position.z);
        }
        if (PlayerMovement.character == "Sickler")
        {
            plSiPosition = sicklerGO.transform;
            plSiCoordinates = new Vector3(plSiPosition.position.x, plSiPosition.position.y + 2, plSiPosition.position.z);
        }


        //Characters pick

        //Pick rifler
        if (CanChangeR && Input.GetKeyDown(riflerKey) && PlayerMovement.character == "Sniper")
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            PlayerMovement.character = "Rifler";
            riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());

        }
        if (CanChangeR && Input.GetKeyDown(riflerKey) && PlayerMovement.character == "Sickler")
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            PlayerMovement.character = "Rifler";
            riflerGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
        }
        //Pick sniper
        if (CanChangeS && Input.GetKeyDown(sniperKey) && PlayerMovement.character == "Rifler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            PlayerMovement.character = "Sniper";
            sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());
        }
        if (CanChangeS && Input.GetKeyDown(sniperKey) && PlayerMovement.character == "Sickler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            PlayerMovement.character = "Sniper";
            sniperGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
        }
        //Pick sickler
        if (CanChangeSi && Input.GetKeyDown(sicklerKey) && PlayerMovement.character == "Rifler")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            PlayerMovement.character = "Sickler";
            sicklerGO.transform.position = plRCoordinates;
            StartCoroutine(ChangeVoid());
        }
        if (CanChangeSi && Input.GetKeyDown(sicklerKey) && PlayerMovement.character == "Sniper")
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            PlayerMovement.character = "Sickler";
            sicklerGO.transform.position = plSCoordinates;
            StartCoroutine(ChangeVoid());
        }

        //Infinite ammo

        if (GameManager.Instance.bulletsRAtAllInt <= 0)
        {
            GameManager.Instance.bulletsRAtAllInt = 75;
        }
        if (GameManager.Instance.bulletsSAtAllInt <= 0)
        {
            GameManager.Instance.bulletsSAtAllInt = 15;
        }
    }

    IEnumerator ChangeVoid()
    {
        yield return null;
        Change = true;
        //Change animations
        if (PlayerMovement.character == "Rifler")
        {
            rAnim.SetBool("change", Change);
        }
        if (PlayerMovement.character == "Sniper")
        {
            sAnim.SetBool("change", Change);
        }
        if (PlayerMovement.character == "Sickler")
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