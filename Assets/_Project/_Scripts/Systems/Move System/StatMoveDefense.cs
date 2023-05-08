using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_DFS] ", menuName = "Petrmon System/Moves/New Defense Stat Move")]
    public class StatMoveDefense : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterDefense(_multiplier);
                return $"{fromPetrmon.Name}'s Defense <br>rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterDefense(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name}'s Defense <br>rose!" :
                    _multiplier < 1 ? $"{toPetrmon.Name}'s Defense <br>fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
