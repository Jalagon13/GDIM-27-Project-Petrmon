using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PetrPanel : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _hpText;

        private void Awake()
        {
            _nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _hpText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void UpdatePanel(PetrmonObject petrmon)
        {
            _nameText.text = petrmon.Name.ToUpper();
            _hpText.text = $"{petrmon.CurrentHP}/{petrmon.BaseMaxHP}";
            _healthBar.UpdateFill(petrmon.CurrentHP, petrmon.BaseMaxHP);
        }
    }
}
