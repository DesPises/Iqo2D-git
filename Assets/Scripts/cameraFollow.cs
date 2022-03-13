using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerRifler;
    [SerializeField] private Transform playerSniper;
    [SerializeField] private Transform playerSickler;

    private readonly float movingSpeed = 3f;
    private Camera cam;
    private Vector3 target;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        // Set target point and FOV depending of character

        if (PlayerMovement.character == "Rifler" && playerRifler)
        {
            cam.orthographicSize = 6;
            target = new Vector3(playerRifler.position.x, 1, -10);
        }

        else if (PlayerMovement.character == "Sniper" && playerSniper)
        {
            cam.orthographicSize = 6.8f;
            if (PlayerMovement.isMovingFW)
            {
                target = new Vector3(playerSniper.position.x + 1, 1.5f, -10);
            }
            else
            {
                target = new Vector3(playerSniper.position.x - 1, 1.5f, -10);
            }
        }

        else if (PlayerMovement.character == "Sickler" && playerSickler)
        {
            cam.orthographicSize = 5.2f;
            target = new Vector3(playerSickler.position.x, 0, -10);
        }

        // Camera following player
        transform.position = Vector3.Lerp(transform.position, target, movingSpeed * Time.deltaTime);
    }
}
