using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCTrainer : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactText;
        [SerializeField] private float _gpaAwarded;
        [SerializeField] private string _npcName;
        [SerializeField] private PartyObject _npcPetrmonParty;
        [SerializeField] private DialogueObject _undefeatedDialogue;
        [SerializeField] private DialogueObject _defeatedDialogue;

        private bool _defeated;

        public string InteractText { get { return _interactText; } }
        public bool Defeated { get { return _defeated; } set { _defeated = value; } }
        public string NPCName { get { return _npcName; } }
        public PartyObject NPCParty { get { return _npcPetrmonParty; } }
        public Vector3 Position { get { return transform.position; } }
        public float GpaAwarded { get { return _gpaAwarded; } }

        public void Interact()
        {

            if (_defeated)
                DialogueManager.Instance.StartDialogue(_defeatedDialogue);
            else
                DialogueManager.Instance.StartDialogue(_undefeatedDialogue, this);
            //DialogueManager.Instance.StartDialogue(_dialogue, _npcName, _npcPetrmonParty);
        }
    }
}
