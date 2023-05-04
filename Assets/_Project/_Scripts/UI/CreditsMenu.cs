using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace ProjectPetrmon
{
    public class CreditsMenu : MonoBehaviour
    {
        [SerializeField] private string mainMenuSceneName;
        [SerializeField] private List<GameObject> postCreditButtons;

        public void EnableMenuButton()
        {
            foreach (var button in postCreditButtons) button.SetActive(true);
        }

        public void LoadMainMenu()
        {
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
