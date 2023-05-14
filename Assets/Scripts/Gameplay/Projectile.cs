using Gameplay.Character;
using Mirror;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : Spawnable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        
        private GameObject _owner;
        
        private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();

        public void Init(GameObject owner, Vector2 direction)
        {
            _owner = owner;
            Rigidbody.velocity = direction.normalized * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (! isServer || ! other.TryGetComponent(out CharacterVitals vitals) || other.gameObject == _owner)
                return;
            Unspawn();
            vitals.TakeDamage(_damage);
        }
    }
}