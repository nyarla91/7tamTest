using TMPro;
using UnityEngine;

namespace Gameplay.Character
{
    [RequireComponent(typeof(CharacterVitals))]
    [RequireComponent(typeof(CharacterScore))]
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] private RectTransform _healthbar;
        [SerializeField] private TMP_Text _scoreCounter;
        
        private CharacterVitals _vitals;
        private CharacterVitals Vitals => _vitals ??= GetComponent<CharacterVitals>();
        private CharacterScore _score;
        private CharacterScore Score => _score ??= GetComponent<CharacterScore>();

        private void Update()
        {
            UpdateHealthbar();
            UpdateScore();
        }

        private void UpdateHealthbar()
        {
            float scale = Vitals.Percent;
            _healthbar.localScale = new Vector3(scale, 1, 1);
        }

        private void UpdateScore()
        {
            _scoreCounter.text = Score.CurrentScore.ToString();
        }
    }
}