using Connection;
using Identity;
using Mirror.Core;
using Mirror.Discovery;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _networkManagerPrefab;
    [SerializeField] private GameObject _skinsLibaryPrefab;
    [SerializeField] private GameObject _identityPrefab;
    
    public override void InstallBindings()
    {
        GameObject netowkrManager = BindFromPrefab<NetworkManager>(_networkManagerPrefab);
        Container.Bind<NetworkDiscovery>().FromInstance(netowkrManager.GetComponent<NetworkDiscovery>());
        
        BindFromPrefab<SkinsLibary>(_skinsLibaryPrefab);
        
        Identity.Identity identity = InstantiateForComponent<Identity.Identity>(_identityPrefab);
        Container.Bind<IIdentityChange>().FromInstance(identity).AsSingle();
        Container.Bind<IIdentityInfo>().FromInstance(identity).AsSingle();
    }

    private GameObject BindFromPrefab<T>(GameObject prefab) where T : Component
    {
        T instance = InstantiateForComponent<T>(prefab);
        Container.Bind<T>().FromInstance(instance).AsSingle();
        return instance.gameObject;
    }

    private T InstantiateForComponent<T>(GameObject prefab) => Container.InstantiatePrefab(prefab).GetComponent<T>();
}