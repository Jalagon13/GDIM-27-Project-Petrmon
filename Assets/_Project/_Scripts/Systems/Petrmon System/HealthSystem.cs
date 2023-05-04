using System;

namespace ProjectPetrmon
{
    public class HealthSystem 
    {
        public static event Action Fainted; 

        //private readonly Action _onFaint;
        private readonly int _maxHp;
        private int _currentHp;
        private int _Defense;
        private Type _type;

        public int MaxHp { get { return _maxHp; } }
        public int CurrentHp { get { return _currentHp; } }
        public int Defense{ get { return _Defense; } }
        public HealthSystem(int maxHp, int Defense, Type type/*, Action onFaint*/)
        {
            //_onFaint = onFaint;
            //Fainted += _onFaint;

            _maxHp = maxHp;
            _currentHp = maxHp;
            _Defense = Defense;
            // _type = type;
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
            // if(_type == Type Fire)
            // {
            //     _currentHp -= 200;
            // }
            _currentHp -= (damage-_Defense);

            if(_currentHp <= 0)
            {
                Fainted?.Invoke();
                //Fainted -= _onFaint;
            }
        }
    }
}
