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
        
        public float jumpSpeed;

        public float gravityForce;
        public Vector3 gravityDirection = Vector3.down; // Default gravity direction


        Vector3 gravity;
        Vector3 raycastFloorPos;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
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
            //if (!isGrounded)
            //{
            //    TransitionToState(fallingState);
            //    gravity += gravityDirection * gravityForce * Time.fixedDeltaTime;
            //    rb.velocity = gravity;
            //}else
            //    rb.velocity = Vector3.zero;
        }

        Vector3 FloorRaycasts(float offsetx, float offsetz, float raycastLength)
        {
            RaycastHit hit;
            // move raycast
            raycastFloorPos = transform.TransformPoint(0 + offsetx, 0.3f, 0 + offsetz);

            Debug.DrawRay(raycastFloorPos, gravityDirection * raycastLength, Color.magenta);
            if (Physics.Raycast(raycastFloorPos, gravityDirection, out hit, raycastLength))
            {
                return hit.point;
            }
            else return Vector3.zero;
        }

        void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnterState(collision);
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
        public abstract void OnCollisionEnterState(Collision collision);
        public virtual void FixedUpdateState() { }
    } 
}