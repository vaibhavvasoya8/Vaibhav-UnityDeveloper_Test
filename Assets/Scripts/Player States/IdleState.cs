using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class IdleState : PlayerState
    {
        public override void EnterState(PlayerController _player)
        {
            if (player == null) player = _player;
            player.animator.SetFloat("Speed", 0);
        }

        public override void UpdateState()
        {
            if (player.playerInput.horizontalInput != 0 || player.playerInput.verticalInput != 0)
            {
                player.TransitionToState(player.runningState);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.TransitionToState(player.jumpingState);
            }
        }

        public override void OnCollisionEnterState(Collision collision)
        {
            //if (collision.gameObject.CompareTag("Ground"))
            //{
            //    player.isGrounded = true;
            //}
        }

    }
}
