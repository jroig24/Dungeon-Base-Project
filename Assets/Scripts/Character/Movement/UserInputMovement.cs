using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Character{
    public class UserInputMovement : MonoBehaviour
    {
        private CharacterMovement _characterMovement;
        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            if(_characterMovement == null)
            {
                Debug.LogError("[UserInputMovement] CharacterMovement is null! Add a CharacterMovement component to the GameObject.");
            }
        }
        void Update()
        {
            Vector2 inputMovementVector = InputSystem.actions["Move"].ReadValue<Vector2>();
            _characterMovement.Move(inputMovementVector);

            Vector3 lookVector = new Vector3(inputMovementVector.x, 0, inputMovementVector.y);
            _characterMovement.LookAt(lookVector + transform.position);

            if (InputSystem.actions["Jump"].WasPressedThisFrame())
            {
                _characterMovement.TriggerJump();
            }

            if (InputSystem.actions["Jump"].WasReleasedThisFrame())
            {
                _characterMovement.CancelJump();
            }
        }
    }
}