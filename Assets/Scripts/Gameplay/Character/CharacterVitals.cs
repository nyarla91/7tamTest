using System;
using Mirror;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterVitals : NetworkBehaviour
    {
        [SerializeField] private float _maxHealth;

        [SyncVar] private float _health;

        public event Action Dead; 
        
        public float Percent => _health / _maxHealth;
        
        private void Start()
        {
            if ( ! isServer)
                return;
            _health = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if ( ! isServer || damage <= 0)
                return;
            _health -= damage;
            if (_health <= 0)
                Die();
        }

        private void Die()
        {
            if ( ! isServer)
                return;
            Dead?.Invoke();
            gameObject.SetActive(false);
        }
    }
}