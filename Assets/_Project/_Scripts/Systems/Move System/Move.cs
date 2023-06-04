using UnityEngine;

namespace ProjectPetrmon
{
    public abstract class Move : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _accuracy = 100;
        [SerializeField] private int _maxPP;
        [SerializeField] private Type _type;
        [SerializeField] protected AudioClip _moveSFX;

        public string MoveName { get { return _name; } }
        public int Accuracy { get { return _accuracy; } }
        public int MaxPP { get { return _maxPP; } }

        public abstract string Execute(PetrmonObject fromPetrmon, PetrmonObject toPetrmon);
    }
}
