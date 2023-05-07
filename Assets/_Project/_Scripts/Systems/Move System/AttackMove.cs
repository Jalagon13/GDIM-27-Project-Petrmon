using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[M_ATK] ", menuName = "Petrmon System/Moves/New Attack Move")]
    public class AttackMove : Move
    {
        [SerializeField] private int _power;

        public sealed override void Execute(PetrmonObject targetPetrmon)
        {
            
        }
    }
}
