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
                if(collider.TryGetComponent(out NPCInteractable npc))
                {
                    npc.Interact();
                }
            }
        }

        public NPCInteractable GetInteractableObject()
        {
            List<NPCInteractable> interactables = new List<NPCInteractable>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRange);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    interactables.Add(npcInteractable);
                }
            }

            NPCInteractable closestNPCInteractable = null;
            foreach (NPCInteractable interactable in interactables)
            {
                if(closestNPCInteractable == null)
                {
                    closestNPCInteractable = interactable;
                }
                else
                {
                    if(Vector3.Distance(transform.position, interactable.transform.position) <
                       Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                    {
                        closestNPCInteractable = interactable;
                    }
                }
            }

            return closestNPCInteractable;
        }
    }
}
