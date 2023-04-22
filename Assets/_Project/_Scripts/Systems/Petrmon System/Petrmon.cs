using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private int _maxHp;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private StatSystem _statSystem;
        [SerializeField] private MoveSet _moveSet;

        private readonly HealthSystem _healthSystem;

        public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public HealthSystem HealthSystem { get { return _healthSystem; } }
        public StatSystem StatSystem { get { return _statSystem; } }
        public MoveSet MoveSet { get { return _moveSet; } }

        public Petrmon()
        {
            _healthSystem = new(_maxHp, OnFaint);
        }

        private void OnFaint()
        {
            // for when Petrmon Faints
        }
    }
}
