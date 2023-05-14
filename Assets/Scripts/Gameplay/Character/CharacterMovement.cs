using Extentions;
using Gameplay.Pause;
using Mirror;
using Mirror.Core;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : NetworkTransformableBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _accelerationTime;

        private Vector2 _velocity;
        private Vector2 _moveInput;

        private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody ??= GetComponent<Rigidbody2D>();
        
        [Inject] private IPauseInfo Pause { get; set; }
        [Inject] private NetworkManager NetworkManager { get; set; }

        [Command]
        public void CmdSetMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }

        private void Move(float deltaTime)
        {
            if (!isServer)
                return;
            if (Pause.IsPaused)
            {
                Rigidbody.velocity = Vector2.zero;
                return;
            }
            
            Vector2 targetVelocity = _moveInput * _maxSpeed;
            float maxDelta = _maxSpeed / _accelerationTime * deltaTime;
            _velocity = Vector2.MoveTowards(_velocity, targetVelocity, maxDelta);

            Rigidbody.velocity = _velocity;
        }

        public void Restart()
        {
            Transform.position = NetworkManager.GetStartPosition().position;
        }
    }
}