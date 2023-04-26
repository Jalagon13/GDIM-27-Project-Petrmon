using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class FightButton : MonoBehaviour
    {
        private Move _move;

        public void UpdateFightButton(Move move)
        {
            Debug.Log($"UpdateFightButton() callback from [{name}]");
            Debug.Log($"Move Injected: [{move.MoveName}]");

            _move = move;
        }
    }
}
