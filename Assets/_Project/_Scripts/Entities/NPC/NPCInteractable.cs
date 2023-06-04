using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCInteractable : MonoBehaviour
    {
        [SerializeField] private string _interactText;
        [SerializeField] private PartyObject _npcPetrmonParty;
        [SerializeField] private DialogueObject _undefeatedDialogue;
        [SerializeField] private DialogueObject _defeatedDialogue;

        private bool _defeated;

        public string InteractText { get { return _interactText; } }
        public bool Defeated { get { return _defeated; } set { _defeated = value; } }
        public PartyObject NPCParty { get { return _npcPetrmonParty; } }

        public void Interact()
        {
            foreach (PetrmonObject petr in _npcPetrmonParty.Party)
                petr.RefreshPetrmon();

            if (_defeated)
                DialogueManager.Instance.StartDialogue(_defeatedDialogue);
            else
                DialogueManager.Instance.StartDialogue(_undefeatedDialogue, this);
        }
    }
}
