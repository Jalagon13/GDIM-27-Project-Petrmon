using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_SPD] ", menuName = "Petrmon System/Moves/New Speed Stat Move")]
    public class StatMoveSpeed : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            _currentPP--;

            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterSpeed(_multiplier);
                return $"{fromPetrmon.Name}'s Speed <br>rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterSpeed(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name}'s Speed <br>rose!" :
                    _multiplier < 1 ? $"{toPetrmon.Name}'s Speed <br>fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
