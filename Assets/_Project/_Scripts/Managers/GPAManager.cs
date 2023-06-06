using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class GPAManager : Singleton<GPAManager>
    {
        [SerializeField] private TextMeshProUGUI _gpaText;

        private readonly float _startingGPA = 1.1f;
        private float _currentGPA;

        private void Start()
        {
            AwardGPA(_startingGPA);
        }

        public void AwardGPA(float gpaAwarded)
        {
            _currentGPA += gpaAwarded;
            UpdateGPAText();
        }

        private void UpdateGPAText()
        {
            _gpaText.text = $"CURRENT GPA: {_currentGPA}";
        }
    }
}
