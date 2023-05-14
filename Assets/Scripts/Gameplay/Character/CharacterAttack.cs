using Extentions;
using Gameplay.Pause;
using Mirror;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterAttack : NetworkTransformableBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _attackPeriod;

        private Vector2 _fireInput;
        private Timer _attackCooldown;
        
        [Inject] private IPauseInfo Pause { get; set; }
        
        private void Start()
        {
            if (isServer)
                _attackCooldown = new Timer(this, _attackPeriod, Pause);
        }

        [Command]
        public void CmdSetFireVector(Vector2 fireinput)
        {
            _fireInput = fireinput;
        }

        private void FixedUpdate()
        {
            TryFire();
        }
        
        private void TryFire()
        {
            if (!isServer || Pause.IsPaused || _fireInput.magnitude == 0 || _attackCooldown.IsOn)
                return;

            _attackCooldown.Restart();
            
            Projectile projectile = SpawnProjectile();
            projectile.Init(gameObject, _fireInput);
        }

        private Projectile SpawnProjectile()
        {
            if (!isServer || Pause.IsPaused)
                return null;
            Projectile projectile = Instantiate(_projectilePrefab, Transform.position, Quaternion.identity)
                .GetComponent<Projectile>();
            NetworkServer.Spawn(projectile.gameObject, gameObject);
            return projectile;
        }
    }
}