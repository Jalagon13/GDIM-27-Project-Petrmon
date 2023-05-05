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
        [SerializeField] protected Type _type;
        [SerializeField] private int _level;
        [SerializeField] protected int p_maxHp;
        [SerializeField] protected int _Defense;
        [SerializeField] private Sprite _sprite;
        [SerializeField] protected StatSystem p_statSystem;
        [SerializeField] protected MoveSet p_moveSet;
        

        protected HealthSystem p_healthSystem;

        public string PetrName => _name;
        public int Defense { get => _Defense; set { _Defense = value; } }
        public int MaxHp { get => p_maxHp; set { p_maxHp = value; } }
        public int Level { get => _level; set { _level = value; } }
        public HealthSystem HealthSystem { get => p_healthSystem; set { p_healthSystem = value; } }
        public StatSystem StatSystem => p_statSystem;
        public MoveSet MoveSet => p_moveSet;

        private void Awake()
        {
            Type waterType = Type.Water;
            Type fire = Type.Fire;

            if(fire == waterType)
            {

            }
        }
    }
}
