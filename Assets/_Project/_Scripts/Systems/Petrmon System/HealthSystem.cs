using System;

namespace ProjectPetrmon
{
    public class HealthSystem 
    {
        public static event Action Fainted; 

        //private readonly Action _onFaint;
        private readonly int _maxHp;
        private int _currentHp;

        public int MaxHp { get { return _maxHp; } }
        public int CurrentHp { get { return _currentHp; } }

        public HealthSystem(int maxHp/*, Action onFaint*/)
        {
            //_onFaint = onFaint;
            //Fainted += _onFaint;

            _maxHp = maxHp;
            _currentHp = maxHp;
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
            _currentHp -= damage;

            if(_currentHp <= 0)
            {
                Fainted?.Invoke();
                //Fainted -= _onFaint;
            }
        }
    }
}
