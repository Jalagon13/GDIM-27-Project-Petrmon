using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ProjectPetrmon
{
    public class CreditsMenu : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private List<GameObject> creditsGameObjects;
        [SerializeField] private List<GameObject> postCreditButtons;
        [SerializeField] private AudioClip creditsBGM;

        private bool _canClickToSkip = true;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AudioManager.Instance.PlayClip(creditsBGM, true, false, GlobalSettings.MusicSetting);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && _canClickToSkip)
            {
                _canClickToSkip = false;
                _animator.enabled = false;
                foreach (var go in creditsGameObjects) go.SetActive(false);
                EnableMenuButton();
            }
        }

        public void EnableMenuButton()
        {
            foreach (var button in postCreditButtons) button.SetActive(true);
        }

        public void LoadMainMenu()
        {
            AudioManager.Instance.StopClip(creditsBGM);
            Debug.Log("Loading Main Menu");
            SceneManager.LoadScene(mainMenuSceneName);
        }
        
        public void QuitGame()
        {
            Debug.Log("Quit Game Button Pressed");
            Application.Quit();
        }

    }
}
