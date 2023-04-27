using System.Collections;
using System.Collections.Generic;
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
                Debug.Log("Execute Successful");
                targetPetrmon.HealthSystem.TakeDamage(_damageAmount);
            }
            Debug.Log($"{targetPetrmon.name} Current HP: {targetPetrmon.HealthSystem.CurrentHp}/{targetPetrmon.HealthSystem.MaxHp}");
            //Debug.Log(targetPetrmon.HealthSystem == null);
            return true;
        }
    }
}
