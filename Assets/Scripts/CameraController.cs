using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private const float Y_ANGLE_MIN = 10.0f;
    private const float Y_ANGLE_MAX = 45.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 20.0f;

    private float currentX = 0.0f;
    private float currentY = 20.0f;

    public bool canMove { get; set; }

    private void Start()
    {
        camTransform = transform;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            currentX += Input.GetAxis("Horizontal");
            currentY += Input.GetAxis("Vertical"); ;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
    }

    private void LateUpdate()
    {
        if (canMove)
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransform.position = lookAt.position + rotation * dir;
            camTransform.LookAt(lookAt.position);
        }
    }
}
