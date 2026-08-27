using System.Collections.Generic;
using Gameplay.Character;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _footstepAudioClips;

    private void Awake()
    {
        _audioSource = GetComponentInParent<AudioSource>();

        _characterMovement = GetComponentInParent<CharacterMovement>();
        if(_characterMovement == null)
        {
            Debug.LogError("[CharacterSoundController] CharacterMovement is null! Add a CharacterMovement component to the GameObject.");
        }
        _characterMovement.OnJumpStateChanged += JumpStateChanged;
    }

    private void JumpStateChanged(bool isJumping)
    {
        PlayStepSound();
    }

    public void PlayStepSound()
    {
        int randomIndex = Random.Range(0, _footstepAudioClips.Count);
        AudioClip randomClip = _footstepAudioClips[randomIndex];
        _audioSource.PlayOneShot(randomClip);
        _audioSource.pitch = Random.Range(0.8f, 1.2f); // Randomize pitch for variety
    }
}
