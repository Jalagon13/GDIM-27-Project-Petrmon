using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPetrmon
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private bool _triggeredOnEntry;
        [SerializeField] private PartyObject _party;
        [SerializeField] private DialogueObject _conversation;

        private bool _isEntered;

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isEntered = true;
                if (_triggeredOnEntry)
                {
                    //maybe have some animation coroutine run first, then dialogue? Like have the NPC run towards the character, like in the games.

                    DialogueManager.Instance.StartDialogue(_conversation, _party); // Will switch to the code below once the DialogueManager is updated to take in a DialogueObject, rather than printing out that hardcoded string:
                    //DialogueManager.Instance.StartDialogue(new DialogueObject(_thisConversation));
                }
            }

        }
        public void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isEntered = false;
            }

        }
    }
}
