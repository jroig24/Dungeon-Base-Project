using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Interactions{
    public class Interactable : MonoBehaviour
    {
        //This fancy (=>) code is just so the parameter can be read from other scripts but it can't be set.
        public static List<Interactable> AllInteractables => _allInteractables;

        //An static parameter is shared between all instances of the class.
        readonly static List<Interactable> _allInteractables = new List<Interactable>();
        

        public bool IsInteractionCandidate => _isInteractionCandidate;
        private bool _isInteractionCandidate = false;

        public bool InteractionEnabled = true;

        public UnityAction<bool> OnCandidateChange;
        public UnityAction<CharacterInteractor> OnInteract;
        
        void Awake()
        {
            //When the object is created, add it to the list of interactables.
            _allInteractables.Add(this);
        }

        void OnDestroy()
        {
            //If the object gets destroyed, remove it from the list of interactables.
            _allInteractables.Remove(this);        
        }

        public void Interact(CharacterInteractor interactor)
        {
            OnInteract?.Invoke(interactor);
        }

        public void SetInteractCandidate(bool isCandidate)
        {
            _isInteractionCandidate = isCandidate;
            OnCandidateChange?.Invoke(isCandidate);
        }

    }
}
