using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCHealParty : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactText;
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private DialogueObject _dialogue;

        public string InteractText { get { return _interactText; } }
        public Vector3 Position { get { return transform.position; } }

        public void Interact()
        {
            foreach (PetrmonObject petr in _playerParty.Party)
                petr.RefreshPetrmon();

            DialogueManager.Instance.StartDialogue(_dialogue);
        }
    }
}
