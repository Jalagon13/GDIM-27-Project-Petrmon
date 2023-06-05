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
            if (toPetrmon.CurrentHP < 0)
                toPetrmon.CurrentHP = 0;
            if (_moveSFX) AudioManager.Instance.PlayClip(_moveSFX, false, true, GlobalSettings.VolumeSetting); 
            return string.Empty;
        }

        private float CalculateDamage(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            int tempLevel = 5; // replace with fromPetrmon's actual level once we have that coded (starts at 5 because that show it is in game)
            float multiplicity = getTypeMultiplicity(fromPetrmon, toPetrmon);
            float damage = (((2 * tempLevel / 5) + 2) * multiplicity * _power * (fromPetrmon.BattleStats.BattleAttack * 100 / toPetrmon.BattleStats.BattleDefense) / 50) / 100 + 2;

            return Mathf.Round(damage);
        }

        private float getTypeMultiplicity(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            return fromPetrmon.typeMultiplicity[fromPetrmon.type][toPetrmon.type];
        }
    }
}
