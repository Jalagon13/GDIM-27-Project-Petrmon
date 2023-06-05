using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public interface IInteractable
    {
        string InteractText { get; }
        Vector3 Position { get; }
        void Interact();
    }
}
