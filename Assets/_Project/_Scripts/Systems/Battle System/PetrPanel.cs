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

        public void UpdatePanel(PetrmonObject petrmon)
        {
            _nameText.text = petrmon.Name;
            _hpText.text = $"HP: {petrmon.CurrentHP}/{petrmon.BaseMaxHP}";
        }
    }
}
