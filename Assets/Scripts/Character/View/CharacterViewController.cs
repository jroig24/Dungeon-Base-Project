using System.Collections.Generic;
using Gameplay.Character;
using Gameplay.Interactions;
using UnityEngine;

public class CharacterViewController : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterInteractor _characterInteractor;

    private Damageable _damageable;
    
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _characterMeshPivot;

    [SerializeField] private ParticleSystem _landParticleSystem;



    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        if(_characterMovement == null)
        {
            Debug.LogError("[CharacterViewController] CharacterMovement is null! Add a CharacterMovement component to the GameObject.");
        }
        _characterMovement.OnJumpStateChanged += JumpStateChanged;

        _characterInteractor = GetComponent<CharacterInteractor>();
        if(_characterInteractor == null)
        {
            Debug.LogError("[CharacterViewController] CharacterInteractor is null! Add a CharacterInteractor component to the GameObject.");
        }
        _characterInteractor.OnInteraction += OnInteraction;

        _damageable = GetComponent<Damageable>();
        if(_damageable == null)
        {
            Debug.LogError("[CharacterViewController] Damageable is null! Add a Damageable component to the GameObject.");
        }
        _damageable.OnDamageTaken += TakeDamage;
        _damageable.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        _animator.SetTrigger("die");
    }

    private void TakeDamage(float damage)
    {
        _animator.SetTrigger("damaged");
    }

    private void JumpStateChanged(bool isJumping)
    {
        if(!isJumping)
            _landParticleSystem.Play();
    }

    private void OnInteraction(Interactable interactable)
    {
        if(interactable != null)
        {
            _animator.SetTrigger("interact");
        }
        else
        {
            _animator.SetTrigger("emote_no");
        }
    }
    void Update()
    {
        //Rotate the character.
        _characterMeshPivot.forward = _characterMovement.LookDirection;

        //Set animator parameters.
        _animator.SetFloat("horizontalVelocity", _characterMovement.GetCurrentHorizontalVelocity());
        _animator.SetFloat("verticalVelocity", _characterMovement.GetCurrentVerticalVelocity());
        _animator.SetBool("isGrounded", _characterMovement.IsGrounded());
    }
}
