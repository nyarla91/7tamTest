using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Identity
{
    public class AppearanceSetting : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _skin;
        [SerializeField] private TMP_InputField _input;
        
        [Inject] private IIdentityChange Identity { get; set; }

        public void Apply()
        {
            Identity.Apply(_input.text, _skin.value);
        }
    }
}