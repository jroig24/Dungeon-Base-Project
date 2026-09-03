using Gameplay.Character;
using Gameplay.Interactions;
using Gameplay.Systems;
using UnityEngine;


public class DeathBehaviour : MonoBehaviour
{
    Damageable _damageable;

    void Awake()
    {
        _damageable = GetComponent<Damageable>();
        if(_damageable == null)
        {
            Debug.LogError("[DeathBehaviour] Damageable is null! Add a Damageable component to the GameObject.");
        }
        _damageable.OnDeath += Die;
    }

    private void Die()
    {
        // Disable the character's movement and interaction components
        CharacterMovement movement = GetComponent<CharacterMovement>();
        if (movement != null)
        {
            movement.SetMovementEnabled(false);
        }

        CharacterInteractor interactor = GetComponent<CharacterInteractor>();
        if (interactor != null)
        {
            interactor.SetInteractionEnabled(false);
        }
    }
}
