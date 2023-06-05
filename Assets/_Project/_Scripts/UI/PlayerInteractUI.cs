using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PlayerInteractUI : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private PlayerInteract _playerInteract;
        [SerializeField] private TextMeshProUGUI _interactText;

        private void Update()
        {
            if(_playerInteract.GetInteractableObject() != null)
            {
                Show(_playerInteract.GetInteractableObject());
            }
            else
            {
                Hide();
            }
        }

        private void Show(IInteractable npcInteractable)
        {
            _container.SetActive(true);
            _interactText.text = npcInteractable.InteractText;
        }

        private void Hide()
        {
            _container.SetActive(false);
        }
    }
}
