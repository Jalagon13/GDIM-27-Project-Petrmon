using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PetrPanel : MonoBehaviour
    {
        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _hpText;

        private void Awake()
        {
            _nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _hpText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void UpdatePanel(Petrmon petrmon)
        {
            _nameText.text = petrmon.PetrName;
            _hpText.text = $"HP: {petrmon.HealthSystem.CurrentHp}/{petrmon.HealthSystem.MaxHp}";
        }
    }
}
