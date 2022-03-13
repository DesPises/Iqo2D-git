using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeCode : MonoBehaviour
{
    [SerializeField] private GameObject riflerGO;
    [SerializeField] private GameObject sniperGO;
    [SerializeField] private GameObject sicklerGO;

    private Vector3 plRCoordinates;
    private Vector3 plSCoordinates;
    private Vector3 plSiCoordinates;

    public static bool canChange;
    public static bool change;

    void Start()
    {
        // Set starting character to rifler
        PlayerMovement.character = "Rifler";

        change = false;
        canChange = true;
    }

    void Update()
    {
        // Save coordinates
        plRCoordinates = riflerGO.transform.position + new Vector3(0, -2);
        plSCoordinates = sniperGO.transform.position + new Vector3(0, -2);
        plSiCoordinates = sicklerGO.transform.position + new Vector3(0, 2);

        // Characters pick

        // Pick rifler
        if (canChange && Input.GetKeyDown(InputManager.IM.torKey) && !GameManager.Instance.rIsDead)
        {
            if (PlayerMovement.character == "Sniper")
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);

            }
            if (PlayerMovement.character == "Sickler")
            {
                riflerGO.transform.position = plSiCoordinates;

            }
            GameManager.Instance.SwitchToRifler();
            PickRifler();
        }

        // Pick sniper
        if (canChange && Input.GetKeyDown(InputManager.IM.tosKey) && !GameManager.Instance.sIsDead)
        {
            if (PlayerMovement.character == "Rifler")
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            }
            if (PlayerMovement.character == "Sickler")
            {
                sniperGO.transform.position = plSiCoordinates;
            }
            GameManager.Instance.SwitchToSniper();
            PickSniper();
        }

        // Pick sickler
        if (canChange && Input.GetKeyDown(InputManager.IM.tosiKey) && !GameManager.Instance.siIsDead)
        {
            if (PlayerMovement.character == "Rifler")
            {
                sicklerGO.transform.position = plRCoordinates;
            }
            if (PlayerMovement.character == "Sniper")
            {
                sicklerGO.transform.position = plSCoordinates;
            }
            GameManager.Instance.SwitchToSickler();
            PickSickler();
        }

        // Rifler dies

        if (PlayerMovement.character == "Rifler" && GameManager.Instance.rIsDead)
        {
            if (!GameManager.Instance.siIsDead)
            {
                sicklerGO.transform.position = plRCoordinates;
                PickSickler();
            }
            if (GameManager.Instance.siIsDead && !GameManager.Instance.sIsDead)
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                PickSniper();
            }
        }

        // Sniper dies

        if (PlayerMovement.character == "Sniper" && GameManager.Instance.sIsDead)
        {
            if (!GameManager.Instance.siIsDead)
            {
                sicklerGO.transform.position = plSCoordinates;
                PickSickler();
            }
            if (GameManager.Instance.siIsDead && !GameManager.Instance.rIsDead)
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
                PickRifler();
            }
        }

        // Sickler dies

        if (PlayerMovement.character == "Sickler" && GameManager.Instance.siIsDead)
        {
            if (!GameManager.Instance.sIsDead)
            {
                riflerGO.transform.position = plSiCoordinates;
                PickSniper();
            }
            if (!GameManager.Instance.sIsDead && GameManager.Instance.rIsDead)
            {
                sniperGO.transform.position = plSiCoordinates;
                PickRifler();
            }
        }
    }

    private void PickRifler()
    {
        riflerGO.gameObject.SetActive(true);
        sniperGO.gameObject.SetActive(false);
        sicklerGO.gameObject.SetActive(false);
        PlayerMovement.character = "Rifler";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSniper()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(true);
        sicklerGO.gameObject.SetActive(false);
        PlayerMovement.character = "Sniper";
        StartCoroutine(CooldownCoroutine());
    }

    private void PickSickler()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(false);
        sicklerGO.gameObject.SetActive(true);
        PlayerMovement.character = "Sickler";
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        yield return null;
        change = true;
        yield return null;
        change = false;
        canChange = false;
        yield return new WaitForSeconds(0.25f);
        if (!Star.isDDOn && !Star.isInfAmmoOn && !Star.isInfHPOn)
        {
            canChange = true;
        }
    }

}
