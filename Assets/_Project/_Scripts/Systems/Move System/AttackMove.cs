using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_ATK] ", menuName = "Petrmon System/Moves/New Attack Move")]
    public class AttackMove : Move
    {
        [SerializeField] private int _damageAmount;

        public sealed override bool Execute(Petrmon targetPetrmon)
        {
            if (base.Execute(targetPetrmon))
            {
                targetPetrmon.HealthSystem.TakeDamage(_damageAmount);
            }

            return true;
        }
    }
}
