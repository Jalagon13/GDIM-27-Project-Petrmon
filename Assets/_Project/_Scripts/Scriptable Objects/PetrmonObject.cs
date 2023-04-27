using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[Petr] ", menuName = "Petrmon System/New Petrmon")]
    public class PetrmonObject : Petrmon, ISerializationCallbackReceiver
    {
        public void OnAfterDeserialize()
        {
            p_healthSystem = new(p_maxHp);
            p_moveSet.RefreshPP();

            if (Level <= 0) Level = 1;
            if (MaxHp <= 0) MaxHp = 10;

            //if(StatSystem.Attack <= 0) StatSystem.Attack = 10;
            //if(StatSystem.Defense <= 0) StatSystem.Defense = 10;
            //if(StatSystem.Speed <= 0) StatSystem.Speed = 10;

            if (MoveSet.Set.Count != 4)
                MoveSet.Set = new(MoveSet.MoveSetAmount);

            if (StatSystem.Stats.Count != 3)
                StatSystem.Stats = new(StatSystem.StatAmount);
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}
