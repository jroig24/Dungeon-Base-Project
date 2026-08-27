using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PressurePlateController : ActivatorBase
{
    private int _objectsOnPlate = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PressurePlateActivator activator))
        {
            if(_objectsOnPlate == 0)
            {
                _isActivated = true;
                ActivateObjects(_isActivated);
            }
            _objectsOnPlate++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PressurePlateActivator activator))
        {
            _objectsOnPlate--;
            if(_objectsOnPlate == 0)
            {
                _isActivated = false;
                ActivateObjects(_isActivated);
            }
        }
    }
}
