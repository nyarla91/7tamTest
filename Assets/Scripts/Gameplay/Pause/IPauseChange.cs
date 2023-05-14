using UnityEngine;

namespace Gameplay.Pause
{
    public interface IPauseChange
    {
        void AddPauseSource(MonoBehaviour source);
        void RemovePauseSource(MonoBehaviour source);
    }
}