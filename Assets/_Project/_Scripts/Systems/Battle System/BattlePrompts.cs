using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class BattlePrompts : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _battleText;

        private void Awake()
        {
            _battleText.text = string.Empty;
        }

        public void DisplayCustomText(string customText)
        {
            _battleText.text = customText;
        }

        public void DisplayExpGainText(string petrmonName)
        {
            _battleText.text = $"{petrmonName} gained <br>EXP. Points!";
        }

        public void DisplayWhatWillPetrmonDoText(string petrmonName)
        {
            _battleText.text = $"What will<br>{petrmonName} do?";
        }

        public void DisplayMoveUsedText(string petrmonName, string moveName)
        {
            _battleText.text = $"{petrmonName} used <br>{moveName}!";
        }

        public void DisplayFaintedText(string petrmonName)
        {
            _battleText.text = $"{petrmonName} <br>fainted!";
        }

        public void DisplayStatRoseText(string petrmonName, string statName)
        {
            _battleText.text = $"{petrmonName}'s {statName} <br>rose!";
        }

        public void DisplayStatFellText(string petrmonName, string statName)
        {
            _battleText.text = $"{petrmonName}'s {statName} <br>fell!";
        }

        public void DisplayWildPetrmonAppearedText(string petrmonName)
        {
            _battleText.text = $"Wild {petrmonName} appeared!";
        }

        public void DisplayGoPetrmonText(string petrmonName)
        {
            _battleText.text = $"Go! {petrmonName}!";
        }
    }
}
