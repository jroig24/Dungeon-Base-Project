using UnityEngine;
using UnityEngine.Events;

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
 
        public Vector3 LookDirection => _lookDirection;
        private Vector3 _lookDirection = Vector3.forward;

        //true -> Jump started. | false -> Jump ended.
        public UnityAction<bool> OnJumpStateChanged;

        private bool _movementEnabled = true;

        void Awake()
        {
            _myRigidBody = GetComponent<Rigidbody>();
            if(_myRigidBody == null)
            {
                Debug.LogError("[CharacterMovement] Rigidbody is null! Add a Rigidbody component to the GameObject.");
            }
        }

        public void SetMovementEnabled(bool enabled)
        {
            _movementEnabled = enabled;
        }

        public void Move(Vector2 velocityVector)
        {
            if (!_movementEnabled)
            {
                velocityVector = Vector2.zero;
            }

            //Calculate the desired move velocity based on the input vector and move speed
            Vector2 desiredMoveVelocity = velocityVector * moveSpeed;

            //Set the Rigidbody's velocity to the desired move velocity while preserving the current y-axis velocity (for gravity and jumping)
            _myRigidBody.linearVelocity = new Vector3(desiredMoveVelocity.x, _myRigidBody.linearVelocity.y, desiredMoveVelocity.y);
        }

        public void LookAt(Vector3 targetLookPosition)
        {
            if (!_movementEnabled)
            {
                return;
            }

            Vector3 tmpLookDirection = (targetLookPosition - transform.position);

            //Don't store the new look direction if its too small. I could cause issues if the character is trying to look inside itself.
            if(tmpLookDirection.magnitude < 0.1f)
                return;

            //We only store the look direction here. We don't need it for the movement. The visual controller and other scripts will use this vector as needed.
            _lookDirection = tmpLookDirection.normalized;
        }

        public void TriggerJump()
        {
            //Only allow jumping if the character is touching the ground.
            if(!IsGrounded())
                return;

            OnJumpStateChanged?.Invoke(true);
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

                if (IsGrounded() && _myRigidBody.linearVelocity.y <= 0f){
                    isJumping = false;
                    OnJumpStateChanged?.Invoke(false);
                }
            }
        }

        public bool IsGrounded()
        {
            //Slightly below the character's position. This is to avoid missing the ground when the character is just above it.
            Vector3 origin = transform.position + Vector3.up * 0.1f; 
            
            //Cast a ray downwards to check if the ground is close enough.
            return Physics.Raycast(origin, Vector3.down, 0.2f, LayerMask.GetMask("Ground")); //Setting the Ground layer is important, to avoid detecting the player's own collider.
        }

        //Getters
        public float GetCurrentHorizontalVelocity()
        {
            var horizontalVelocity = new Vector3(_myRigidBody.linearVelocity.x, 0f, _myRigidBody.linearVelocity.z);
            return horizontalVelocity.magnitude;
        }
        public float GetCurrentVerticalVelocity()
        {
            return _myRigidBody.linearVelocity.y;
        }
    }
}