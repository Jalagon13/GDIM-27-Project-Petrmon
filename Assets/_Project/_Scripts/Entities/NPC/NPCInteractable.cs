using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCInteractable : MonoBehaviour
    {
        [SerializeField] private string _interactText;
        [SerializeField] private DialogueObject _dialogue;

        public string InteractText { get { return _interactText; } }

        public void Interact()
        {
            if (DialogueManager.Instance.InDialogue) return;
            Debug.Log("Entering dialogue");
            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
