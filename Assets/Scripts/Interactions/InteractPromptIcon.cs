using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InteractPromptIcon : MonoBehaviour
{
    Animator _animator;
    [SerializeField] MeshRenderer _meshRenderer;

    [SerializeField] Texture2D _interactKeyboard;
    [SerializeField] Texture2D _interactController;

    private void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        var usingGamepad = device is Gamepad;

        if (usingGamepad)
            _meshRenderer.material.SetTexture("_BaseMap", _interactController);
        else
            _meshRenderer.material.SetTexture("_BaseMap", _interactKeyboard);
    }
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void SetVisible(bool visible)
    {
        _animator.SetBool("showHint", visible);
    }

    void Update()
    {
        Vector3 lookatVector = Camera.main.transform.position - _meshRenderer.transform.position;
        _meshRenderer.transform.forward = -lookatVector;
    }
}
