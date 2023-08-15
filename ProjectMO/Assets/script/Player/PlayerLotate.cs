using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLotate : MonoBehaviour
{
    Camera _camera;

    public float speed = 5f;
    
    public bool toggleCameraRotation;

    public float smoothness = 10f;

    private Quaternion playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
    }
    void LateUpdate()
    {
        if(toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            playerRotation = Quaternion.LookRotation(playerRotate);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, Time.deltaTime * smoothness);
        }
    }
}
