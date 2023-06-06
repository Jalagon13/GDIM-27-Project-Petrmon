using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class GPAManager : Singleton<GPAManager>
    {
        [SerializeField] private TextMeshProUGUI _gpaText;
        [SerializeField] private Color _1GpaColor;
        [SerializeField] private Color _2GpaColor;
        [SerializeField] private Color _3GpaColor;
        [SerializeField] private Color _4GpaColor;

        private readonly float _startingGPA = 1.0f;
        private float _currentGPA;

        public float CurrentGPA { get { return _currentGPA; } }

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
            if(_currentGPA <= 1)
                _gpaText.color = _1GpaColor;
            else if(_currentGPA <= 2 && _currentGPA > 1)
                _gpaText.color = _2GpaColor;
            else if(_currentGPA <= 3 && _currentGPA > 2)
                _gpaText.color = _3GpaColor;
            else if(_currentGPA <= 4 && _currentGPA > 3)
                _gpaText.color = _4GpaColor;

            _gpaText.text = $"CURRENT GPA: {_currentGPA:0.0#}";
        }
    }
}
