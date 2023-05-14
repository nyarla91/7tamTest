using Extentions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick
{
    public class OnScreenJoystickArea : RectTransformableBehavior, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private OnScreenJoystick _tagetJoystick;

        public void OnPointerDown(PointerEventData eventData) => _tagetJoystick.Show(eventData.position);
        
        public void OnDrag(PointerEventData eventData) => _tagetJoystick.MoveStick(eventData.position);
        
        public void OnEndDrag(PointerEventData eventData) => _tagetJoystick.Hide();
    }
}