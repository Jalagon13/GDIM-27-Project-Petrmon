using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPetrmon
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float _interactRange;

        private DefaultInputActions _input;

        private void Awake()
        {
            _input = new();
            _input.Player.NextEntry.started += Interact;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Interact(InputAction.CallbackContext context)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRange);
            foreach (Collider collider in colliders)
            {
                if(collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }

        public IInteractable GetInteractableObject()
        {
            List<IInteractable> interactables = new List<IInteractable>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable npcInteractable))
                {
                    interactables.Add(npcInteractable);
                }
            }

            IInteractable closestNPCInteractable = null;
            foreach (IInteractable interactable in interactables)
            {
                if(closestNPCInteractable == null)
                {
                    closestNPCInteractable = interactable;
                }
                else
                {
                    if(Vector3.Distance(transform.position, interactable.Position) <
                       Vector3.Distance(transform.position, closestNPCInteractable.Position))
                    {
                        closestNPCInteractable = interactable;
                    }
                }
            }

            return closestNPCInteractable;
        }
    }
}
