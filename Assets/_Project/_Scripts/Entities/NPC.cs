using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPetrmon
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private string _thisConversation; // Lines separated by / characters
        [SerializeField] private bool _triggeredOnEntry;
        private bool _isEntered;

        private void NextEntry(InputAction.CallbackContext context)
        {
            if (_isEntered && !_triggeredOnEntry)
            {
                DialogueManager.Instance.StartDialogue(); // Will switch to the code below once the DialogueManager is updated to take in a DialogueObject, rather than printing out that hardcoded string:
                //DialogueManager.Instance.StartDialogue(new DialogueObject(_thisConversation));
            }
        }

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isEntered = true;
                if (_triggeredOnEntry)
                {
                    //maybe have some animation coroutine run first, then dialogue? Like have the NPC run towards the character, like in the games.

                    DialogueManager.Instance.StartDialogue(); // Will switch to the code below once the DialogueManager is updated to take in a DialogueObject, rather than printing out that hardcoded string:
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
