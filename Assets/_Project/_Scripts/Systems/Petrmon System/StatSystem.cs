using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [Serializable]
    public class StatSystem
    {
        //public List<StatInfo> Stats;

        private static int _statAmount = 3;

        public int StatAmount { get { return _statAmount; } }

        public StatSystem()
        {
            //Stats = new(_statAmount);
        }

        //public void ApplyStatChange(Stat stat, int changeAmount)
        //{
        //    foreach (StatInfo statInfo in Stats)
        //    {
        //        if (statInfo.StatEqualTo(stat))
        //        {
        //            statInfo.ApplyChangeToValue(changeAmount);
        //        }
        //    }
        //}
    }
}
