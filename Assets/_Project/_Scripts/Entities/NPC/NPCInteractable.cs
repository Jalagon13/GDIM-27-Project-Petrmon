using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCInteractable : MonoBehaviour
    {
        [SerializeField] private string _interactText;
        [SerializeField] private PartyObject _npcPetrmonParty;
        [SerializeField] private DialogueObject _dialogue;

        public string InteractText { get { return _interactText; } }

        public void Interact()
        {
            DialogueManager.Instance.StartDialogue(_dialogue, _npcPetrmonParty);
        }
    }
}
