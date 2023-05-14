using Gameplay.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private ServerPause _pause;
        [SerializeField] private ServerGamePhase _gamePhase;
        
        public override void InstallBindings()
        {
            Container.Bind<IPauseChange>().FromInstance(_pause).AsSingle();
            Container.Bind<IPauseInfo>().FromInstance(_pause).AsSingle();
            Container.Bind<ServerGamePhase>().FromInstance(_gamePhase).AsSingle();
        }
    }
}