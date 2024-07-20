using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RunningState : PlayerState
    {
        float direction;
        PlayerController player;

        Vector3 gravity;
        Vector3 raycastFloorPos;

        public override void EnterState(PlayerController _player)
        {
            if (player == null) player = _player;
            // player.animator.SetFloat("Speed", 1);
        }

        public override void UpdateState()
        {

            Vector3 movement = new Vector3(player.playerInput.horizontalInput, 0.0f, player.playerInput.verticalInput).normalized;

            if (movement.magnitude >= 0.1f)
            {
                Vector3 right = Vector3.Cross(player.transform.up, Camera.main.transform.forward).normalized;
                Vector3 forward = Vector3.Cross(right, player.transform.up).normalized;

                Vector3 moveDirection = movement.x * right + movement.z * forward;

                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref direction, player.rotateSpeed * Time.deltaTime);

                player.transform.position = player.transform.position + moveDirection * player.moveSpeed * Time.deltaTime;

                //player.transform.rotation = Quaternion.Euler(player.transform.eulerAngles.x,angle, player.transform.eulerAngles.z);
                player.transform.rotation = Quaternion.LookRotation(moveDirection, player.transform.up);
                
                player.animator.SetFloat("Speed", movement.magnitude, .1f, Time.deltaTime);
            }
            else
            {
                player.TransitionToState(player.idleState);
            }
            if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.TransitionToState(player.jumpingState);
            }

            
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            // if not grounded , increase down force
            player.animator.SetBool("IsGrounded", (FloorRaycasts(0, 0, 2f) != Vector3.zero));
            if (FloorRaycasts(0, 0, 2f) == Vector3.zero)
            {
                gravity += player.gravityDirection * player.gravityForce * Time.fixedDeltaTime;
            }

        }
        
        Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
        {
            RaycastHit hit;
            // move raycast
            raycastFloorPos = player.transform.TransformPoint(0 + offsetx, 0.5f, 0 + offsetz);

            Debug.DrawRay(raycastFloorPos, player.gravityDirection * raycastLength, Color.magenta);
            if (Physics.Raycast(raycastFloorPos, player.gravityDirection, out hit, raycastLength))
            {
                return hit.point;
            }
            else return Vector3.zero;
        }
    


        public override void OnCollisionEnterState(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                player.isGrounded = true;
            }
        }
    }
}