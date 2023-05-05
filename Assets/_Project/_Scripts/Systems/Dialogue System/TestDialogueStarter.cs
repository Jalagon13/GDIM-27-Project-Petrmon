using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class TestDialogueStarter : MonoBehaviour
    {
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
}
