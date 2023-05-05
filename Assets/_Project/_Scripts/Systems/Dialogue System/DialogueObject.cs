using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class DialogueObject
    {
        private List<string> _lines;
        private string _charName;
        private bool _displayCharName;
        private bool _isSign;

        public List<string> Lines { get { return new List<string>(_lines); } }
        public string CharName { get { return _charName; } }
        public bool DisplayCharName { get { return _displayCharName; } }

        public DialogueObject(string lines, string charName = null, bool displayCharName = false, bool isSign = false)
        {
            _lines = new List<string>(lines.Split("/"));
            _charName = charName;
            _displayCharName = displayCharName;
            _isSign = isSign;

        }
    }
}
