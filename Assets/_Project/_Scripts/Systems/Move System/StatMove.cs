using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT] ", menuName = "Petrmon System/Moves/New Stat Move")]
    public class StatMove : Move
    {
        [SerializeField] private float _multiplier;

        public sealed override void Execute(PetrmonObject targetPetrmon)
        {
            
        }
    }
}
