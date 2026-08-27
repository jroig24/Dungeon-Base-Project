using UnityEngine;

public class LaunchableReward : MonoBehaviour
{
    public float launchForce = 2.5f;

    [ContextMenu("Launch Reward")]
    public void LaunchReward()
    {
        PickupBase pickup = GetComponent<PickupBase>();
        pickup.pickupEnabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            
            var randomDirection = Random.insideUnitSphere;
            randomDirection.y = 1f;
            randomDirection.Normalize();

            rb.AddForce(randomDirection * launchForce, ForceMode.VelocityChange);
        }

        currentTimeAlive = 0f;
        enabled = true;
    }

    public float timeToEnablePickup = 2f;
    private float currentTimeAlive = 0f;
    void Update()
    {
        currentTimeAlive += Time.deltaTime;
        if(currentTimeAlive >= timeToEnablePickup)
        {
            PickupBase pickup = GetComponent<PickupBase>();
            if(pickup != null)
            {
                pickup.pickupEnabled = true;
            }

            enabled = false;
        }
    }
}
