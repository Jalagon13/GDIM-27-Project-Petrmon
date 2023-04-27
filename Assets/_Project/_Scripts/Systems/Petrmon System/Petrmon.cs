using UnityEngine;

namespace ProjectPetrmon
{
    public enum Type
    {
        Fire,
        Water,
        Stone
    }

    public abstract class Petrmon : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Type _type;
        [SerializeField] private int _level;
        [SerializeField] protected int p_maxHp;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private StatSystem _statSystem;
        [SerializeField] private MoveSet _moveSet;

        protected HealthSystem p_healthSystem;

        public string PetrName => _name;
        public int MaxHp { get => p_maxHp; set { p_maxHp = value; } }
        public int Level { get => _level; set { _level = value; } }
        public HealthSystem HealthSystem => p_healthSystem;
        public StatSystem StatSystem => _statSystem;
        public MoveSet MoveSet => _moveSet;
    }
}
