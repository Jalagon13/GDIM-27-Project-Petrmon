using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[Party] ", menuName = "Petrmon System/New Party")]
    public class PartyObject : ScriptableObject
    {
        [SerializeField] private Dictionary<PetrmonObject, int> _party;
    }
}
