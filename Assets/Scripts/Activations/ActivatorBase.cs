using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActivatorBase : MonoBehaviour
{
    //The bool will be true when the activator gets activated, and false when it gets deactivated.
    [HideInInspector] public UnityEvent<bool> ToActivate;
    public List<GameObject> ObjectsToActivate = new List<GameObject>();
    public bool IsActivated => _isActivated;
    protected bool _isActivated = false;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var obj in ObjectsToActivate)
        {
            if(obj != null)
                Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }

    protected void ActivateObjects(bool activate)
    {
        ToActivate?.Invoke(activate);
        foreach (var obj in ObjectsToActivate)
        {
            IActivable activable = obj.GetComponent<IActivable>();
            if(activable != null)
            {
                activable.Activate(activate);
            }
        }
    }

}
