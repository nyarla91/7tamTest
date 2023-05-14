using Gameplay.Character;
using UnityEngine;

namespace Gameplay
{
    public class Coin : Spawnable
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ( ! isServer || ! other.TryGetComponent(out CharacterScore score))
                return;
            score.AddOne();
            Unspawn();
        }
    }
}