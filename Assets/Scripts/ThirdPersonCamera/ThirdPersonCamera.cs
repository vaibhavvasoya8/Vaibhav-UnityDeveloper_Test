using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The player
    public float distance = 5.0f; // Distance from the player
    public float height = 2.0f; // Height above the player
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement

    private Vector3 offset;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void LateUpdate()
    {
        offset = new Vector3(0, height, -distance);
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Calculate the desired position based on the player's orientation
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;

        // Smoothly interpolate the camera rotation
        targetRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
    }
    //public Transform target;
    //public Vector3 offset;
    //public float smoothSpeed = 0.125f;

    //void LateUpdate()
    //{
    //    // Calculate the new camera position based on the player's up direction
    //    Vector3 desiredPosition = target.position + offset;

    //    // Smoothly interpolate to the new position
    //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    //    transform.position = smoothedPosition;
    //}

}
