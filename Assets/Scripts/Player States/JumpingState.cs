using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class JumpingState : PlayerState
    {
        public override void EnterState(PlayerController _player)
        {
            if (player == null) player = _player;
            //  player.animator.SetBool("IsJumping", true);
            player.rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);
            player.isGrounded = false;
        }

        public override void UpdateState()
        {
            if (player.isGrounded)
            {
                player.TransitionToState(player.idleState);
            }
        }

        public override void OnCollisionEnterState(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                player.isGrounded = true;
                //  player.animator.SetBool("IsJumping", false);
            }
        }
    }
}