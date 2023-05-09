using System;
using UnityEngine;

namespace ProjectPetrmon
{
    [Serializable]
    public class BattleStats
    {
        // these stats are the dynamic stats that are able to be altered during battle
        private int _battleAttack;
        private int _battleDefense;
        private int _battleSpeed;

        private int _baseAttackReference;
        private int _baseDefenseReference;
        private int _baseSpeedReference;

        public int BattleAttack { get { return _battleAttack; } }
        public int BattleDefense { get { return _battleDefense; } }
        public int BattleSpeed { get { return _battleSpeed; } }

        public void InitializeBattleStats(int baseAttack, int baseDefense, int baseSpeed)
        {
            _battleAttack = baseAttack;
            _battleDefense = baseDefense;
            _battleSpeed = baseSpeed;

            _baseAttackReference = baseAttack;
            _baseDefenseReference = baseDefense;
            _baseSpeedReference = baseSpeed;
        }

        public void ResetBattleStats()
        {
            _battleAttack = _baseAttackReference;
            _battleDefense = _baseDefenseReference;
            _battleSpeed = _baseSpeedReference;
        }

        public void AlterAttack(float multiplier)
        {
            float var = multiplier * _baseAttackReference;
            _battleAttack = (int)Mathf.Round(var);
        }

        public void AlterDefense(float multiplier)
        {
            float var = multiplier * _baseDefenseReference;
            _battleDefense = (int)Mathf.Round(var);
        }

        public void AlterSpeed(float multiplier)
        {
            float var = multiplier * _baseSpeedReference;
            _battleSpeed = (int)Mathf.Round(var);
        }
    }
}
