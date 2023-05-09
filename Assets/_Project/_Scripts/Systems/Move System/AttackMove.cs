using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_ATK] ", menuName = "Petrmon System/Moves/New Attack Move")]
    public class AttackMove : Move
    {
        [SerializeField] private int _power;

        public sealed override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            toPetrmon.CurrentHP -= CalculateDamage(fromPetrmon, toPetrmon);
            _currentPP--;
            if (_moveSFX) AudioManager.Instance.PlayClip(_moveSFX, false, true, MainMenuSettings.VolumeSetting); 
            return string.Empty;
        }

        private int CalculateDamage(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            int tempLevel = 5; // replace with fromPetrmon's actual level once we have that coded (starts at 5 because that show it is in game)
            return (((2 * tempLevel / 5) + 2) * _power * (fromPetrmon.BattleStats.BattleAttack / toPetrmon.BattleStats.BattleDefense) / 50) + 2;
        }
    }
}
