using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class GravityController : MonoBehaviour
{
    private Vector3 gravityDirection = Vector3.down;
    [SerializeField] Transform characterTransform;
    [SerializeField] Transform cameraTransform;

    [SerializeField] GameObject characterHolo;
    [SerializeField] Transform characterHoloPosition;

    PlayerController player;
    
    private void Start()
    {
        player = characterTransform.GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleGravityInput();
    }
    float cameraAngle;
    
    void HandleGravityInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gravityDirection = GetProjectedDirection(cameraTransform.forward);
            ShowHologram(gravityDirection);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gravityDirection = GetProjectedDirection(-cameraTransform.forward);// GetCameraFlatForward();
            ShowHologram(gravityDirection);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gravityDirection = GetProjectedDirection(-cameraTransform.right);
            ShowHologram(gravityDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gravityDirection = GetProjectedDirection(cameraTransform.right);
            ShowHologram(gravityDirection);
        }
        else if(player.playerInput.horizontalInput != 0 || player.playerInput.verticalInput != 0)
        {
            HideHologram();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HideHologram();
            player.UpdateGravityDirection(gravityDirection);
            Physics.gravity = gravityDirection * 9.81f;
            characterTransform.GetComponent<Collider>().enabled = false;
            RotateCharacterToGravityDirection();
            RotateCameraToGravityDirection();
            characterTransform.GetComponent<Collider>().enabled = true;
        }
        
    }
    //Remove camera rotation
    Vector3 GetProjectedDirection(Vector3 direction)
    {
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, cameraTransform.up);
        return projectedDirection.normalized;
    }
    
    void ShowHologram(Vector3 direction)
    {
        //set the position 
        characterHolo.transform.position = characterHoloPosition.position;// + direction.normalized;
        //set the direction
        characterHolo.transform.up = -direction;
        characterHolo.SetActive(true);
    }

    void HideHologram()
    {
        characterHolo.SetActive(false);
    }
    void RotateCharacterToGravityDirection()
    {
        // Calculate the up direction as the opposite of the gravity direction
        Vector3 upDirection = -gravityDirection;

        // Rotate the character to face the new up direction
         characterTransform.rotation = Quaternion.FromToRotation(characterTransform.up, upDirection) * characterTransform.rotation;
    }
    void RotateCameraToGravityDirection()
    {
        // Calculate the up direction as the opposite of the gravity direction
        Vector3 upDirection = -gravityDirection;

        // Rotate the camera to face the new up direction
        cameraTransform.rotation = Quaternion.FromToRotation(cameraTransform.up, upDirection) * cameraTransform.rotation;
    }
   
}
