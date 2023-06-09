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
        [SerializeField] private Canvas _transitionCanvas;
        [SerializeField] private Canvas _battleCanvas;
        [Header("Menu Panel Stuff")]
        [SerializeField] private RectTransform _battleAssets;
        [SerializeField] private RectTransform _playerPanel;
        [SerializeField] private RectTransform _opponentPanel;
        [SerializeField] private RectTransform _fightPanel;
        [SerializeField] private RectTransform _menuPanel;
        [SerializeField] private RectTransform _fightButtons;
        [SerializeField] private RectTransform _battleFadeAnimationLeft;
        [SerializeField] private RectTransform _battleFadeAnimationRight;
        [Header("Battle Assets")]
        [SerializeField] private Image _currentPlayerPetrImage;
        [SerializeField] private Image _currentOpponentPetrImage;

        [Header("Sound Assets")]
        [SerializeField] private AudioClip _buttonClickSound;
        [SerializeField] private AudioClip _playerLoseSound;
        [SerializeField] private AudioClip _playerWinSound;
        [SerializeField] private AudioClip _battleStartSound;
        [SerializeField] private AudioClip _battleBGMSound;
        [SerializeField] private AudioClip _worldBGMSound;

        private List<int> _playerPartyRef;
        private string _opponentName;
        private PartyObject _opponentParty;
        private BattlePrompts _battlePrompts;
        private Animator _playerPetrAnim;
        private Animator _opponentPetrAnim;
        private PetrPanel _playerPetrPanel;
        private PetrPanel _opponentPetrPanel;
        private PetrmonObject _currentPlayerPetrmon;
        private PetrmonObject _currentOpponentPetrmon;
        private NPCTrainer _currentNPC;
        private WaitForSeconds _wait;
        private bool _inBattle;

        public bool InBattle { get { return _inBattle; } }


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
            AudioManager.Instance.PlayClip(_worldBGMSound, true, false, GlobalSettings.MusicSetting);
            _transitionCanvas.gameObject.SetActive(false);
            ShowBattleCanvas(false);
        }

        public void StartBattle(NPCTrainer currentNPC) // Hooked up to Start Battle Button
        {
            AudioManager.Instance.StopClip(_worldBGMSound);

            _playerPartyRef = new List<int>() { 0, 1, 2, 3, 4, 5 };
            _currentNPC = currentNPC;
            _opponentParty = currentNPC.NPCParty;
            _opponentName = currentNPC.NPCName;
            _battlePrompts.DisplayCustomText(string.Empty);
            _menuPanel.gameObject.SetActive(true);
            _fightPanel.gameObject.SetActive(false);
            _inBattle = true;

            _currentPlayerPetrmon = _playerParty.Party[0];
            _currentOpponentPetrmon = _opponentParty.Party[0];

            _currentPlayerPetrImage.sprite = _currentPlayerPetrmon.Sprite;
            _currentOpponentPetrImage.sprite = _currentOpponentPetrmon.Sprite;

            _opponentParty.RestoreParty();

             OnBattleStart?.Invoke();

            UpdateMoves();
            InitializePetrmonBattleStats();
            ShowBattleCanvas(true);

            StartCoroutine(BeginningSequence());
        }

        private IEnumerator BeginningSequence()
        {
            _battleAssets.gameObject.SetActive(false);
            _menuPanel.gameObject.SetActive(false);
            _fightPanel.gameObject.SetActive(false);
            _playerPanel.gameObject.SetActive(false);
            _opponentPanel.gameObject.SetActive(false);
            _currentPlayerPetrImage.gameObject.SetActive(false);
            _currentOpponentPetrImage.gameObject.SetActive(false);

            _transitionCanvas.gameObject.SetActive(true);
            _battleFadeAnimationLeft.gameObject.SetActive(true);
            _battleFadeAnimationRight.gameObject.SetActive(true);
            Animator _battleFadeAnimatorLeft = _battleFadeAnimationLeft.GetComponent<Animator>();
            Animator _battleFadeAnimatorRight = _battleFadeAnimationRight.GetComponent<Animator>();
            _battleFadeAnimatorLeft.SetBool("StartTransition", true);
            _battleFadeAnimatorRight.SetBool("StartTransition", true);
            AudioManager.Instance.PlayClip(_battleStartSound, false, false, GlobalSettings.VolumeSetting);
            yield return WaitSeconds(1f);
            _battleFadeAnimatorLeft.SetBool("StartTransition", false);
            _battleFadeAnimatorRight.SetBool("StartTransition", false);

            _battleAssets.gameObject.SetActive(true);

            yield return WaitSeconds(1.4f);

            AudioManager.Instance.PlayClip(_battleBGMSound, true, false, GlobalSettings.MusicSetting);
            _battleFadeAnimationLeft.gameObject.SetActive(false);
            _battleFadeAnimationRight.gameObject.SetActive(false);
            _transitionCanvas.gameObject.SetActive(false);

            yield return WaitSeconds(0.5f);

            if (_opponentName == null)
                _battlePrompts.DisplayWildPetrmonAppearedText(_currentOpponentPetrmon.Name);
            else
            {
                _battlePrompts.DisplayChallengeText(_opponentName);
                yield return WaitSeconds(2.5f);
                _battlePrompts.DisplaySentOutPetrmonText(_opponentName, _currentOpponentPetrmon.Name);
            }

            _opponentPanel.gameObject.SetActive(true);
            _currentOpponentPetrImage.gameObject.SetActive(true);
            _opponentPetrAnim.SetTrigger("spawn");

            yield return WaitSeconds(2.5f);

            _currentPlayerPetrImage.gameObject.SetActive(true);
            _playerPetrAnim.SetTrigger("spawn");
            _battlePrompts.DisplayGoPetrmonText(_currentPlayerPetrmon.Name);

            yield return WaitSeconds(1.5f);

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
            AudioManager.Instance.PlayClip(_worldBGMSound, true, false, GlobalSettings.MusicSetting);
            _inBattle = false;
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
                    if (index < _currentPlayerPetrmon.MoveSet.MoveSetAmount)
                    {
                        var move = _currentPlayerPetrmon.MoveSet.Set[index];
                        fightButton.UpdateFightButton(move, BattleRoutine);
                    }
                    else
                        fightButton.HideButton();
                    index++;
                }
            }

            UpdateCurrentPetrPanels();
        }

        public void UpdateSwappablePetrs(PetrmonSwapButton currentPetrmon, GameObject buttonsParent)
        {
            currentPetrmon.UpdatePetrmonButton(_playerParty.Party[_playerPartyRef[0]]);
            Debug.Log("Starting to populate swap buttons!");
            var index = 1;

            foreach (Transform child in buttonsParent.transform)
            {
                if (child.TryGetComponent(out PetrmonSwapButton petrmonButton))
                {
                    Debug.Log($"Current button index: {index}");

                    if (_playerParty.Party.Count > index)
                    {
                        PetrmonObject temp = _playerParty.Party[_playerPartyRef[index]];
                        petrmonButton.UpdatePetrmonButton(temp);
                        if (temp.CurrentHP <= 0) // don't select if ded
                            child.GetComponent<Button>().interactable = false;
                        else
                            child.GetComponent<Button>().interactable = true;
                        Debug.Log("Button within range; populated.");
                    }
                    else
                    {
                        petrmonButton.BlankButton();
                        child.GetComponent<Button>().interactable = false;
                        Debug.Log("Button outside of range; deactivated.");
                    }
                    index++;
                }
                Debug.Log("Buttons populated!");
            }
        }
        public void SwapRoutine(int petrPartySlot)
        {
            _menuPanel.gameObject.SetActive(false);

            StartCoroutine(SwapSequence(petrPartySlot));
        }

        private IEnumerator SwapSequence(int slot, bool swapAlive = true)
        {
            if (swapAlive)
            {
                _battlePrompts.DisplayWithdrawPetrmonText("You", _currentPlayerPetrmon.Name);
                yield return WaitSeconds(1.5f);

                _playerPetrAnim.SetTrigger("swap");
                yield return WaitSeconds(1.5f);
            }

            // make the swap after petrmon is withdrawn
            int temp = _playerPartyRef[0];
            _playerPartyRef[0] = _playerPartyRef[slot];
            _playerPartyRef[slot] = temp;

            // update change to UI
            _currentPlayerPetrmon = _playerParty.Party[_playerPartyRef[0]];
            _currentPlayerPetrImage.sprite = _currentPlayerPetrmon.Sprite;
            UpdateMoves();

            _battlePrompts.DisplaySentOutPetrmonText("You", _currentPlayerPetrmon.Name);
            yield return WaitSeconds(1.5f);

            _playerPetrAnim.SetTrigger("spawn");
            yield return WaitSeconds(1.5f);

            if (swapAlive)
            {
                // resume battle
                yield return OpponentMoveOnPlayer();
                if (_currentPlayerPetrmon.CurrentHP <= 0)
                    yield return PlayerPetrFaints();

                _menuPanel.gameObject.SetActive(true);
                _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
            }
        }

        private void BattleRoutine(MoveManager playerMoveToOpponent)
        {
            _menuPanel.gameObject.SetActive(false);
            _fightPanel.gameObject.SetActive(false);

            StartCoroutine(BattleSequence(playerMoveToOpponent));
        }

        private IEnumerator BattleSequence(MoveManager move)
        {
            // determine who goes first
            if (_currentPlayerPetrmon.BaseSpeed >= _currentOpponentPetrmon.BaseSpeed)
            {
                yield return PlayerMoveOnOpponent(move);
                if (_currentOpponentPetrmon.CurrentHP <= 0)
                    yield return OpponentPetrFaints();
                else
                {
                    yield return OpponentMoveOnPlayer();
                    if (_currentPlayerPetrmon.CurrentHP <= 0)
                        yield return PlayerPetrFaints();
                }
            }
            else
            {
                yield return OpponentMoveOnPlayer();
                if (_currentPlayerPetrmon.CurrentHP <= 0)
                    yield return PlayerPetrFaints();
                else
                {
                    yield return PlayerMoveOnOpponent(move);
                    if (_currentOpponentPetrmon.CurrentHP <= 0)
                        yield return OpponentPetrFaints();
                }
            }

            _menuPanel.gameObject.SetActive(true);
            _battlePrompts.DisplayWhatWillPetrmonDoText(_currentPlayerPetrmon.Name);
        }

        private IEnumerator PlayerMoveOnOpponent(MoveManager move)
        {
            _battlePrompts.DisplayMoveUsedText(_currentPlayerPetrmon.Name, move.MoveName);

            yield return WaitSeconds(1.5f);

            string battleText = move.Execute(_currentPlayerPetrmon, _currentOpponentPetrmon);
            // move animations here
            _opponentPetrAnim.SetTrigger("tookDamage");

            UpdateCurrentPetrPanels();

            if (battleText != string.Empty)
            {
                _battlePrompts.DisplayCustomText(battleText);
            }
            yield return WaitSeconds(2f);
        }

        private IEnumerator OpponentMoveOnPlayer()
        {
            int move2do = new System.Random().Next(_currentOpponentPetrmon.MoveSet.MoveSetAmount);
            _battlePrompts.DisplayMoveUsedText(_currentOpponentPetrmon.Name, _currentOpponentPetrmon.MoveSet.Set[move2do].MoveName);

            yield return WaitSeconds(1.5f);

            // move animations here
            _playerPetrAnim.SetTrigger("tookDamage");

            string battleText = _currentOpponentPetrmon.MoveSet.Set[move2do].Execute(_currentOpponentPetrmon, _currentPlayerPetrmon);

            UpdateCurrentPetrPanels();

            yield return WaitSeconds(1.5f);

            if (battleText != string.Empty)
            {
                _battlePrompts.DisplayCustomText(battleText);
                yield return WaitSeconds(2f);
            }
        }

        private IEnumerator OpponentPetrFaints()
        {
            // opponent faint animations here
            _opponentPetrAnim.SetTrigger("dies");

            _battlePrompts.DisplayFaintedText(_currentOpponentPetrmon.Name);

            yield return WaitSeconds(2f);

            foreach (PetrmonObject petr in _opponentParty.Party)
            {
                if (petr.CurrentHP > 0)
                {
                    _currentOpponentPetrmon = petr;
                    break;
                }
            }

            if (_currentOpponentPetrmon.CurrentHP <= 0)
                yield return PlayerWins();
            else
            {
                // update change to UI
                _currentOpponentPetrImage.sprite = _currentOpponentPetrmon.Sprite;
                _opponentPetrPanel.UpdatePanel(_currentOpponentPetrmon);

                _battlePrompts.DisplaySentOutPetrmonText(_opponentName, _currentOpponentPetrmon.Name);
                yield return WaitSeconds(2.5f);

                _opponentPetrAnim.SetTrigger("spawn");
                yield return WaitSeconds(1.5f);
            }
        }

        private IEnumerator PlayerPetrFaints()
        {
            // player petr faint animations here
            _playerPetrAnim.SetTrigger("dies");

            _battlePrompts.DisplayFaintedText(_currentPlayerPetrmon.Name);

            yield return WaitSeconds(2f);

            // do swapping here
            for(int i = 1; i < _playerParty.Party.Count; i++)
            {
                if (_playerParty.Party[_playerPartyRef[i]].CurrentHP > 0)
                {
                    yield return SwapSequence(i, false);
                    break;
                }
            }
            // Loses if no petrs left alive
            if (_currentPlayerPetrmon.CurrentHP <= 0)
                yield return PlayerLoses();
        }

        private IEnumerator PlayerWins()
        {
            _battlePrompts.DisplayNoMorePetrText(_opponentName);

            yield return WaitSeconds(2.5f);

            _battlePrompts.DisplayWinText(_opponentName);

            yield return WaitSeconds(3.5f);

            AudioManager.Instance.StopClip(_battleBGMSound);
            AudioManager.Instance.PlayClip(_playerWinSound, false, true, GlobalSettings.VolumeSetting);

            GPAManager.Instance.AwardGPA(_currentNPC.GpaAwarded);
            _battlePrompts.DisplayGpaText(_currentNPC.GpaAwarded);

            yield return WaitSeconds(3.25f);

            _battlePrompts.DisplayCurrentGPA();

            yield return WaitSeconds(2.5f);

            _currentNPC.Defeated = true;
            ExitBattle();
        }

        private IEnumerator PlayerLoses()
        {
            // loss game feel (if there is any) here
            AudioManager.Instance.PlayClip(_playerLoseSound, false, true, GlobalSettings.VolumeSetting);
            _battlePrompts.DisplayNoMorePetrText("You");
            yield return WaitSeconds(2f);
            _battlePrompts.DisplayLoseText(_opponentName);
            yield return WaitSeconds(3f);
            _battlePrompts.DisplayCustomText("Better luck next time!");
            yield return WaitSeconds(4f);
            AudioManager.Instance.StopClip(_battleBGMSound);

            _currentNPC.Defeated = false;
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
            AudioManager.Instance.PlayClip(_buttonClickSound, false, true, GlobalSettings.VolumeSetting);
        }
    }
}