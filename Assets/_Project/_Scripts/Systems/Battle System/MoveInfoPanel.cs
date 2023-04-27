using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class MoveInfoPanel : MonoBehaviour
    {
        private TextMeshProUGUI _ppText;
        private TextMeshProUGUI _typeText;

        private void Awake()
        {
            _ppText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _typeText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        public void UpdateMoveInfoPanel(Move move)
        {
            _ppText.SetText($"PP: {move.CurrentPP}/{move.MaxPP}");
            // moves have no type functionality built in
        }
    }
}
