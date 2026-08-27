using UnityEngine;

public class DoorController : MonoBehaviour, IActivable
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Activate(bool activate)
    {
        _animator.SetBool("isOpen", activate);
    }
}
