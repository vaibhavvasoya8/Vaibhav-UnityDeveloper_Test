using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] Transform player; // Player transform
    [SerializeField] float distanceBehind = 5.0f; // Distance behind the player
    [SerializeField] float height = 2.0f; // Height above the player
    [SerializeField] float smoothSpeed = 0.125f; // Smoothing speed for camera movement
    [SerializeField] float rotationSpeed = 0.125f; // Smoothing speed for camera rotation

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, height, -distanceBehind);
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Calculate the desired position based on the player's rotation and offset
        Vector3 desiredPosition = player.position + player.rotation * offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Calculate the desired rotation to keep the camera behind the player
        Quaternion desiredRotation = Quaternion.LookRotation(player.position - transform.position, player.up);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed);
    }


}
