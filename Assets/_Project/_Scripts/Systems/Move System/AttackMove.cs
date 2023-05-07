using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_ATK] ", menuName = "Petrmon System/Moves/New Attack Move")]
    public class AttackMove : Move
    {
        [SerializeField] private int _power;

        public sealed override void Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            toPetrmon.CurrentHP -= CalculateDamage(fromPetrmon, toPetrmon);
        }

        private int CalculateDamage(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            int tempLevel = 1; // replace with fromPetrmon's actual level once we have that coded
            return (((2 * tempLevel / 5) + 2) * _power * (fromPetrmon.BattleStats.BattleAttack / toPetrmon.BattleStats.BattleDefense)) / 50 + 2;
        }
    }
}
