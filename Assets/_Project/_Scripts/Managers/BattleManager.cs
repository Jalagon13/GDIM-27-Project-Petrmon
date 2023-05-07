using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    /// <summary>
    /// GAMEPLAY TO DO LIST:
    /// - Need to research how stat buffs/debuffs work
    ///     - Add functionality for StatMove moves
    /// - Create actual Battle Sequence
    ///     - Add Dialogue and Narration.
    /// - Add Type functionality to moves
    ///     - Need to research how Types work 
    /// - Replace hard coded Petrmon calls (EX: _playerParty.Party[0]) with dynamically cached 
    /// private variables that change depending on which Petrmon the player and opponent has currently 
    /// out on the field.
    /// </summary>

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
            ShowBattleUI(false);
        }

        public void StartBattle(PartyObject opponentParty) // Hooked up to Start Battle Button
        {
            _opponentParty = opponentParty;

            UpdateMoves();
            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();
            ShowBattleUI(true);
        }

        public void Run() // Hooked up to Run Button
        {
            ShowBattleUI(false);
        }

        public void DebugAttackPlayer() // Hooked up to Attack Player Button
        {
            _opponentParty.Party[0].MoveSet.Set[0].Execute(_playerParty.Party[0]);
            UpdatePlayerPetrPanel();
        }

        public void DebugRefreshPetrmon() // Hooked up to Heal Petrmon Button
        {
            _playerParty.Party[0].RefreshPetrmon();
            UpdatePlayerPetrPanel();
        }

        private void UpdatePlayerPetrPanel() => _playerPetrPanel.UpdatePanel(_playerParty.Party[0]);

        private void UpdateOpponentPetrPanel() => _opponentPetrPanel.UpdatePanel(_opponentParty.Party[0]);

        private void UpdateMoves()
        {
            var targetPetrmon = _opponentParty.Party[0];
            var petrmonIndex = 0;
            var index = 0;

            foreach(Transform child in _fightButtonLayout.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    var move = _playerParty.Party[petrmonIndex].MoveSet.Set[index];

                    fightButton.UpdateFightButton(move, targetPetrmon, UpdateOpponentPetrPanel);
                    index++;
                }
            }
        }

        private void ShowBattleUI(bool var)
        {
            _battleCanvas.gameObject.SetActive(var);
            _playerAssets.SetActive(var);
            _opponentAssets.SetActive(var);
        }
    }
}