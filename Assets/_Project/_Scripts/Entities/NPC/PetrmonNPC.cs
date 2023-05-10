using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PetrmonNPC : MonoBehaviour
    {
        [SerializeField] private PartyObject _opponentParty;
        [SerializeField] private DialogueObject _conversation;

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                DialogueManager.Instance.StartDialogue(_conversation, _opponentParty);
            }
        }
    }
}
