using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    /// <summary>
    /// GAMEPLAY TO DO LIST:
    /// - Create actual Battle Sequence
    ///     - Add Dialogue and Narration.
    /// - Create AI for opponents
    /// - Replace hard coded Petrmon calls (EX: _playerParty.Party[0]) with dynamically cached 
    /// private variables that change depending on which Petrmon the player and opponent has currently 
    /// out on the field.
    /// </summary>

    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private PetrPanel _playerPetrPanel;
        [SerializeField] private PetrPanel _opponentPetrPanel;
        [SerializeField] private RectTransform _fightPanel;
        [SerializeField] private RectTransform _menuPanel;

        private Canvas _battleCanvas;
        private GridLayoutGroup _fightButtonLayout;
        private PartyObject _opponentParty;

        protected override void Awake()
        {
            base.Awake();

            _battleCanvas = transform.GetChild(0).GetComponent<Canvas>();
            _fightButtonLayout = transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            ShowBattleUI(false);
        }

        public void StartBattle(PartyObject opponentParty) // Hooked up to Start Battle Button
        {
            _opponentParty = opponentParty;
            _menuPanel.gameObject.SetActive(true);
            _fightPanel.gameObject.SetActive(false);

            UpdateMoves();
            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();
            InitializePetrmonBattleStats();
            ShowBattleUI(true);
        }

        public void Run() // Hooked up to Run Button
        {
            InitializePetrmonBattleStats();
            ShowBattleUI(false);
        }

        public void DebugAttackPlayer() // Hooked up to Attack Player Button
        {
            _opponentParty.Party[0].MoveSet.Set[0].Execute(_opponentParty.Party[0], _playerParty.Party[0]);
            UpdatePlayerPetrPanel();
        }

        public void DebugRefreshPetrmon() // Hooked up to Heal Petrmon Button
        {
            _playerParty.Party[0].RefreshPetrmon();
            UpdatePlayerPetrPanel();
        }

        private void InitializePetrmonBattleStats()
        {
            foreach (PetrmonObject petrmon in _playerParty.Party)
            {
                petrmon.InitializeBattleStats();
            }

            foreach (PetrmonObject petrmon in _opponentParty.Party)
            {
                petrmon.InitializeBattleStats();
            }
        }

        private void UpdatePlayerPetrPanel() => _playerPetrPanel.UpdatePanel(_playerParty.Party[0]);

        private void UpdateOpponentPetrPanel() => _opponentPetrPanel.UpdatePanel(_opponentParty.Party[0]);

        private void UpdateMoves()
        {
            var toPetrmon = _opponentParty.Party[0];
            var fromPetrmon = _playerParty.Party[0];
            var petrmonIndex = 0;
            var index = 0;

            foreach(Transform child in _fightButtonLayout.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    var move = _playerParty.Party[petrmonIndex].MoveSet.Set[index];

                    fightButton.UpdateFightButton(move, fromPetrmon, toPetrmon, UpdateOpponentPetrPanel);
                    index++;
                }
            }
        }

        private void ShowBattleUI(bool var)
        {
            _battleCanvas.gameObject.SetActive(var);
        }
    }
}