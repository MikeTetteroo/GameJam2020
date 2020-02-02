using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity = 4;
    public Transform target;
    public float defaultDistanceFromTarget = 7;
    public Vector2 pitchMinMax = new Vector2(-10, 40);
    public bool shouldCollideWithTerrain = true;
    public float rotationSmoothTime = 1.2f;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;
    private float distanceFromTarget;
    private float yaw;
    private float pitch;

    void Start()
    {
        distanceFromTarget = defaultDistanceFromTarget;
    }

    void FixedUpdate()
    {
        // Get the yaw and pitch 
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // Get the proper rotation
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        Vector3 targetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles = targetRotation;

        // Terrain detection
        Vector3 terrainCollisionVector = (transform.position - target.position) * defaultDistanceFromTarget;
        RaycastHit hit;
        Debug.DrawLine(transform.position, target.position);
        if (shouldCollideWithTerrain && Physics.Raycast(target.position, terrainCollisionVector, out hit) && hit.distance < defaultDistanceFromTarget && hit.collider.tag != "Player")
        {
            
            distanceFromTarget = hit.distance;
        }
        else
        {
            distanceFromTarget = defaultDistanceFromTarget;
        }

        // Put everything in the right place
        transform.position = target.position - transform.forward * distanceFromTarget;
        target.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
