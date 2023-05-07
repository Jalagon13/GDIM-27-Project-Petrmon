using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectPetrmon
{
    public class DialogueInteractable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private string _thisConversation; // Lines separated by / characters
        [SerializeField] private bool _triggeredOnEntry;
        private bool _isEntered;
        private bool _hasRun = false;

        /*
        public void Update()
        {
            if (!_triggeredOnEntry and self._isEntered)
            {
                //WRITE CODE CHECKING FOR IF CHARACTER HAS CLICKED E
                //STARTS DIALOGUE, SETS HASRUN TO TRUE
            }
        }
        */

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_triggeredOnEntry)
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    DialogueManager.Instance.StartConversation(new DialogueObject(_thisConversation), false, gameObject);
                }
            }
        }
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isEntered = true;
                if (_triggeredOnEntry)
                {
                    DialogueManager.Instance.StartConversation(new DialogueObject(_thisConversation), true, gameObject);
                }
            }
            
        }
        public void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isEntered = false;
            }

        }
    }
}
