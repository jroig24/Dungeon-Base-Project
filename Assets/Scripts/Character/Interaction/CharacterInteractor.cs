using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Interactions{
    public class CharacterInteractor : MonoBehaviour
    {
        private Interactable closestInteractable;
        public float interactDistance = 3f;

        void Update()
        {
            UpdateInteractablesByDistance();

            if (InputSystem.actions["Interact"].WasPressedThisFrame() && closestInteractable != null)
            {
                closestInteractable.Interact(this);
            }
        }

        //This function will find the closest interactable, and let it know that the player can interact with it.
        private void UpdateInteractablesByDistance()
        {
            //If there are not interactables in the scene, there is no need to do anything.
            if(Interactable.AllInteractables == null)
                return;

            float closestDistance = Mathf.Infinity;
            Interactable newClosestInteractable = null;
            foreach (var interactable in Interactable.AllInteractables)
            {
                //Ignore interactables that are not interactable at the moment.
                if(!interactable.InteractionEnabled)
                    continue;
                
                //Check if the interactable is closer than the current closest one.
                float checkDistance =  Vector3.Distance(transform.position, interactable.transform.position);
                if(checkDistance < closestDistance && checkDistance < interactDistance)
                {
                    closestDistance = checkDistance;
                    newClosestInteractable = interactable;
                }
            }

            //Update the closest interactable.
            SetClosestInteractable(newClosestInteractable);
        }

        private void SetClosestInteractable(Interactable _interactable)
        {
            //If its the same, there is no need to trigger anything.
            if(_interactable == closestInteractable)
                return;

            closestInteractable?.SetInteractCandidate(false);

            _interactable?.SetInteractCandidate(true);

            closestInteractable = _interactable;
        }
    }
}
