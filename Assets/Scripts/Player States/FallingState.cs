using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class FallingState : PlayerState
    {
        Vector3 gravity;
        Vector3 raycastFloorPos;
        public override void EnterState(PlayerController _player)
        {
            if (player == null) player = _player;
            if (FloorRaycasts(0, 0, 20f) == Vector3.zero)
            {
                UIController.inst.ShowGameOver();
            }
        }
        public override void UpdateState()
        {
            if (player.isGrounded)
                player.TransitionToState(player.idleState);
        }

        public override void OnCollisionEnterState(Collision collision)
        {
        }

        Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
        {
            RaycastHit hit;
            // move raycast
            raycastFloorPos = player.transform.TransformPoint(0 + offsetx, 0.3f, 0 + offsetz);

            Debug.DrawRay(raycastFloorPos, player.gravityDirection * raycastLength, Color.magenta);
            if (Physics.Raycast(raycastFloorPos, player.gravityDirection, out hit, raycastLength))
            {
                return hit.point;
            }
            else return Vector3.zero;
        }


    }
}