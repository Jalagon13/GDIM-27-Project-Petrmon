using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using System.Collections;

namespace ProjectPetrmon
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public delegate void StartAction();
        public static event StartAction OnStart;
        public delegate void EndAction();
        public static event EndAction OnIntroEnd;

        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private TMP_Text _characterName;
        //[SerializeField] private Image _displayPic;
        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _signDialogueBox;
        [SerializeField] private GameObject _dialogueDisplay;
        [SerializeField] private AudioClip _textSound;
        [SerializeField] private int _charLimit;

        private GameObject _conversationStarter;
        private DefaultInput _input;
        private bool _isTalking = false;
        private bool _enterPressed;
        private bool _isTyping = false;


        protected override void Awake()
        {
            base.Awake();
            _dialogueDisplay.SetActive(false);

            _input = new DefaultInput();

            _input.Player.NextEntry.started += NextEntry;
        }

        private void NextEntry(InputAction.CallbackContext context)
        {
            _enterPressed = context.ReadValue<float>() > 0;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        public void StartConversation(DialogueObject currentConversation, bool isTriggered, GameObject talker)
        {
            if (!_isTalking)
            {
                _isTalking = true;
                _dialogueDisplay.SetActive(true);

                StartCoroutine(ConversationCo(currentConversation, isTriggered, talker));
            }
        }

        private IEnumerator ConversationCo(DialogueObject currentConversation, bool isTriggered, GameObject talker)
        {
            OnStart?.Invoke();

            foreach (string line in currentConversation.Lines)
            {
                PopulateCurrentEntry(line);
                yield return WaitForPlayerCo();
            }

            _dialogueDisplay.SetActive(false);
            _isTalking = false;
            if (isTriggered)
            {
                talker.SetActive(false);
            }
            OnIntroEnd?.Invoke();
        }

        private void PopulateCurrentEntry(string line)
        {
            StartCoroutine(TypeText(line));
        }

        private IEnumerator WaitForPlayerCo()
        {
            //SoundManager.Instance.PlaySFX(_textSound, 0.2f);
            yield return new WaitForSeconds(0.5f);
            while (!_enterPressed || _isTyping) //there was a glitch where if you pressed enter too soon, it'd skip ahead and intertwine both lines of text LOL. This fixes it.
            {
                yield return null;
            }
        }

        private IEnumerator TypeText(string text)
        {
            _isTyping = true;
            _dialogueText.text = String.Empty;
            if (text.Length > _charLimit)
            {
                text = text.Substring(0, _charLimit); //if the text goes over, it just gets cut off.
            }
            foreach (char chr in text)
            {
                _dialogueText.text += chr;
                yield return new WaitForSeconds(0.025f);
            }
            _isTyping = false;
        }

        public bool IsTalking()
        {
            return _isTalking;
        }
    }
}
