using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        [SerializeField] private PartyObject _playerParty;
        [SerializeField] private Canvas _battleCanvas; 
        [Header("Menu Panel Stuff")]
        [SerializeField] private RectTransform _playerPanel;
        [SerializeField] private RectTransform _opponentPanel;
        [SerializeField] private RectTransform _fightPanel;
        [SerializeField] private RectTransform _menuPanel;
        [SerializeField] private RectTransform _fightButtons;
        [Header("Battle Assets")]
        [SerializeField] private Image _currentPlayerPetrImage;
        [SerializeField] private Image _currentOpponentPetrImage;

        private PartyObject _opponentParty;
        private BattlePrompts _battlePrompts;
        private PetrPanel _playerPetrPanel;
        private PetrPanel _opponentPetrPanel;
        private PetrmonObject _currentPlayerPetrmon;
        private PetrmonObject _currentOpponentPetrmon;
        private WaitForSeconds _wait;

        protected override void Awake()
        {
            base.Awake();
            _battlePrompts = GetComponent<BattlePrompts>();
            _playerPetrPanel = _playerPanel.GetComponent<PetrPanel>();
            _opponentPetrPanel = _opponentPanel.GetComponent<PetrPanel>();
        }

        private void Start()
        {
            ShowBattleCanvas(false);
        }

        public void StartBattle(PartyObject opponentParty) // Hooked up to Start Battle Button
        {
            _opponentParty = opponentParty;
            _menuPanel.gameObject.SetActive(true);
            _fightPanel.gameObject.SetActive(false);

            _currentPlayerPetrmon = _playerParty.Party[0];
            _currentOpponentPetrmon = _opponentParty.Party[0];

            _currentPlayerPetrImage.sprite = _playerParty.Party[0].Sprite;
            _currentOpponentPetrImage.sprite = _opponentParty.Party[0].Sprite;

            UpdateMoves();
            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();
            InitializePetrmonBattleStats();
            ShowBattleCanvas(true);

            StartCoroutine(BeginningSequence());
        }

        private IEnumerator BeginningSequence()
        {
            _menuPanel.gameObject.SetActive(false);
            _fightPanel.gameObject.SetActive(false);
            _playerPanel.gameObject.SetActive(false);
            _opponentPanel.gameObject.SetActive(false);
            _currentOpponentPetrImage.gameObject.SetActive(true);
            _currentPlayerPetrImage.gameObject.SetActive(false);

            yield return WaitSeconds(2f);

            _opponentPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWildPetrmonAppearedText(_currentOpponentPetrmon.Name);

            yield return WaitSeconds(2f);

            _battlePrompts.DisplayGoPetrmonText(_currentPlayerPetrmon.Name);
            _currentPlayerPetrImage.gameObject.SetActive(true);

            yield return WaitSeconds(2f);

            _playerPanel.gameObject.SetActive(true);
            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentOpponentPetrmon.Name);
        }

        private WaitForSeconds WaitSeconds(float sec)
        {
            _wait = new(sec);
            return _wait;
        }

        public void ExitBattle() // Hooked up to Run Button
        {
            InitializePetrmonBattleStats();
            ShowBattleCanvas(false);
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
            var petrmonIndex = 0;
            var index = 0;

            foreach(Transform child in _fightButtons.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    var move = _playerParty.Party[petrmonIndex].MoveSet.Set[index];

                    fightButton.UpdateFightButton(move, BattleSequence);
                    index++;
                }
            }
        }

        private void BattleSequence(Move playerMoveToOpponent)
        {
            _menuPanel.gameObject.SetActive(false);
            _fightPanel.gameObject.SetActive(false);
            StartCoroutine(PlayerFirstSequence(playerMoveToOpponent));
        }

        private IEnumerator PlayerFirstSequence(Move move)
        {
            yield return PlayerMoveOnOpponent(move);
            yield return OpponentMoveOnPlayer();

            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
        }

        private IEnumerator PlayerMoveOnOpponent(Move move)
        {
            _battlePrompts.DisplayMoveUsedText(_currentPlayerPetrmon.Name, move.MoveName);

            yield return WaitSeconds(1.5f);

            // move animations

            string battleText = move.Execute(_currentPlayerPetrmon, _currentOpponentPetrmon);

            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();

            yield return WaitSeconds(1.5f);

            if(_currentOpponentPetrmon.CurrentHP <= 0)
            {
                // opponent faint animations

                _battlePrompts.DisplayFaintedText(_currentOpponentPetrmon.Name);

                yield return WaitSeconds(2f);
                yield return PlayerWins();
            }
            else if (battleText != string.Empty)
            {
                _battlePrompts.DisplayCustomText(battleText);
                yield return WaitSeconds(2f);
            }
        }

        private IEnumerator PlayerWins()
        {
            // start happy win music
            _battlePrompts.DisplayExpGainText(_currentPlayerPetrmon.Name);
            yield return WaitSeconds(4f);

            ExitBattle();
        }

        private IEnumerator OpponentMoveOnPlayer()
        {
            _battlePrompts.DisplayMoveUsedText(_currentOpponentPetrmon.Name, _currentOpponentPetrmon.MoveSet.Set[0].MoveName);

            yield return WaitSeconds(1.5f);

            // move animations

            string battleText = _currentOpponentPetrmon.MoveSet.Set[0].Execute(_currentOpponentPetrmon, _currentPlayerPetrmon);

            UpdatePlayerPetrPanel();
            UpdateOpponentPetrPanel();

            yield return WaitSeconds(1.5f);

            if(_currentPlayerPetrmon.CurrentHP <= 0)
            {
                // player petr faint animations

                _battlePrompts.DisplayFaintedText(_currentPlayerPetrmon.Name);

                yield return WaitSeconds(2f);
                yield return PlayerLoses();
            }
            else if (battleText != string.Empty)
            {
                _battlePrompts.DisplayCustomText(battleText);
                yield return WaitSeconds(2f);
            }
        }

        private IEnumerator PlayerLoses()
        {
            // loss game feel (if there is any)
            _battlePrompts.DisplayCustomText("Better luck <br>next time!");
            yield return WaitSeconds(4f);

            ExitBattle();
        }

        private void ShowBattleCanvas(bool var)
        {
            _battleCanvas.gameObject.SetActive(var);
        }
    }
}