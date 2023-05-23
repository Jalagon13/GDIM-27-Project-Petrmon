using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ProjectPetrmon
{
    public class BattlePrompts : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _battleText;
        [SerializeField] private AudioClip _dialogueSound;

        private void Awake()
        {
            _battleText.text = string.Empty;
        }

        private void PlayDialogueSound()
        {
            AudioManager.Instance.PlayClip(_dialogueSound, false, true, GlobalSettings.VolumeSetting);
        }

        public void DisplayCustomText(string customText)
        {
            StartCoroutine(TypeText(customText));
            if (!customText.Equals(string.Empty)) PlayDialogueSound();
        }

        public void DisplayExpGainText(string petrmonName)
        {
            StartCoroutine(TypeText($"{petrmonName.ToUpper()} gained no EXP. Points!"));
            PlayDialogueSound();
        }

        public void DisplayWhatWillPetrmonDoText(string petrmonName)
        {
            StartCoroutine(TypeText($"What will {petrmonName.ToUpper()} do?"));
            PlayDialogueSound();
        }

        public void DisplayMoveUsedText(string petrmonName, string moveName)
        {
            StartCoroutine(TypeText($"{petrmonName.ToUpper()} used {moveName}!"));
            PlayDialogueSound();
        }

        public void DisplayFaintedText(string petrmonName)
        {
            StartCoroutine(TypeText($"{petrmonName.ToUpper()} fainted!"));
            PlayDialogueSound();
        }

        public void DisplayStatRoseText(string petrmonName, string statName)
        {
            _battleText.text = $"{petrmonName.ToUpper()}'s {statName} rose!";
            StartCoroutine(TypeText($"{petrmonName.ToUpper()}'s {statName} fell!"));
            PlayDialogueSound();
        }

        public void DisplayStatFellText(string petrmonName, string statName)
        {
            StartCoroutine(TypeText($"{petrmonName.ToUpper()}'s {statName} fell!"));
            PlayDialogueSound();
        }

        public void DisplayWildPetrmonAppearedText(string petrmonName)
        {
            StartCoroutine(TypeText($"Wild {petrmonName.ToUpper()} appeared!"));
            PlayDialogueSound();
        }

        public void DisplayGoPetrmonText(string petrmonName)
        {
            StartCoroutine(TypeText($"Go! {petrmonName.ToUpper()}!"));
            PlayDialogueSound();
        }
        public void DisplayWithdrawPetrmonText(string trainer, string petrmonName)
        {
            StartCoroutine(TypeText($"{trainer} withdrew {petrmonName.ToUpper()}!"));
            PlayDialogueSound();
        }
        public void DisplaySentOutPetrmonText(string trainer,  string petrmonName)
        {
            StartCoroutine(TypeText($"{trainer} sent out {petrmonName.ToUpper()}!"));
            PlayDialogueSound();
        }

        private IEnumerator TypeText(string text)
        {
            int charLimit = 300;

            _battleText.text = String.Empty;

            if (text.Length > charLimit)
                text = text.Substring(0, charLimit); //if the text goes over, it just gets cut off.

            foreach (char chr in text)
            {
                _battleText.text += chr;
                yield return new WaitForSeconds(0.025f);
            }

        }
    }
}
