using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[Petr] ", menuName = "Petrmon System/New Petrmon")]
    public class PetrmonObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private int _hp;
        [SerializeField] private int _attack;
        [SerializeField] private int _defense;
        [SerializeField] private int _speed;

        public string Name { get { return _name; } }
        public int HP { get { return _hp; } }
        public int Attack { get { return _attack; } }
        public int Defense { get { return _defense; } }
        public int Speed { get { return _speed; } }
    }
}
