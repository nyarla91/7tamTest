using UnityEngine;

namespace Identity
{
    public interface IIdentityChange
    {
        void Apply(string name, int skin);
    }
}