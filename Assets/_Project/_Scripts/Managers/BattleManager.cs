using System.Collections;
using UnityEngine;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;

        private Canvas _battleCanvas;

        protected override void Awake()
        {
            base.Awake();
            _battleCanvas = transform.GetChild(0).GetComponent<Canvas>();
        }

        private void Start()
        {
            _battleCanvas.gameObject.SetActive(false);
        }

        public void StartBattle(PartyObject opponentParty)
        {
            _battleCanvas.gameObject.SetActive(true);
        }
    }
}
