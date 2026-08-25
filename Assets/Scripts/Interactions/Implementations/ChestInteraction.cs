using UnityEngine;

namespace Gameplay.Interactions{
    public class ChestInteraction : MonoBehaviour
    {
        private Interactable _interactable;
        private Animator _animator;

        void Awake()
        {
            _interactable = GetComponent<Interactable>();
            _interactable.OnInteract += Interact;

            _animator = GetComponent<Animator>();
        }

        private void Interact(CharacterInteractor interactor)
        {
            _animator.SetTrigger("Open");
            
            _interactable.InteractionEnabled = false;
        }
    }
}
