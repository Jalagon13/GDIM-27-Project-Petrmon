using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class FightButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private MoveInfoPanel _moveInfoPanel;
        [SerializeField] private AudioClip _hoverClip;

        private Move _move;
        private Button _fightButton;
        private TextMeshProUGUI _buttonText;
        private Action<Move> _moveExecuteEvent;

        private void Awake()
        {
            _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _fightButton = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _moveInfoPanel.UpdateMoveInfoPanel(_move);
            AudioManager.Instance.PlayClip(_hoverClip, false, false, 1f);
        }

        public void UpdateFightButton(Move move, Action<Move> updateOpponentPetrPanel)
        {
            _move = move;
            _moveExecuteEvent = updateOpponentPetrPanel;

            UpdateDisplay();
            ButtonSetup();
        }

        private void UpdateDisplay()
        {
            _buttonText.text = $"{_move.MoveName.ToUpper()}";
        }

        private void ButtonSetup()
        {
            _fightButton.onClick.RemoveAllListeners();
            _fightButton.onClick.AddListener(() =>
            {
                if (_move.CurrentPP <= 0) return;

                _moveExecuteEvent?.Invoke(_move);
                _moveInfoPanel.UpdateMoveInfoPanel(_move);
            });
        }
    }
}
