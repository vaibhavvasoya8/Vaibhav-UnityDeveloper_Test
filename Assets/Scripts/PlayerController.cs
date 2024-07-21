using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotateSpeed = 5f;
        public float jumpForce = 7f;
        public Animator animator;
        public Rigidbody rb;
        public bool isGrounded = true;

        public readonly PlayerState idleState = new IdleState();
        public readonly PlayerState runningState = new RunningState();
        public readonly PlayerState jumpingState = new JumpingState();
        public readonly PlayerState fallingState = new FallingState();

        private PlayerState currentState;
        public PlayerInput playerInput;
        
        public Vector3 gravityDirection = Vector3.down; // Default gravity direction

        Vector3 raycastFloorPos;
        void Start()
        {
            if(rb==null) rb = GetComponent<Rigidbody>();
            if (animator==null) animator = GetComponent<Animator>();

            currentState = idleState;
            currentState.EnterState(this);
        }

        void Update()
        {
            currentState.UpdateState();
        }
        private void FixedUpdate()
        {
            currentState.FixedUpdateState();

            // if not grounded , increase down force
            isGrounded = (FloorRaycasts(0, 0, 0.5f) != Vector3.zero);
            animator.SetBool("IsGrounded", isGrounded);

            if (!isGrounded && currentState != fallingState)
            {
                TransitionToState(fallingState);
            }
        }
        Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
        {
            RaycastHit hit;
            // move raycast
            raycastFloorPos = transform.TransformPoint(0 + offsetx, 0.2f, 0 + offsetz);

            Debug.DrawRay(raycastFloorPos, gravityDirection * raycastLength, Color.magenta);
            if (Physics.Raycast(raycastFloorPos, gravityDirection, out hit, raycastLength))
            {
                return hit.point;
            }
            else return Vector3.zero;
        }

        public void TransitionToState(PlayerState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        public void UpdateGravityDirection(Vector3 newGravityDirection)
        {
            gravityDirection = newGravityDirection.normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            Collectable collectable = other.GetComponent<Collectable>();
            if(collectable != null)
            {
                collectable.Collected();
            }
        }
    }

    public abstract class PlayerState
    {
        public PlayerController player;
        public abstract void EnterState(PlayerController player);
        public abstract void UpdateState();
        public virtual void FixedUpdateState() { }
    } 
}