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

    public static bool change;
    public static bool canChange;
    public static bool isChanged;

    void Start()
    {
        // Set starting character to rifler
        plMovement.character = "Rifler";

        change = false;
        canChange = true;
        isChanged = false;
    }

    void Update()
    {
        // Save coordinates
        plRCoordinates = riflerGO.transform.position + new Vector3(0, -2);
        plSCoordinates = sniperGO.transform.position + new Vector3(0, -2);
        plSiCoordinates = sicklerGO.transform.position + new Vector3(0, 2);

        // Characters pick

        // Pick rifler
        if (canChange && Input.GetKeyDown(InputManager.IM.torKey) && !GameManager.instance.rIsDead)
        {
            if (plMovement.character == "Sniper")
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);

            }
            if (plMovement.character == "Sickler")
            {
                riflerGO.transform.position = plSiCoordinates;

            }
            GameManager.instance.SwitchToRifler();
            PickRifler();
        }

        // Pick sniper
        if (canChange && Input.GetKeyDown(InputManager.IM.tosKey) && !GameManager.instance.sIsDead)
        {
            if (plMovement.character == "Rifler")
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            }
            if (plMovement.character == "Sickler")
            {
                sniperGO.transform.position = plSiCoordinates;
            }
            GameManager.instance.SwitchToSniper();
            PickSniper();
        }

        // Pick sickler
        if (canChange && Input.GetKeyDown(InputManager.IM.tosiKey) && !GameManager.instance.siIsDead)
        {
            if (plMovement.character == "Rifler")
            {
                sicklerGO.transform.position = plRCoordinates;
            }
            if (plMovement.character == "Sniper")
            {
                sicklerGO.transform.position = plSCoordinates;
            }
            GameManager.instance.SwitchToSickler();
            PickSickler();
        }

        // Rifler dies

        if (plMovement.character == "Rifler" && GameManager.instance.rIsDead)
        {
            if (!GameManager.instance.siIsDead)
            {
                sicklerGO.transform.position = plRCoordinates;
                PickSickler();
            }
            if (GameManager.instance.siIsDead && !GameManager.instance.sIsDead)
            {
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                PickSniper();
            }
        }

        // Sniper dies

        if (plMovement.character == "Sniper" && GameManager.instance.sIsDead)
        {
            if (!GameManager.instance.siIsDead)
            {
                sicklerGO.transform.position = plSCoordinates;
                PickSickler();
            }
            if (GameManager.instance.siIsDead && !GameManager.instance.rIsDead)
            {
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
                PickRifler();
            }
        }

        // Sickler dies

        if (plMovement.character == "Sickler" && GameManager.instance.siIsDead)
        {
            if (!GameManager.instance.sIsDead)
            {
                riflerGO.transform.position = plSiCoordinates;
                PickSniper();
            }
            if (!GameManager.instance.sIsDead && GameManager.instance.rIsDead)
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
        plMovement.character = "Rifler";
        StartCoroutine(ChangeCoroutine());
    }

    private void PickSniper()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(true);
        sicklerGO.gameObject.SetActive(false);
        plMovement.character = "Sniper";
        StartCoroutine(ChangeCoroutine());
    }

    private void PickSickler()
    {
        riflerGO.gameObject.SetActive(false);
        sniperGO.gameObject.SetActive(false);
        sicklerGO.gameObject.SetActive(true);
        plMovement.character = "Sickler";
        StartCoroutine(ChangeCoroutine());
    }

    IEnumerator ChangeCoroutine()
    {
        yield return null;
        change = true;
        yield return null;
        change = false;
        canChange = false;
        yield return new WaitForSeconds(0.25f);
        if (!STAR.isDDOn && !STAR.isInfAmmoOn && !STAR.isInfHPOn)
        {
            canChange = true;
        }
    }

}
