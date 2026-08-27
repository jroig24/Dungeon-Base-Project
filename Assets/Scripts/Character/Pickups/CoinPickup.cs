using UnityEngine;

public class CoinPickup : PickupBase
{
    protected override void PickupEffect()
    {
        Debug.Log("Coin picked up!");
    }
}
