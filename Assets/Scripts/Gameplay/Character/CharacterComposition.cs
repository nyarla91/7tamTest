using Extentions;
using Gameplay.Character.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Character
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterAttack))]
    [RequireComponent(typeof(CharacterAppearance))]
    public class CharacterComposition : NetworkTransformableBehaviour
    {
        [SerializeField] private GameObject _localPlayerPrefab;

        private CharacterMovement _movement;
        private CharacterAttack _attack;
        private CharacterAppearance _appearance;
        private CharacterVitals _vitals;
        private CharacterScore _score;
         
        public CharacterMovement Movement => _movement ??= GetComponent<CharacterMovement>();
        public CharacterAttack Attack => _attack ??= GetComponent<CharacterAttack>();
        public CharacterAppearance Appearance => _appearance ??= GetComponent<CharacterAppearance>();
        public CharacterVitals Vitals => _vitals ??= GetComponent<CharacterVitals>();
        public CharacterScore Score => _score ??= GetComponent<CharacterScore>();

        [Inject] private ServerGamePhase Phase { get; set; }
        
        [Inject]
        private void Construct(ServerGamePhase phase)
        {
            if ( ! isServer)
                return;
            Phase = phase;
        }

        private void OnDestroy()
        {
            if ( ! isServer)
                return;
            Phase.RemoveCharacter(this);
        }

        private void Start()
        {
            InitLocalPlayer();

            Phase.AddCharacter(this);
        }

        private void InitLocalPlayer()
        {
            if (!isLocalPlayer)
                return;
            PlayerPresenter presenter = Instantiate(_localPlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerPresenter>();
            presenter.Init(this);
        }

        private void OnValidate()
        {
            if (_localPlayerPrefab.GetComponent<PlayerPresenter>() != null)
                return;
            
            Debug.LogWarning("LocalPlayerPrefab must have PlayerPresenter component");
            _localPlayerPrefab = null;
        }
    }
}