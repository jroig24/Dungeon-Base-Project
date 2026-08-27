using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public static Picker Instance { get; private set; }
    private List<PickupBase> _allPickups = new List<PickupBase>();
    public float PickupDistance = 2f;
    private void Awake()
    {
        Instance = this;
    }
    
    public void AddPickup(PickupBase pickup)
    {
        if (!_allPickups.Contains(pickup))
        {
            _allPickups.Add(pickup);
        }
    }

    public void RemovePickup(PickupBase pickup)
    {
        if (_allPickups.Contains(pickup))
        {
            _allPickups.Remove(pickup);
        }
    }

    void Update()
    {
        //Check for pickups in range.
        foreach (var pickup in _allPickups)
        {
            if (pickup == null) continue; // Skip if the pickup has been destroyed

            if(pickup.pickupEnabled == false) continue; // Skip if the pickup is not enabled

            float distance = Vector3.Distance(transform.position, pickup.transform.position);
            if (distance <= PickupDistance)
            {
                //Trigger pickup logic.
                pickup.PickUp();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PickupDistance);
    }
}
