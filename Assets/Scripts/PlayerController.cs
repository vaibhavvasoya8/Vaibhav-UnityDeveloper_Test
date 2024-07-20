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

        private PlayerState currentState;
        public PlayerInput playerInput;
        
        public float jumpSpeed;

        public float gravityForce;
        public Vector3 gravityDirection = Vector3.down; // Default gravity direction

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