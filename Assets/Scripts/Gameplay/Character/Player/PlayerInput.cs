using System;
using Joystick;
using UnityEngine;

namespace Gameplay.Character.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private OnScreenJoystick _movementJoystick;
        [SerializeField] private OnScreenJoystick _fireJoystick;

        public Vector2 LookDirection => Fire.Equals(Vector2.zero) ? Movement : Fire;
        public Vector2 Movement => _movementJoystick.Offset;
        public Vector2 Fire => _fireJoystick.Offset;
        
    }
}