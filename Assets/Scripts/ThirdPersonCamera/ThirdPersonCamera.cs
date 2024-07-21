using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform target; // The player
    [SerializeField] float distance = 5.0f; // Distance from the player
    [SerializeField] float height = 2.0f; // Height above the player
    [SerializeField] float smoothSpeed = 0.125f; // Smooth speed for camera movement

    private Vector3 offset;
    private Quaternion targetRotation;

    [SerializeField] GameObject objCameraChangeHint;


    void Start()
    {
        objCameraChangeHint.SetActive(true);
        targetRotation = transform.rotation;
        offset = new Vector3(0, height, -distance);
        Invoke("HideCameraHint", 5);
    }

    void LateUpdate()
    {
        FollowPlayer();
        if(Input.GetMouseButtonDown(0))
        {
            offset = new Vector3(0, height, -offset.z);
        }
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

    void HideCameraHint()
    {
        objCameraChangeHint.SetActive(false);
    }
}
