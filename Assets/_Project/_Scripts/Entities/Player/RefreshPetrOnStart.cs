using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class RefreshPetrOnStart : MonoBehaviour
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private PlayerCanvas _playerCanvas;

        private void Start()
        {
            foreach (PetrmonObject petr in _playerParty.Party)
                petr.RefreshPetrmon();
            _playerCanvas.UpdateCanvasPetrStats();
        }
    }
}
