using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_DFS] ", menuName = "Petrmon System/Moves/New Defense Stat Move")]
    public class StatMoveDefense : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            if (_moveSFX) AudioManager.Instance.PlayClip(_moveSFX, false, true, GlobalSettings.VolumeSetting); 

            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterDefense(_multiplier);
                return $"{fromPetrmon.Name.ToUpper()}'s Defense rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterDefense(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name.ToUpper()}'s Defense rose!" :
                    _multiplier < 1 ? $"{toPetrmon.Name.ToUpper()}'s Defense fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
