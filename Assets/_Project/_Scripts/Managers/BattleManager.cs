using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class BattleManager : Singleton<BattleManager>
    {
        public event Action OnBattleStart;
        public event Action OnBattleEnd;

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

        [Header("Sound Assets")]
        [SerializeField] private AudioClip _buttonClickSound;
        [SerializeField] private AudioClip _playerLoseSound;
        [SerializeField] private AudioClip _playerWinSound;
        [SerializeField] private AudioClip _battleBGMSound;

        private List<int> _playerPartyRef;
        private PartyObject _opponentParty;
        private BattlePrompts _battlePrompts;
        private Animator _playerPetrAnim;
        private Animator _opponentPetrAnim;
        private PetrPanel _playerPetrPanel;
        private PetrPanel _opponentPetrPanel;
        private PetrmonObject _currentPlayerPetrmon;
        private PetrmonObject _currentOpponentPetrmon;
        private WaitForSeconds _wait;

        protected override void Awake()
        {
            base.Awake();
            _battlePrompts = GetComponent<BattlePrompts>();
            _playerPetrAnim = _currentPlayerPetrImage.GetComponent<Animator>();
            _opponentPetrAnim = _currentOpponentPetrImage.GetComponent<Animator>();
            _playerPetrPanel = _playerPanel.GetComponent<PetrPanel>();
            _opponentPetrPanel = _opponentPanel.GetComponent<PetrPanel>();
        }

        private void Start()
        {
            ShowBattleCanvas(false);
        }

        public void StartBattle(PartyObject opponentParty) // Hooked up to Start Battle Button
        {
            _playerPartyRef = new List<int>() { 0, 1, 2, 3, 4, 5 };
            _opponentParty = opponentParty;
            _battlePrompts.DisplayCustomText(string.Empty);
            _menuPanel.gameObject.SetActive(true);
            _fightPanel.gameObject.SetActive(false);

            _currentPlayerPetrmon = _playerParty.Party[0];
            _currentOpponentPetrmon = _opponentParty.Party[0];

            _currentPlayerPetrImage.sprite = _currentPlayerPetrmon.Sprite;
            _currentOpponentPetrImage.sprite = _currentOpponentPetrmon.Sprite;

            _currentOpponentPetrmon.RefreshPetrmon();

            OnBattleStart?.Invoke();

            UpdateMoves();
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
            _currentPlayerPetrImage.gameObject.SetActive(false);
            _currentOpponentPetrImage.gameObject.SetActive(false);
            AudioManager.Instance.PlayClip(_battleBGMSound, true, false, MainMenuSettings.MusicSetting);

            yield return WaitSeconds(0.5f);

            _opponentPanel.gameObject.SetActive(true);
            _currentOpponentPetrImage.gameObject.SetActive(true);
            _opponentPetrAnim.SetTrigger("spawn");
            _battlePrompts.DisplayWildPetrmonAppearedText(_currentOpponentPetrmon.Name);

            yield return WaitSeconds(1.5f);

            _currentPlayerPetrImage.gameObject.SetActive(true);
            _playerPetrAnim.SetTrigger("spawn");
            _battlePrompts.DisplayGoPetrmonText(_currentPlayerPetrmon.Name);

            yield return WaitSeconds(1f);

            _playerPanel.gameObject.SetActive(true);
            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
        }

        private WaitForSeconds WaitSeconds(float sec)
        {
            _wait = new(sec);
            return _wait;
        }

        public void ExitBattle() // Hooked up to Run Button
        {
            AudioManager.Instance.StopClip(_battleBGMSound);
            InitializePetrmonBattleStats();
            StopAllCoroutines();
            ShowBattleCanvas(false);

            // disable battle trigger here

            OnBattleEnd?.Invoke();
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

        private void UpdateMoves()
        {
            var index = 0;

            foreach(Transform child in _fightButtons.transform)
            {
                if(child.TryGetComponent(out FightButton fightButton))
                {
                    var move = _currentPlayerPetrmon.MoveSet.Set[index];

                    fightButton.UpdateFightButton(move, BattleRoutine);
                    index++;
                }
            }

            UpdateCurrentPetrPanels();
        }
        // Alaina works above this line
        public void SwapRoutine(int petrPartySlot)
        {
            _menuPanel.gameObject.SetActive(false);

            StartCoroutine(SwapSequence(petrPartySlot));
        }

        private IEnumerator SwapSequence(int slot)
        {
            _battlePrompts.DisplayWithdrawPetrmonText("You", _currentPlayerPetrmon.name);
            yield return WaitSeconds(1.5f);

            _playerPetrAnim.SetTrigger("swap");
            yield return WaitSeconds(1.5f);

            // make the swap after petrmon is withdrawn
            int temp = _playerPartyRef[0];
            _playerPartyRef[0] = _playerPartyRef[slot];
            _playerPartyRef[slot] = temp;

            // update change to UI
            _currentPlayerPetrmon = _playerParty.Party[_playerPartyRef[0]];
            _currentPlayerPetrImage.sprite = _currentPlayerPetrmon.Sprite;
            UpdateMoves();

            _battlePrompts.DisplaySentOutPetrmonText("You", _currentPlayerPetrmon.name);
            yield return WaitSeconds(1.5f);

            _playerPetrAnim.SetTrigger("spawn");
            yield return WaitSeconds(1.5f);

            // resume battle
            yield return OpponentMoveOnPlayer();

            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
        }

        private void BattleRoutine(Move playerMoveToOpponent)
        {
            _menuPanel.gameObject.SetActive(false);
            _fightPanel.gameObject.SetActive(false);

            StartCoroutine(BattleSequence(playerMoveToOpponent));
        }

        private IEnumerator BattleSequence(Move move)
        {
            // determine who goes first
            if (_currentPlayerPetrmon.BaseSpeed >= _currentOpponentPetrmon.BaseSpeed)
            {
                yield return PlayerMoveOnOpponent(move);
                yield return OpponentMoveOnPlayer();
            }
            else 
            {
                yield return OpponentMoveOnPlayer();
                yield return PlayerMoveOnOpponent(move);
            }

            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
        }

        private IEnumerator PlayerMoveOnOpponent(Move move)
        {
            _battlePrompts.DisplayMoveUsedText(_currentPlayerPetrmon.Name, move.MoveName);

            yield return WaitSeconds(1.5f);

            // move animations here
            _opponentPetrAnim.SetTrigger("tookDamage");

            string battleText = move.Execute(_currentPlayerPetrmon, _currentOpponentPetrmon);

            UpdateCurrentPetrPanels();

            yield return WaitSeconds(1.5f);

            if(_currentOpponentPetrmon.CurrentHP <= 0)
            {
                // opponent faint animations here
                _opponentPetrAnim.SetTrigger("dies");

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
            // start happy win music here
            AudioManager.Instance.StopClip(_battleBGMSound);
            AudioManager.Instance.PlayClip(_playerWinSound, false, true, MainMenuSettings.VolumeSetting);
            _battlePrompts.DisplayExpGainText(_currentPlayerPetrmon.Name);
            yield return WaitSeconds(4f);

            ExitBattle();
        }

        private IEnumerator OpponentMoveOnPlayer()
        {
            _battlePrompts.DisplayMoveUsedText(_currentOpponentPetrmon.Name, _currentOpponentPetrmon.MoveSet.Set[0].MoveName);

            yield return WaitSeconds(1.5f);

            // move animations here
            _playerPetrAnim.SetTrigger("tookDamage");

            string battleText = _currentOpponentPetrmon.MoveSet.Set[0].Execute(_currentOpponentPetrmon, _currentPlayerPetrmon);

            UpdateCurrentPetrPanels();

            yield return WaitSeconds(1.5f);

            if(_currentPlayerPetrmon.CurrentHP <= 0)
            {
                // player petr faint animations here
                _playerPetrAnim.SetTrigger("dies");

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
            // loss game feel (if there is any) here
            AudioManager.Instance.StopClip(_battleBGMSound);
            AudioManager.Instance.PlayClip(_playerLoseSound, false, true, MainMenuSettings.VolumeSetting);
            _battlePrompts.DisplayCustomText("Better luck <br>next time!");
            yield return WaitSeconds(4f);

            ExitBattle();
        }

        private void UpdateCurrentPetrPanels()
        {
            _playerPetrPanel.UpdatePanel(_currentPlayerPetrmon);
            _opponentPetrPanel.UpdatePanel(_currentOpponentPetrmon);
        }

        private void ShowBattleCanvas(bool var)
        {
            _battleCanvas.gameObject.SetActive(var);
        }

        public void PlayButtonClickSound()
        {
            AudioManager.Instance.PlayClip(_buttonClickSound, false, true, MainMenuSettings.VolumeSetting);
        }
    }
}