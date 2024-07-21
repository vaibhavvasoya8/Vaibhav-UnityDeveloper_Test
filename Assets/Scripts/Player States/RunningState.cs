using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class RunningState : PlayerState
    {
        float direction;

        public override void EnterState(PlayerController _player)
        {
            if (player == null) player = _player;
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
    }
}