using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float minYAngle = -80f;
    public float maxYAngle = 80f;

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();
        RotateCamera();
        MoveCamera();
    }

    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        currentX += mouseX;
        currentY -= mouseY;

        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    void RotateCamera()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
        transform.rotation = rotation;
    }

    void MoveCamera()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
        Vector3 targetPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }
}
