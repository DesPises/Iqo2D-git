using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeCode : MonoBehaviour
{
    public static KeyCode riflerKey, sniperKey, sicklerKey;
    public GameObject riflerGO, sniperGO, sicklerGO;
    private Transform plRPosition, plSPosition, plSiPosition;
    public SpriteRenderer riffire;
    public Vector3 plRCoordinates, plSCoordinates, plSiCoordinates;
    public static bool Change, CanChange, isChanged;

    void Start()
    {
        riflerKey = InputManager.IM.torKey;
        sniperKey = InputManager.IM.tosKey;
        sicklerKey = InputManager.IM.tosiKey;
        plMovement.character = "Rifler";
        riflerKey = KeyCode.Alpha2;
        sniperKey = KeyCode.Alpha1;
        sicklerKey = KeyCode.Alpha3;
        Change = false;
        CanChange = true;
        isChanged = false;
    }

    void Update()
    { 
        //Coordinates
        if (plMovement.character == "Rifler")
        {
            plRPosition = riflerGO.transform;
            plRCoordinates = new Vector3(plRPosition.position.x, plRPosition.position.y - 2, plRPosition.position.z);
        }
        if (plMovement.character == "Sniper")
        {
            plSPosition = sniperGO.transform;
            plSCoordinates = new Vector3(plSPosition.position.x, plSPosition.position.y - 2, plSPosition.position.z);
        }
        if (plMovement.character == "Sickler")
        {
            plSiPosition = sicklerGO.transform;
            plSiCoordinates = new Vector3(plSiPosition.position.x, plSiPosition.position.y + 2, plSiPosition.position.z);
        }


        //Characters pick

        //Pick rifler
        if (CanChange && Input.GetKeyDown(riflerKey) && plMovement.character == "Sniper" && !GameManager.rIsDead)
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            plMovement.character = "Rifler";
            riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToRifler();

        }
        if (CanChange && Input.GetKeyDown(riflerKey) && plMovement.character == "Sickler" && !GameManager.rIsDead)
        {
            riflerGO.gameObject.SetActive(true);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(false);
            plMovement.character = "Rifler";
            riflerGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToRifler();
        }
        //Pick sniper
        if (CanChange && Input.GetKeyDown(sniperKey) && plMovement.character == "Rifler" && !GameManager.sIsDead)
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            plMovement.character = "Sniper";
            sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToSniper();
        }
        if (CanChange && Input.GetKeyDown(sniperKey) && plMovement.character == "Sickler" && !GameManager.sIsDead)
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(true);
            sicklerGO.gameObject.SetActive(false);
            plMovement.character = "Sniper";
            sniperGO.transform.position = plSiCoordinates;
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToSniper();
        }
        //Pick sickler
        if (CanChange && Input.GetKeyDown(sicklerKey) && plMovement.character == "Rifler" && !GameManager.siIsDead)
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            plMovement.character = "Sickler";
            sicklerGO.transform.position = plRCoordinates;
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToSickler();
        }
        if (CanChange && Input.GetKeyDown(sicklerKey) && plMovement.character == "Sniper" && !GameManager.siIsDead)
        {
            riflerGO.gameObject.SetActive(false);
            sniperGO.gameObject.SetActive(false);
            sicklerGO.gameObject.SetActive(true);
            plMovement.character = "Sickler";
            sicklerGO.transform.position = plSCoordinates;
            StartCoroutine(ChangeVoid());
            GameManager.Instance.SwitchToSickler();
        }

        //Rifler dies

        if (plMovement.character == "Rifler" && GameManager.rIsDead)
        {
            if ((!GameManager.siIsDead && !GameManager.sIsDead) || (!GameManager.siIsDead && GameManager.sIsDead))
            {
                riflerGO.gameObject.SetActive(false);
                sniperGO.gameObject.SetActive(false);
                sicklerGO.gameObject.SetActive(true);
                plMovement.character = "Sickler";
                sicklerGO.transform.position = plRCoordinates;
                StartCoroutine(ChangeVoid());
            }
            if (GameManager.siIsDead && !GameManager.sIsDead)
            {
                riflerGO.gameObject.SetActive(false);
                sniperGO.gameObject.SetActive(true);
                sicklerGO.gameObject.SetActive(false);
                plMovement.character = "Sniper";
                sniperGO.transform.position = plRCoordinates + new Vector3(0, 2, 0);
                StartCoroutine(ChangeVoid());
            }
        }

        //Sniper dies

        if (plMovement.character == "Sniper" && GameManager.sIsDead)
        {
            if ((!GameManager.siIsDead && !GameManager.rIsDead) || (!GameManager.siIsDead && GameManager.rIsDead))
            {
                riflerGO.gameObject.SetActive(false);
                sniperGO.gameObject.SetActive(false);
                sicklerGO.gameObject.SetActive(true);
                plMovement.character = "Sickler";
                sicklerGO.transform.position = plSCoordinates;
                StartCoroutine(ChangeVoid());
            }
            if (GameManager.siIsDead && !GameManager.rIsDead)
            {
                riflerGO.gameObject.SetActive(true);
                sniperGO.gameObject.SetActive(false);
                sicklerGO.gameObject.SetActive(false);
                plMovement.character = "Rifler";
                riflerGO.transform.position = plSCoordinates + new Vector3(0, 2, 0);
                StartCoroutine(ChangeVoid());
            }
        }

        //Sickler dies

        if (plMovement.character == "Sickler" && GameManager.siIsDead)
        {
            if ((!GameManager.sIsDead && !GameManager.rIsDead) || (GameManager.sIsDead && !GameManager.rIsDead))
            {
                riflerGO.gameObject.SetActive(true);
                sniperGO.gameObject.SetActive(false);
                sicklerGO.gameObject.SetActive(false);
                plMovement.character = "Rifler";
                riflerGO.transform.position = plSiCoordinates;
                StartCoroutine(ChangeVoid());
            }
            if (!GameManager.sIsDead && GameManager.rIsDead)
            {
                riflerGO.gameObject.SetActive(false);
                sniperGO.gameObject.SetActive(true);
                sicklerGO.gameObject.SetActive(false);
                plMovement.character = "Sniper";
                sniperGO.transform.position = plSiCoordinates;
                StartCoroutine(ChangeVoid());
            }
        }
    }

    IEnumerator ChangeVoid()
    {
        yield return null;
        Change = true;
        yield return null;
        Change = false;
        CanChange = false;
        yield return new WaitForSeconds(0.25f);
        if (!STAR.isDDOn && !STAR.isInfAmmoOn && !STAR.isInfHPOn)
            CanChange = true;
    }

}
