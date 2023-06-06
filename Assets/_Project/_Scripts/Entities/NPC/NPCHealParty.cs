using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCHealParty : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactText;
        [SerializeField] private AudioClip _healSound;
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private DialogueObject _dialogue;

        public string InteractText { get { return _interactText; } }
        public Vector3 Position { get { return transform.position; } }

        public void Interact()
        {
            if (DialogueManager.Instance.InDialogue) return;

            foreach (PetrmonObject petr in _playerParty.Party)
                petr.RefreshPetrmon();

            AudioManager.Instance.PlayClip(_healSound, false, false);
            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
