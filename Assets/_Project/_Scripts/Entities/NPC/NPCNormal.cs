using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCNormal : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactText;
        [SerializeField] private DialogueObject _dialogue;

        public string InteractText => _interactText;

        public Vector3 Position => transform.position;

        public void Interact()
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
