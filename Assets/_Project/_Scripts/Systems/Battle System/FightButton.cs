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

        private MoveManager _move;
        private Button _fightButton;
        private TextMeshProUGUI _buttonText;
        private Action<MoveManager> _moveExecuteEvent;

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

        public void UpdateFightButton(MoveManager move, Action<MoveManager> updateOpponentPetrPanel)
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
            gameObject.SetActive(true);
            _fightButton.onClick.AddListener(() =>
            {
                if (_move.CurrentPP <= 0) return;

                _moveExecuteEvent?.Invoke(_move);
                _moveInfoPanel.UpdateMoveInfoPanel(_move);
            });
        }

        public void HideButton()
        {
            _fightButton.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}
