using System;
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
            AudioManager.Instance.PlayClip(_dialogueSound, false, true, MainMenuSettings.VolumeSetting);
        }

        public void DisplayCustomText(string customText)
        {
            _battleText.text = customText;
            if (!customText.Equals(string.Empty)) PlayDialogueSound();
        }

        public void DisplayExpGainText(string petrmonName)
        {
            _battleText.text = $"{petrmonName.ToUpper()} gained <br>EXP. Points!";
            PlayDialogueSound();
        }

        public void DisplayWhatWillPetrmonDoText(string petrmonName)
        {
            _battleText.text = $"What will<br>{petrmonName.ToUpper()} do?";
            PlayDialogueSound();
        }

        public void DisplayMoveUsedText(string petrmonName, string moveName)
        {
            _battleText.text = $"{petrmonName.ToUpper()} used <br>{moveName}!";
            PlayDialogueSound();
        }

        public void DisplayFaintedText(string petrmonName)
        {
            _battleText.text = $"{petrmonName.ToUpper()} <br>fainted!";
            PlayDialogueSound();
        }

        public void DisplayStatRoseText(string petrmonName, string statName)
        {
            _battleText.text = $"{petrmonName.ToUpper()}'s {statName} <br>rose!";
            PlayDialogueSound();
        }

        public void DisplayStatFellText(string petrmonName, string statName)
        {
            _battleText.text = $"{petrmonName.ToUpper()}'s {statName} <br>fell!";
            PlayDialogueSound();
        }

        public void DisplayWildPetrmonAppearedText(string petrmonName)
        {
            _battleText.text = $"Wild {petrmonName.ToUpper()} appeared!";
            PlayDialogueSound();
        }

        public void DisplayGoPetrmonText(string petrmonName)
        {
            _battleText.text = $"Go! {petrmonName.ToUpper()}!";
            PlayDialogueSound();
        }
    }
}
