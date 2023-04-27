using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private GameObject _playerAssets;
        [SerializeField] private GameObject _opponentAssets;

        private Canvas _battleCanvas;
        private GridLayoutGroup _fightButtonLayout;
        private PartyObject _opponentParty;
        private PetrPanel _playerPetrPanel;
        private PetrPanel _opponentPetrPanel;

        protected override void Awake()
        {
            base.Awake();

            _battleCanvas = transform.GetChild(0).GetComponent<Canvas>();
            _fightButtonLayout = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>();
            _playerPetrPanel = transform.GetChild(0).GetChild(1).GetComponent<PetrPanel>();
            _opponentPetrPanel = transform.GetChild(0).GetChild(2).GetComponent<PetrPanel>();
        }

        private void Start()
        {
            _battleCanvas.gameObject.SetActive(false);
            _playerAssets.SetActive(false);
            _opponentAssets.SetActive(false);
        }

        public void StartBattle(PartyObject opponentParty)
        {
            _opponentParty = opponentParty;
            _playerParty.Party[0].MoveSet.RefreshPP();
            _opponentParty.Party[0].MoveSet.RefreshPP();

            UpdateMoves();
            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();
            ShowAssets();
        }

        private void UpdatePlayerPetrPanel() => _playerPetrPanel.UpdatePanel(_playerParty.Party[0]);

        private void UpdateOpponentPetrPanel() => _opponentPetrPanel.UpdatePanel(_opponentParty.Party[0]);

        private void UpdateMoves()
        {
            var petrmonIndex = 0;
            var index = 0;

            foreach(Transform child in _fightButtonLayout.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    var move = _playerParty.Party[petrmonIndex].MoveSet.Set[index];
                    var targetPetrmon = _opponentParty.Party[0];

                    fightButton.UpdateFightButton(move, targetPetrmon, UpdateOpponentPetrPanel);
                    index++;
                }
            }
        }

        private void ShowAssets()
        {
            _battleCanvas.gameObject.SetActive(true);
            _playerAssets.SetActive(true);
            _opponentAssets.SetActive(true);
        }
    }
}
