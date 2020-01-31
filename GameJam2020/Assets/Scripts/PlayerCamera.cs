using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity = 4;
    public Transform target;
    public float dstFromTarget = 7;
    public Vector2 pitchMinMax = new Vector2 (-10, 40);

    public float rotationSmoothTime = 1.2f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    void Update() {
        yaw += Input.GetAxis ("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        Vector3 targetRotation = new Vector3 (pitch, yaw);
        transform.eulerAngles = targetRotation;

        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
