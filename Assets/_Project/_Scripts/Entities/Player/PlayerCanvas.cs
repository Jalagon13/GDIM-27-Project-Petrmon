using UnityEngine;
using TMPro;

namespace ProjectPetrmon
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statsText;
        [SerializeField] private PartyObject _playerParty;

        private void Start()
        {
            UpdateCanvasPetrStats();
        }

        private void OnEnable()
        {
            BattleManager.Instance.OnBattleEnd += UpdateCanvasPetrStats;
        }

        private void OnDisable()
        {
            BattleManager.Instance.OnBattleEnd -= UpdateCanvasPetrStats;
        }

        public void UpdateCanvasPetrStats()
        {
            string stats = $"Your Petrmon:\n";
            foreach (PetrmonObject petr in _playerParty.Party)
            {
                stats += $"{petr.Name}: {petr.CurrentHP}/{petr.BaseMaxHP} hp\n";
            }

            _statsText.text = stats;
        }
    }
}