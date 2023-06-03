using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectPetrmon
{
    public class PauseManager : MonoBehaviour
    {
        public static bool CanPause;
        public static bool IsPaused { get; private set; }
        public static event Action OnPauseEnable;
        public static event Action OnPauseDisable;

        [SerializeField] private GameObject pauseMenuGameObject;
        [SerializeField] private string mainMenuName;
        private float timeScaleBeforePause = 1;
        private static CursorLockMode _cursorModeBeforePause;

        public static void TogglePause()
        {
            if (!CanPause) return;
            // Disable pause menu if currently paused
            if (IsPaused)
            {
                OnPauseDisable?.Invoke();
                // Reset cursor state, occurs after invoke to avoid conflicting cursor states from subscribed methods
                Cursor.lockState = _cursorModeBeforePause;
            }
            // Enable pause menu if currently unpaused
            else OnPauseEnable?.Invoke();
            // Update current pause status
            IsPaused = !IsPaused;
        }
        
        private void OnEnable()
        {
            OnPauseEnable += EnablePauseMenu;
            OnPauseDisable += DisablePauseMenu;
        }

        private void OnDisable()
        {
            OnPauseEnable -= EnablePauseMenu;
            OnPauseDisable -= DisablePauseMenu;
        }

        private void OnDestroy()
        {
            // When changing scenes, make sure to turn off pause if currently active
            if (IsPaused) TogglePause();
        }

        private void Awake()
        {
            _cursorModeBeforePause = Cursor.lockState;
        }

        private void EnablePauseMenu()
        {
            timeScaleBeforePause = Time.timeScale;
            Debug.Log("EnablePauseMenu() callback - Time Scale b4 pause: " + timeScaleBeforePause);
            _cursorModeBeforePause = Cursor.lockState;
            Time.timeScale = 0;
            pauseMenuGameObject.SetActive(true);
        }
        
        private void DisablePauseMenu()
        {
            pauseMenuGameObject.SetActive(false);
            Time.timeScale = timeScaleBeforePause;
            Debug.Log("DisablePauseMenu() callback - Time Scale after pause: " + Time.timeScale);
        }

        public void Resume()
        {
            TogglePause();
        }

        public void LoadMainMenu()
        {
            /*
             * Loading a different scene does not make the game happy:
             *      (1) Coroutines don't work
             *      (2) 40% of the time the DialogueManager has a NullReferenceException
             * Thus functionality for this has been commented out until a fix is found.
             */
            AudioManager.Instance.StopAllClips();
            SceneManager.LoadScene(mainMenuName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
