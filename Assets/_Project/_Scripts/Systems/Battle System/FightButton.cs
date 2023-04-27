using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class FightButton : MonoBehaviour
    {
        private Move _move;
        private Petrmon _targetPetrmon;
        private Button _fightButton;
        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _fightButton = GetComponent<Button>();
        }

        public void UpdateFightButton(Move move, Petrmon targetPetrmon)
        {
            _move = move;
            _targetPetrmon = targetPetrmon;

            UpdateDisplay();
            ButtonSetup();
        }

        private void UpdateDisplay()
        {
            _buttonText.text = _move.MoveName;
        }

        private void ButtonSetup()
        {
            _fightButton.onClick.RemoveAllListeners();
            _fightButton.onClick.AddListener(() =>
            {
                _move.Execute(_targetPetrmon);
            });
        }
    }
}
