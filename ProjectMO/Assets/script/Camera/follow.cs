using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    //{
    //public Transform target;
    //public Vector3 offset;
    //public float sensitivity = 100f;

    //private float rotationY = 0f;

    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    //void Update()
    //{
    //    float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
    //    float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

    //    rotationY += mouseX;

    //    Quaternion camRotation = Quaternion.Euler(0f, rotationY, 0f);
    //    Vector3 rotatedOffset = camRotation * offset;

    //    transform.position = target.position + rotatedOffset;
    //    transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    //}
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        transform.position = target.position + offset;

    }
}
