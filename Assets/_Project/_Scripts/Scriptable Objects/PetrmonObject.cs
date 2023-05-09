using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[Petr] ", menuName = "Petrmon System/New Petrmon")]
    public class PetrmonObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [Header("Persistent Stats")] // The only time these values change is when Petrmon levels up.
        [SerializeField] private int _baseMaxHp;
        [SerializeField] private int _baseAttack;
        [SerializeField] private int _baseDefense;
        [SerializeField] private int _baseSpeed;
        [Header("Non Persistent Stats")]
        [SerializeField] private int _currentHp;
        [SerializeField] private MoveSet _moveSet;

        private BattleStats _battleStats = new();

        public string Name { get { return _name; } }
        public Sprite Sprite { get { return _sprite; } }

        // Persistent Stats
        public int BaseMaxHP { get { return _baseMaxHp; } }
        public int BaseAttack { get { return _baseAttack; } }
        public int BaseDefense { get { return _baseDefense; } }
        public int BaseSpeed { get { return _baseSpeed; } }

        // Non Persistent Stats
        public int CurrentHP { get { return _currentHp; } 
            set 
            { 
                _currentHp = value > _baseMaxHp ? _baseMaxHp : value;
            } 
        }
        public MoveSet MoveSet { get { return _moveSet; } }
        public BattleStats BattleStats { get { return _battleStats; } }

        public void InitializeBattleStats()
        {
            BattleStats.InitializeBattleStats(_baseAttack, _baseDefense, _baseSpeed);
        }

        public void RefreshPetrmon()
        {
            _currentHp = _baseMaxHp;
            _moveSet.RefreshPP();
        }
    }
}
