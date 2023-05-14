using Extentions;
using Mirror;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    public class CharacterAppearance : NetworkBehaviour
    {
        [SerializeField] private Transform _bow;
        [SerializeField] private SpriteRenderer _skinRenderer;
        [SerializeField] private TMP_Text _nameText;
        
        [SyncVar] private string _name;
        [SyncVar] private int _skin;

        private Vector2 _lookDirection;
        
        public string Name => _name;
        
        [Inject] private SkinsLibary _skinsLibary;
        
        [Command]
        public void CmdApply(string name, int skin)
        {
            _name = name;
            _skin = skin;
            RpcApply(_name, _skin);
        }

        [Command]
        public void CmdSetLookDirection(Vector2 lookDirection)
        {
            _lookDirection = lookDirection;
        }

        private void Update()
        {
            RpcApply(_name, _skin);
            
            if ( ! _lookDirection.Equals(Vector2.zero))
                RpcRotate(_lookDirection.ToDegrees());
        }

        [ClientRpc]
        private void RpcRotate(float zRotation)
        {
            _bow.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        [ClientRpc]
        private void RpcApply(string name, int skin)
        {
            _nameText.text = name;
            _skinRenderer.sprite = _skinsLibary.GetSkin(skin);
        }
    }
}