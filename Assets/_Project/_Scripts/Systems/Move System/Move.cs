using UnityEngine;

namespace ProjectPetrmon
{
    public abstract class Move : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _maxPP;
        private int _currentPP;

        public string MoveName { get { return _name; } }
        public int MaxPP { get { return _maxPP; } }
        public int CurrentPP { get { return _currentPP; } }

        public void ResetPP()
        {
            _currentPP = MaxPP;
        }

        public virtual bool Execute(Petrmon targetPetrmon)
        {
            if(_currentPP == 0) 
                return false;

            _currentPP--;
            return true;
        }
    }
}
