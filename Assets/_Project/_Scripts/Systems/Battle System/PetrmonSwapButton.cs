using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class PetrmonSwapButton : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private TextMeshProUGUI _petrmonName;
        [SerializeField] private Image _petrmonIconObject;
        [SerializeField] private AudioClip _hoverClip;

        private PetrmonObject _petr;
        private Button _petrmonSwapButton;

        private void Awake()
        {
            _hpText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            _petrmonName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _petrmonIconObject = transform.GetChild(2).GetComponent<Image>();
            //_fightButton = GetComponent<Button>();
        }

        public void UpdatePetrmonButton(PetrmonObject petr)
        {
            _petr = petr;

            UpdateDisplay();
            //ButtonSetup();
        }

        public void BlankButton()
        {
            _petrmonName.text = "";
            _hpText.text = "";
            _petrmonIconObject.gameObject.SetActive(false);
            _healthBar.gameObject.SetActive(false);
        }

        private void UpdateDisplay()
        {
            _petrmonIconObject.gameObject.SetActive(true);
            _healthBar.gameObject.SetActive(true);
            _petrmonName.text = $"{_petr.Name}";
            _petrmonIconObject.sprite = _petr.Sprite;
            _hpText.text = $"{_petr.CurrentHP}/{_petr.BaseMaxHP}";
            _healthBar.UpdateFill(_petr.CurrentHP, _petr.BaseMaxHP);
        }

        /*
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
        */
    }
}
