using Gameplay.Character;
using UnityEngine;

public class CharacterViewController : MonoBehaviour
{
    private CharacterMovement _characterMovement;

    [SerializeField] private Transform _characterMeshPivot;

    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        if(_characterMovement == null)
        {
            Debug.LogError("[CharacterViewController] CharacterMovement is null! Add a CharacterMovement component to the GameObject.");
        }
    }

    void Update()
    {
        //TODO: Use animations.
        _characterMeshPivot.forward = Vector3.Lerp(_characterMeshPivot.forward, _characterMovement.LookDirection, Time.deltaTime * 20f);
    }
}
