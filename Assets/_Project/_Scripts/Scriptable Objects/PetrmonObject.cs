using UnityEngine;
using System.Collections.Generic;

namespace ProjectPetrmon
{
    public enum Type{
        normal,
        grass,
        fire,
        water,
    }

    [CreateAssetMenu(fileName = "[Petr] ", menuName = "Petrmon System/New Petrmon")]
    public class PetrmonObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Type _type;
        [SerializeField] private Sprite _sprite;
        [Header("Persistent Stats")] // The only time these values change is when Petrmon levels up.
        [SerializeField] private int _baseMaxHp;
        [SerializeField] private int _baseAttack;
        [SerializeField] private int _baseDefense;
        [SerializeField] private int _baseSpeed;
        [Header("Non Persistent Stats")]
        [SerializeField] private float _currentHp;
        [SerializeField] private MoveSet _moveSet;

        private BattleStats _battleStats = new();

        public Dictionary<Type, Dictionary<Type, float>> typeMultiplicity = new Dictionary<Type, Dictionary<Type, float>>{
            {Type.normal, new Dictionary<Type, float>{{Type.normal, 1.0f}, { Type.grass, 1.0f}, {Type.water, 1.0f}, {Type.fire, 1.0f}}},
            {Type.grass, new Dictionary<Type, float>{ { Type.normal, 1.0f }, { Type.grass, 1.0f}, {Type.water, 1.2f}, {Type.fire, .8f}}},
            {Type.fire, new Dictionary<Type, float>{ { Type.normal, 1.0f }, { Type.grass, 1.2f}, {Type.water, .8f}, {Type.fire, 1.0f}}},
            {Type.water, new Dictionary<Type, float>{ { Type.normal, 1.0f }, { Type.grass, .8f}, {Type.water, 1.0f}, {Type.fire, 1.2f}}},
        };

        public string Name { get { return _name; } }
        public Sprite Sprite { get { return _sprite; } }

        // Persistent Stats
        public int BaseMaxHP { get { return _baseMaxHp; } }
        public int BaseAttack { get { return _baseAttack; } }
        public int BaseDefense { get { return _baseDefense; } }
        public int BaseSpeed { get { return _baseSpeed; } }
        public Type type {get { return _type; }}

        // Non Persistent Stats
        public float CurrentHP { get { return _currentHp; } 
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
