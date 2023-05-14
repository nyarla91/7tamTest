using Mirror;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class EndGameMessage : NetworkBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _winnerName;

        [ClientRpc]
        public void RpcShow(string winner, int score)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _winnerName.text = $"{winner} won!\nScore: {score}";
        }

        [ClientRpc]
        public void RpcHide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}