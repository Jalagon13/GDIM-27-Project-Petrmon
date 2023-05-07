using System;

namespace ProjectPetrmon
{
    public class HealthSystem 
    {
        public static event Action Fainted; 

        private readonly int _maxHp;
        private int _currentHp;
        private int _Defense;

        public int MaxHp { get { return _maxHp; } }
        public int CurrentHp { get { return _currentHp; } }
        public int Defense{ get { return _Defense; } }

        public HealthSystem(int maxHp, int Defense)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
            _Defense = Defense;
        }

        // Restores Petrmon HP by the given amount, maxes at _maxHp.
        public void Heal(int healAmount)
        {
            _currentHp += healAmount;

            if (CurrentHp > healAmount)
                _currentHp = healAmount;
        }

        public void FullHeal()
        {
            _currentHp = _maxHp;
        }

        public void TakeDamage(int damage)
        {
            _currentHp -= (damage-_Defense);

            if(_currentHp <= 0)
            {
                Fainted?.Invoke();
            }
        }
    }
}
