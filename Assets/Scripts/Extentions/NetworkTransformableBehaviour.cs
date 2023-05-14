using System;
using Mirror;
using UnityEngine;

namespace Extentions
{
    public class NetworkTransformableBehaviour : NetworkBehaviour
    {
        private Transform _transform;

        public Transform Transform => _transform ??= gameObject.transform;
        
        [Obsolete] public Transform transform => Transform;
    }
}