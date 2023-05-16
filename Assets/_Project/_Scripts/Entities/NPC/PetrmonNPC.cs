using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PetrmonNPC : MonoBehaviour
    {
        [SerializeField] private PartyObject _opponentParty;
        [SerializeField] private DialogueObject _battleConversation;
        [SerializeField] private DialogueObject _lossConversation;

        private bool _alreadyBattled = false;

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!_alreadyBattled)
                {
                    DialogueManager.Instance.StartDialogue(_battleConversation, _opponentParty);
                    _alreadyBattled = true;
                }
                else
                    DialogueManager.Instance.StartDialogue(_lossConversation);
            }
        }
    }
}
