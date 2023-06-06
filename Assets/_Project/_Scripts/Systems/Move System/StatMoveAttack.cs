using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_STAT_ATK] ", menuName = "Petrmon System/Moves/New Attack Stat Move")]
    public class StatMoveAttack : StatMove
    {
        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            if (_moveSFX) AudioManager.Instance.PlayClip(_moveSFX, false, true, GlobalSettings.VolumeSetting); 

            if (_useOnOwnPetrmon)
            {
                fromPetrmon.BattleStats.AlterAttack(_multiplier);
                return $"{fromPetrmon.Name.ToUpper()}'s Attack rose!";
            }
            else
            {
                toPetrmon.BattleStats.AlterAttack(_multiplier);
                return _multiplier > 1 ? $"{toPetrmon.Name.ToUpper()}'s Attack rose!" : 
                    _multiplier < 1 ? $"{toPetrmon.Name.ToUpper()}'s Attack fell!" : "ERROR MULTIPLER CAN'T BE 0!";
            }
        }
    }
}
