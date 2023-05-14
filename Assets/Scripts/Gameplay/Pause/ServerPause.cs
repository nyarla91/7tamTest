using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace Gameplay.Pause
{
    public class ServerPause : NetworkBehaviour, IPauseInfo, IPauseChange
    {
        private readonly List<MonoBehaviour> _pauseSources = new List<MonoBehaviour>();

        public bool IsPaused => _pauseSources.Where(source => source != null).ToArray().Length > 0;
        public bool IsUnpaused => ! IsPaused;
        
        public void AddPauseSource(MonoBehaviour source) => _pauseSources.Add(source);

        public void RemovePauseSource(MonoBehaviour source)
        {
            if (_pauseSources.Contains(source))
                _pauseSources.Remove(source);
        }

        private void Awake()
        {
            GetComponent<NetworkIdentity>().serverOnly = true;
        }
    }
}