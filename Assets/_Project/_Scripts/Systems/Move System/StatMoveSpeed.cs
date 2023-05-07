using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_SPD] ", menuName = "Petrmon System/Moves/New Speed Stat Move")]
    public class StatMoveSpeed : Move
    {
        // The stages are: x1.5, x2.0, x2.5, x3.0, x3.5, x4.0
        // If you want to lower stat, multiply by appropriate fraction.
        [SerializeField] private float _multiplier;
        [SerializeField] private bool _useOnOwnPetrmon;

        public sealed override void Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            if (_useOnOwnPetrmon)
                fromPetrmon.BattleStats.AlterSpeed(_multiplier);
            else
                toPetrmon.BattleStats.AlterSpeed(_multiplier);
        }
    }
}
