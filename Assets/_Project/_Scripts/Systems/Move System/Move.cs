using UnityEngine;

namespace ProjectPetrmon
{
    public abstract class Move : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _accuracy = 100;
        [SerializeField] private int _maxPP;
        private int _currentPP;

        public string MoveName { get { return _name; } }
        public int Accuracy { get { return _accuracy; } }
        public int MaxPP { get { return _maxPP; } }
        public int CurrentPP { get { return _currentPP; } }

        // Restores PP by given amount, maxes at MaxPP
        public void AddPP(int pp)
        {
            _currentPP += pp;
            if (_currentPP > MaxPP)
                _currentPP = MaxPP;
        }

        public void ResetPP()
        {
            _currentPP = MaxPP;
        }

        public abstract void Execute(PetrmonObject targetPetrmon);
    }
}
