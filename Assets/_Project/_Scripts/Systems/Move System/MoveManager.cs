using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class MoveManager
    {
        private Move _move;
        protected int _currentPP;

        public MoveManager(Move move)
        {
            _move = move;
            _currentPP = MaxPP;
        }

        public int CurrentPP { get { return _currentPP; } }
        public string MoveName { get { return _move.MoveName; } }
        public int Accuracy { get { return _move.Accuracy; } }
        public int MaxPP { get { return _move.MaxPP; } }

        // Restores PP by given amount, maxes at MaxPP
        public void AddPP(int pp)
        {
            _currentPP += pp;
            if (_currentPP > _move.MaxPP)
                _currentPP = _move.MaxPP;
        }

        public void ResetPP()
        {
            _currentPP = _move.MaxPP;
        }

        public string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            _currentPP--;
            return _move.Execute(fromPetrmon, toPetrmon);
        }
    }
}
