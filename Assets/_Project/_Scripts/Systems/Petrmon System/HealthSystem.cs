using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class HealthSystem 
    {
        public static event Action Fainted; 

        private readonly Action _onFaint;
        private readonly int _maxHp;
        private int _currentHp;

        public int MaxHp { get { return _maxHp; } }
        public int CurrentHp { get { return _currentHp; } }

        public HealthSystem(int maxHp, Action onFaint)
        {
            _onFaint = onFaint;
            Fainted += _onFaint;

            _maxHp = maxHp;
            _currentHp = maxHp;
        }

        public void TakeDamage(int damage)
        {
            _currentHp -= damage;
            Debug.Log(CurrentHp);
            if(_currentHp <= 0)
            {
                Fainted?.Invoke();
                Fainted -= _onFaint;
            }
        }
    }
}
