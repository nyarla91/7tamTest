using System;
using System.Collections.Generic;
using Mirror.Core;
using Mirror.Discovery;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Connection
{
    public class ConnectionHUD : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _serverName;

        private readonly List<ServerResponse> _servers = new List<ServerResponse>();
        
        [Inject] private NetworkManager NetworkManager { get; set; }
        [Inject] private NetworkDiscovery NetworkDiscovery { get; set; }

        public void FindServers()
        {
            _servers.Clear();
            NetworkDiscovery.StartDiscovery();
        }
        
        public void HostServer()
        {
            NetworkManager.ServerName = _serverName.text;
            NetworkManager.StartHost();
            NetworkDiscovery.AdvertiseServer();
        }

        public void ConnectToServer()
        {
            ServerResponse server = _servers.Find(response => response.serverName.Equals(_serverName.text));
            NetworkManager.StartClient(server.uri);
        }

        private void Awake()
        {
            NetworkDiscovery.OnServerFound.AddListener(AddServer);
        }

        private void Update()
        {
            string log = "";
            foreach (ServerResponse response in _servers)
            {
                log += $"{response.uri} {response.serverName}";
            }
            print(log);
        }

        private void AddServer(ServerResponse response)
        {
            _servers.Add(response);
        }
    }
}