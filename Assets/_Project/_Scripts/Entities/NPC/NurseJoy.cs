using UnityEngine;

namespace ProjectPetrmon
{
    public class NurseJoy : MonoBehaviour
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private PlayerCanvas _playerCanvas;
        [SerializeField] private DialogueObject _conversation;

        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                DialogueManager.Instance.StartDialogue(_conversation);
                foreach(PetrmonObject petr in _playerParty.Party)
                    petr.RefreshPetrmon();
                _playerCanvas.UpdateCanvasPetrStats();
            }
        }
    }
}
