using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectPetrmon
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public event Action OnDialogueStart;
        public event Action OnDialogueEnd;

        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private int _charLimit;
        [SerializeField] private Canvas _canvas;

        private DialogueObject _currentDialogue;
        private DefaultInputActions _input;
        private PartyObject _opponentParty;
        private bool _inDialogue;
        private bool _isTyping;
        private int _dialogueIndex;

        protected override void Awake()
        {
            _input = new DefaultInputActions();
            _input.Player.NextEntry.started += NextEntry;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Start()
        {
            CloseDialogue();
        }

        private void NextEntry(InputAction.CallbackContext context)
        {
            if (!_inDialogue) return;
            if (_isTyping) return;

            if(_dialogueIndex == _currentDialogue.Lines.Count - 1)
            {
                CloseDialogue();
                return;
            }

            _dialogueIndex++;
            PopulateText(_currentDialogue.Lines[_dialogueIndex]);
        }

        public void StartDialogue(DialogueObject dialogue, PartyObject opponentParty = null)
        {
            if (_inDialogue) return;

            OnDialogueStart?.Invoke();

            _opponentParty = opponentParty;
            _currentDialogue = dialogue;
            _dialogueIndex = 0;
            _inDialogue = true;

            PopulateText(_currentDialogue.Lines[_dialogueIndex]);
            ShowDialogue();
        }

        private void PopulateText(string text)
        {
            StartCoroutine(TypeText(text));
        }

        private IEnumerator TypeText(string text)
        {
            _isTyping = true;
            _dialogueText.text = String.Empty;

            if (text.Length > _charLimit)
                text = text.Substring(0, _charLimit); //if the text goes over, it just gets cut off.

            foreach (char chr in text)
            {
                _dialogueText.text += chr;
                yield return new WaitForSeconds(0.025f);
            }

            _isTyping = false;
        }

        private void ShowDialogue()
        {
            _canvas.enabled = true;
        }

        private void CloseDialogue()
        {
            _canvas.enabled = false;
            
            if(_opponentParty != null)
            {
                BattleManager.Instance.StartBattle(_opponentParty);
            }
            else
            {
                OnDialogueEnd?.Invoke();
            }

            _inDialogue = false;
        }
    }
}
