using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody _myRigidBody;

        [Header("Movement Settings")]
        public float moveSpeed = 2f;

        [Header("Jump Settings")]
        public float jumpForce = 5f;
        public float extraFallGravity = 1f;
        private bool isJumping;
        private bool jumpHeld;



        void Awake()
        {
            _myRigidBody = GetComponent<Rigidbody>();
            if(_myRigidBody == null)
            {
                Debug.LogError("[CharacterMovement] Rigidbody is null! Add a Rigidbody component to the GameObject.");
            }
        }

        public void Move(Vector2 velocityVector)
        {
            //Calculate the desired move velocity based on the input vector and move speed
            Vector2 desiredMoveVelocity = velocityVector * moveSpeed;

            //Set the Rigidbody's velocity to the desired move velocity while preserving the current y-axis velocity (for gravity and jumping)
            _myRigidBody.linearVelocity = new Vector3(desiredMoveVelocity.x, _myRigidBody.linearVelocity.y, desiredMoveVelocity.y);
        }

        public void LookAt(Vector3 targetLookPosition)
        {
            Vector3 direction = (targetLookPosition - transform.position).normalized;

            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _myRigidBody.MoveRotation(targetRotation);//This has to be moved out. Move to character visuals.
            }
        }

        public void TriggerJump()
        {
            //Only allow jumping if the character is touching the ground.
            if(!IsGrounded())
                return;

            isJumping = true;
            jumpHeld = true;

            //Launch the character upwards.
            _myRigidBody.linearVelocity = new Vector3(_myRigidBody.linearVelocity.x, jumpForce, _myRigidBody.linearVelocity.z);
        }

        public void CancelJump()
        {
            jumpHeld = false;
        }

        void Update()
        {
            if (isJumping)
            {
                //Falling or not wanting to jump anymore
                if(_myRigidBody.linearVelocity.y < 0 || !jumpHeld)
                {
                    //Increase fall velocity. This improves the jump feel.
                    _myRigidBody.AddForce(Vector3.down * extraFallGravity);
                }

                if (IsGrounded() && _myRigidBody.linearVelocity.y <= 0f)
                    isJumping = false;
            }
        }

        bool IsGrounded()
        {
            //Slightly below the character's position. This is to avoid missing the ground when the character is just above it.
            Vector2 origin = transform.position + Vector3.up * 0.1f;
            //Setting the Ground layer is important, to avoid detecting the player's own collider.
            return Physics.Raycast(origin, Vector3.down, 0.2f, LayerMask.GetMask("Ground"));
        }
    }
}