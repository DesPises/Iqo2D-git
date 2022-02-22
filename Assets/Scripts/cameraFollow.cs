using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform playerTransform, playerSniperTransform, playerSicklerTransform;
    [SerializeField] private string PlayerTag;
    [SerializeField] private float movingSpeed;
    public static Camera cam;


    void Awake()
    {
        cam = GetComponent<Camera>();

        if (this.playerTransform == null)
        {
            if (this.PlayerTag == "")
            {
                this.PlayerTag = "Player";
            }
            this.playerTransform = GameObject.FindGameObjectWithTag(this.PlayerTag).transform;
        }
        this.transform.position = new Vector3()
        {
            x = this.playerTransform.position.x,
            y = 1,
            z = this.playerTransform.position.z - 10,
        };
    }
    void Update()
    {

        if (plMovement.character == "Rifler")
        {
            cam.orthographicSize = 6;
        }
        if (plMovement.character == "Sniper")
        {
            cam.orthographicSize = 6.8f;
        }
        if (plMovement.character == "Sickler")
        {
            cam.orthographicSize = 5.2f;
        }
    }

    void FixedUpdate()
    {
        if (plMovement.character == "Rifler")
        {
            if (this.playerTransform)
            {
                Vector3 target = new Vector3()
                {
                    x = this.playerTransform.position.x,
                    y = 1,
                    z = this.playerTransform.position.z - 10,
                };
                Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);
                this.transform.position = pos;
            }
        }
        if (plMovement.character == "Sniper")
        {
            if (this.playerSniperTransform)
            {
                if (plMovement.isMovingFW)
                {
                    Vector3 target = new Vector3()
                    {
                        x = this.playerSniperTransform.position.x + 1,
                        y = 1.5f,
                        z = this.playerSniperTransform.position.z - 10,
                    };
                    Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);
                    this.transform.position = pos;
                }
                if (!plMovement.isMovingFW)
                {
                    Vector3 target = new Vector3()
                    {
                        x = this.playerSniperTransform.position.x - 1,
                        y = 1.5f,
                        z = this.playerSniperTransform.position.z - 10,
                    };
                    Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);
                    this.transform.position = pos;
                }

            }
        }
        if (plMovement.character == "Sickler")
        {
            if (this.playerSicklerTransform)
            {
                Vector3 target = new Vector3()
                {
                    x = this.playerSicklerTransform.position.x,
                    y = 0f,
                    z = this.playerSicklerTransform.position.z - 10,
                };
                Vector3 pos = Vector3.Lerp(this.transform.position, target, this.movingSpeed * Time.deltaTime);
                this.transform.position = pos;
            }
        }
    }
}
