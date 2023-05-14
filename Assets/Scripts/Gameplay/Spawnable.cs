using Mirror;
using UnityEngine;

namespace Gameplay
{
    public class Spawnable : NetworkBehaviour
    {
        protected void Unspawn()
        {
            if ( ! isServer)
                return;
            NetworkServer.UnSpawn(gameObject);
            Destroy(gameObject);
        }
    }
}