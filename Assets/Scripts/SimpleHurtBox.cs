using UnityEngine;

namespace Gameplay.Systems{
    public class SimpleHurtBox : MonoBehaviour
    {
        public float DamageAmount = 10f;
        void OnTriggerEnter(Collider other)
        {
            Damageable damageable = other.GetComponent<Damageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(DamageAmount);
            }
        }
    }
}
