using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "[Party] ", menuName = "Petrmon System/New Party")]
    public class PartyObject : ScriptableObject
    {
        [SerializeField] private List<PetrmonObject> _party;

        public List<PetrmonObject> Party { get { return _party; } }
    }
}
