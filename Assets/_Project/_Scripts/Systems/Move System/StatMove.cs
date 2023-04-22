using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT] ", menuName = "Petrmon System/Moves/New Stat Move")]
    public class StatMove : Move
    {
        [SerializeField] private Stat _statToChange;
        [SerializeField] private int _changeAmount;

        public sealed override bool Execute(Petrmon targetPetrmon)
        {
            if (base.Execute(targetPetrmon))
                targetPetrmon.StatSystem.ApplyStatChange(_statToChange, _changeAmount);

            return true;
        }
    }
}
