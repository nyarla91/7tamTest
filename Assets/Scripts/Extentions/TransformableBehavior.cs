using System;
using UnityEngine;

namespace Extentions
{
    public class TransformableBehavior : MonoBehaviour
    {
        private Transform _transform;

        public Transform Transform => _transform ??= gameObject.transform;
        
        [Obsolete] public Transform transform => Transform;
    }
}