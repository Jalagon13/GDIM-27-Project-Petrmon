using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class TestDialogueStarter : MonoBehaviour
    {
        [SerializeField] private List<DialogueTest> _test;
        [TextArea]
        [SerializeField] private string _dialogueText;

        // Start is called before the first frame update
        void Start()
        {
            /*while (true)
            { }*/
            DialogueManager.Instance.StartConversation(new DialogueObject("DEV SAYS I AM BALD/MAIN MENU IS BAD/GONNA ADD A WHILE TRUE"), false, gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

    [Serializable]
    public class DialogueTest
    {
        [TextArea]
        public string _dialogueText;
    }
}
