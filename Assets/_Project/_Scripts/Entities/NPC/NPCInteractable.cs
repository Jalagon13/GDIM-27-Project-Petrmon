using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class NPCInteractable : MonoBehaviour
    {
        [SerializeField] private string _interactText;

        public string InteractText { get { return _interactText; } }

        public void Interact()
        {
            Debug.Log("hi!");
        }
    }
}
