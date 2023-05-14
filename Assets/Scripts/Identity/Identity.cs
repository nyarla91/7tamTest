using UnityEngine;

namespace Identity
{
    public class Identity : MonoBehaviour, IIdentityChange, IIdentityInfo
    {
        [SerializeField] private Appearance _appearance;

        public Appearance Appearance => _appearance;

        public void Apply(string name, int skin)
        {
            _appearance = new Appearance(name, skin);
        }
    }
}