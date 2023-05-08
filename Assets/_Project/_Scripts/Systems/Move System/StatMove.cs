using UnityEngine;

namespace ProjectPetrmon
{
    public class StatMove : Move
    {
        // The stages are: x1.5, x2.0, x2.5, x3.0, x3.5, x4.0
        // If you want to lower stat, multiply by appropriate fraction.
        [SerializeField] protected float _multiplier;
        [SerializeField] protected bool _useOnOwnPetrmon;

        public bool UseOnOwnPetrmon { get { return _useOnOwnPetrmon; } }

        public override string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon)
        {
            return string.Empty;
        }
    }
}
