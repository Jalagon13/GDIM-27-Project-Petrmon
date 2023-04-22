using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [Serializable]
    public class MoveSet
    {
        public List<Move> Set;

        private readonly static int _setAmount = 4;

        public int MoveSetAmount { get { return _setAmount; } }

        public MoveSet()
        {
            Set = new(_setAmount);
        }

        public void ExecuteMove(int moveIndex, Petrmon targetPetrmon)
        {
            Set[moveIndex].Execute(targetPetrmon);
        }

        public void RefreshPP()
        {
            foreach (Move move in Set)
            {
                move.ResetPP();
            }
        }
    }
}
