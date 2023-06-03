using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectPetrmon
{
    public class SwapPanel : MonoBehaviour
    {
        [SerializeField] private BattleManager battleManagerInstance;
        private GameObject buttonsParent;
        private PetrmonSwapButton currentPetrmon;
        public void OnEnable()
        {
            currentPetrmon = this.transform.GetChild(1).gameObject.GetComponent<PetrmonSwapButton>();
            buttonsParent = this.transform.GetChild(2).gameObject;
            Debug.Log("SwapPanel Enabled. Calling swap button populate method, in BattleManager!");
            battleManagerInstance.UpdateSwappablePetrs(currentPetrmon, buttonsParent);
        }
    }
}
