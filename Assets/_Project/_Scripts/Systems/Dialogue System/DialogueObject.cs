using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [Serializable]
    public class DialogueObject
    {
        [SerializeField] private List<string> _lines;

        public List<string> Lines { get { return new List<string>(_lines); } }

        public DialogueObject(string lines)
        {
            _lines = new List<string>(lines.Split("/"));
        }
    }
}
