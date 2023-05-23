using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_DFS] ", menuName = "Petrmon System/Moves/New Defense Stat Move")]
    public class StatMoveDefense : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            _currentPP--;
            if (_moveSFX) AudioManager.Instance.PlayClip(_moveSFX, false, true, GlobalSettings.VolumeSetting); 

            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterDefense(_multiplier);
                return $"{fromPetrmon.Name.ToUpper()}'s Defense <br>rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterDefense(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name.ToUpper()}'s Defense <br>rose!" :
                    _multiplier < 1 ? $"{toPetrmon.Name.ToUpper()}'s Defense <br>fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
