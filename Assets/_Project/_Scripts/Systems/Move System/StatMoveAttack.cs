using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_ATK] ", menuName = "Petrmon System/Moves/New Attack Stat Move")]
    public class StatMoveAttack : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            _currentPP--;

            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterAttack(_multiplier);
                return $"{fromPetrmon.Name}'s Attack <br>rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterAttack(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name}'s Attack <br>rose!" : 
                    _multiplier < 1 ? $"{toPetrmon.Name}'s Attack <br>fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
