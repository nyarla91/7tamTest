using System.Collections.Generic;
using Gameplay.Character;
using Gameplay.Pause;
using Mirror;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ServerGamePhase : NetworkBehaviour
    {
        [SerializeField] private ServerCoinSpawner _coinSpawner;
        [SerializeField] private EndGameMessage _message;
        
        private readonly List<CharacterComposition> _charactersAlive = new List<CharacterComposition>();
        
        [Inject] private IPauseChange Pause { get; set; }
        
        public void AddCharacter(CharacterComposition character)
        {
            if (_charactersAlive.Contains(character))
                return;
            
            _charactersAlive.Add(character);

            character.Vitals.Dead += () => RemoveCharacter(character);
            
            if (_charactersAlive.Count > 1)
                Pause.RemovePauseSource(this);
        }
        
        public void RemoveCharacter(CharacterComposition character)
        {
            if (! _charactersAlive.Contains(character))
                return;
            _charactersAlive.Remove(character);

            if (_charactersAlive.Count == 1)
            {
                CharacterComposition winner = _charactersAlive[0];
                Pause.AddPauseSource(this);
                _message.RpcShow(winner.Appearance.Name, winner.Score.CurrentScore);
            }
        }

        private void Awake()
        {
            GetComponent<NetworkIdentity>().serverOnly = true;
        }

        private void Start()
        {
            Pause.AddPauseSource(this);
        }
    }
}