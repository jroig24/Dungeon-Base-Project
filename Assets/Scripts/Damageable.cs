using UnityEngine;
using UnityEngine.Events;

namespace  Gameplay.Systems
{
    public class Damageable : MonoBehaviour
    {
        public float CurrentHitpoints => _currentHitpoints;
        [SerializeField] float _currentHitpoints;

        public float MaxHitpoints => _maxHitpoints;
        [SerializeField] float _maxHitpoints = 100f;

        bool IsDead => _isDead;
        bool _isDead = false;

        public bool canTakeDamage = true;
        public bool inmortal = false;

        //Events
        public UnityAction OnDeath;
        public UnityAction<float> OnDamageTaken;

        void Start()
        {
            _currentHitpoints = _maxHitpoints;
        }

        public void TakeDamage(float damage)
        {
            if(_isDead || !canTakeDamage) return;

            _currentHitpoints -= damage;
            if (_currentHitpoints <= 0 && !inmortal)
            {
                Die();
            }

            OnDamageTaken?.Invoke(damage);

            _currentHitpoints = Mathf.Clamp(_currentHitpoints, 0, _maxHitpoints); //If the Damageable heals, limit the values between 0 and maxHitpoints.
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} has died.");
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}