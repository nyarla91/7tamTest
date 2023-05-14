using System;
using Mirror;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterScore : NetworkBehaviour
    {
        [SyncVar] private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                if (_currentScore == value)
                    return;
                _currentScore = value;
                CurrentScoreChanged?.Invoke(gameObject, _currentScore);
            }
        }

        public event Action<GameObject, int> CurrentScoreChanged; 

        public void AddOne()
        {
            if ( ! isServer)
                return;
            CurrentScore++;
        }

        public void Restart()
        {
            if ( ! isServer)
                return;
            CurrentScore = 0;
        }
    }
}