using UnityEngine;

namespace Extentions
{
    public class RectTransformableBehavior : MonoBehaviour
    {
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
    }
}