using Identity;
using UnityEngine;
using Zenject;

namespace Gameplay.Character.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerPresenter : MonoBehaviour
    {
        private CharacterComposition _character;

        private PlayerInput _input;
        private PlayerInput Input => _input ??= GetComponent<PlayerInput>();

        [Inject] private IIdentityInfo Identity { get; set; }
        
        public void Init(CharacterComposition character)
        {
            _character = character;
            _character.Appearance.CmdApply(Identity.Appearance.Name, Identity.Appearance.Skin);
        }

        private void Update()
        {
            _character.Movement.CmdSetMoveInput(Input.Movement);
            _character.Attack.CmdSetFireVector(Input.Fire);
            _character.Appearance.CmdSetLookDirection(Input.LookDirection);
        }
    }
}
