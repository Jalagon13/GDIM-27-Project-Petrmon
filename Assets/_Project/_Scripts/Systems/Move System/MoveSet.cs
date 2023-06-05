using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [Serializable]
    public class MoveSet
    {
        [SerializeField] private List<Move> _moves;
        private List<MoveManager> _set;
        private System.Random rand = new System.Random();

        public int MoveSetAmount { get { return _set.Count; } }
        public List<MoveManager> Set { get { return _set; } }

        public void ExecuteMove(int moveIndex, PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            _set[moveIndex].Execute(fromPetrmon, toPetrmon);
        }

        public void RefreshPP()
        {
            if (_set == null)
            {
                _set = new List<MoveManager>();
                for (int i = 0; i < _moves.Count; i++)
                    _set.Add(new MoveManager(_moves[i]));
                return;
            }
            foreach (MoveManager move in _set)
            {
                move.ResetPP();
            }
            
        }
    }
}
