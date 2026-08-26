using Gameplay.Interactions;
using UnityEngine;

public class LeverController : ActivatorBase
{
    private Interactable _interactable;
    private Animator _animator;
    void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.OnInteract += Activate;

        _animator = GetComponent<Animator>();
    }

    //This script could be better. Right now, this function does two different things, and it breaks the single responsibility principle.
    //TODO: Create a visual controller script that listents to the ToActivate event and triggers the animation when needed.
    private void Activate(CharacterInteractor interactor)
    {
        _isActivated = !_isActivated; //logic

        _animator.SetBool("isActive", _isActivated); //visual stuff

        ToActivate.Invoke(_isActivated); //logic.
    }

}
