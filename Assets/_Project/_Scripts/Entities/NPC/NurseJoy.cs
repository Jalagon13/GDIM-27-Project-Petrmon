using UnityEngine;

namespace ProjectPetrmon
{
    public class NurseJoy : MonoBehaviour
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private DialogueObject _conversation;

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                DialogueManager.Instance.StartDialogue(_conversation);
                _playerParty.Party[0].RefreshPetrmon();
            }
        }
    }
}
