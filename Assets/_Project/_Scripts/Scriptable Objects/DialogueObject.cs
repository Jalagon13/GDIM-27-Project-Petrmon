using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    [CreateAssetMenu(fileName = "Conversation", menuName = "Conversation System/Conversation")]
    public class DialogueObject : ScriptableObject
    {
        public DialogueEntryObject[] Lines;
    }

    [Serializable]
    public class DialogueEntryObject
    {
        public string Lines;
        public string CharName;
        public Sprite DisplayPic;
    }
}
