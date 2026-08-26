using UnityEngine;
using UnityEngine.Events;

public abstract class ActivatorBase : MonoBehaviour
{
    //The bool will be true when the activator gets activated, and false when it gets deactivated.
    public UnityEvent<bool> ToActivate;
    public bool IsActivated => _isActivated;
    protected bool _isActivated = false;


}
