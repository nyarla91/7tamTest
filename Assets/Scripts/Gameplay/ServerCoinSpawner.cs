using System.Collections.Generic;
using Extentions;
using Gameplay.Pause;
using Mirror;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ServerCoinSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private BoxCollider2D _spawnArea;
        [SerializeField] private float _spawnPeriod;

        private Timer _timer;

        private List<GameObject> _coins = new List<GameObject>();
        
        [Inject] private IPauseInfo Pause { get; set; }

        public void Restart()
        {
            for (int i = _coins.Count - 1; i >= 0; i--)
            {
                Destroy(_coins[i]);
            }
            _coins = new List<GameObject>();
        }
        
        private void Awake()
        {
            GetComponent<NetworkIdentity>().serverOnly = true;
        }
        
        private void Start()
        {
            if ( ! isServer)
                return;
            _timer = new Timer(this, _spawnPeriod, Pause, true).Start();
            _timer.Expired += Spawn;
        }

        private void Spawn()
        {
            GameObject coin = Instantiate(_prefab, _spawnArea.bounds.RandomPointInBounds2D(), Quaternion.identity);
            NetworkServer.Spawn(coin);
            _coins.Add(coin);
        }
    }
}