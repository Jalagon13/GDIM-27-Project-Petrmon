using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class FightButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private MoveInfoPanel _moveInfoPanel;

        private Move _move;
        private PetrmonObject _toPetrmon;
        private PetrmonObject _fromPetrmon;
        private Button _fightButton;
        private TextMeshProUGUI _buttonText;
        private Action _moveExecuteEvent;

        private void Awake()
        {
            _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _fightButton = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _moveInfoPanel.UpdateMoveInfoPanel(_move);
        }

        public void UpdateFightButton(Move move, PetrmonObject fromPetrmon, PetrmonObject toPetrmon, Action updateOpponentPetrPanel)
        {
            _move = move;
            _fromPetrmon = fromPetrmon;
            _toPetrmon = toPetrmon;
            _moveExecuteEvent = updateOpponentPetrPanel;

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
                _move.Execute(_fromPetrmon, _toPetrmon);
                _moveExecuteEvent?.Invoke();
                _moveInfoPanel.UpdateMoveInfoPanel(_move);
            });
        }
    }
}
