using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectPetrmon
{
    /*
    public class DialogueComponent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private DialogueObjectA _thisConversation;
        [SerializeField] private bool _triggeredOnEntry;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_triggeredOnEntry)
            {
                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    DialogueManager.Instance.StartConversation(_thisConversation, false, gameObject);
                }
            }
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (_triggeredOnEntry)
            {
                if (collision.CompareTag("Player"))
                {
                    DialogueManager.Instance.StartConversation(_thisConversation, true, gameObject);
                }
            }
        }
    }
    */
}
