using Extentions;
using UnityEngine;

namespace Joystick
{
    public class OnScreenJoystick : RectTransformableBehavior
    {
        [SerializeField] private bool _emulateFromKeyboard;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _edgePoint;
        [SerializeField] private RectTransform _thumbstick;
        [SerializeField] [Range(0, 1)] private float _innerDeadZone;
        [SerializeField] [Range(0, 1)] private float _outerDeadZone;

        private float _radius;
        private Vector2 _offset;

        private Vector2 KeyboardOffset => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        public Vector2 Offset => (_offset.magnitude == 0 && _emulateFromKeyboard) ? KeyboardOffset : _offset;

        public void Show(Vector2 screenPoint)
        {
            _canvasGroup.alpha = 1;
            RectTransform.position = screenPoint;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _thumbstick.anchoredPosition = Vector2.zero;
        }

        public void MoveStick(Vector2 screenPoint)
        {
            _thumbstick.position = screenPoint;
        }

        private void Awake()
        {
            _radius = _edgePoint.localPosition.magnitude;
        }

        private void Update()
        {
            _offset = _thumbstick.anchoredPosition / _radius;
            float offsetMagnitude = _offset.magnitude;
            if (offsetMagnitude < _innerDeadZone)
            {
                _offset = Vector2.zero;
            }
            else if (offsetMagnitude > _outerDeadZone)
            {
                _offset.Normalize();
                if (offsetMagnitude > 1)
                {
                    _thumbstick.anchoredPosition = _offset.normalized * _radius;
                }
            }
        }

        private void OnValidate()
        {
            if (_outerDeadZone < _innerDeadZone)
                _outerDeadZone = _innerDeadZone;
        }
    }
}