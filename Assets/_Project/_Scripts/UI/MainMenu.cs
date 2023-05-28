using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class MainMenu : MonoBehaviour, IPointerClickHandler
    {
        [Header("Main Menu")] [SerializeField] private GameObject mainMenu;
        [SerializeField] private TMP_Text teamName;
        [SerializeField] private float teamNameFadeInAndOutTime;
        [SerializeField] private RawImage gameLogo;
        [SerializeField] private float gameNameFadeInTime;
        [SerializeField] private TMP_Text startText;
        [SerializeField] private float startTextFadeInAndOutTime;
        [SerializeField] private RawImage petrImage;
        [SerializeField] private float petrImageFadeInTime;
        [SerializeField] private Button startButton;
        [SerializeField] private string startingLevelName;
        [SerializeField] private string creditsLevelName;
        [SerializeField] private Image screenCover;
        [SerializeField] private float screenFadeTime;

        [Header("Other Menu Items")] [SerializeField] private GameObject settingsMenu;
        [SerializeField] private GameObject additionalButtons;
        
        [Header("Sounds")] [SerializeField] private AudioClip menuButtonClick;
        [SerializeField] private AudioClip startGameClick;
        [SerializeField] private AudioClip menuBGM;
        [SerializeField] private AudioClip petrRoar;

        private Coroutine startTextCoroutine;
        private bool clickToContinueAvailable;

        // Start is called before the first frame update
        private void Start()
        {
            PauseManager.CanPause = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1;
            Debug.Log("MainMenu Start() callback - Time.timeScale value: " + Time.timeScale);
            StopAllCoroutines();
            StartCoroutine(StartSequence());
        }

        private IEnumerator StartSequence()
        {
            yield return StartCoroutine(DisplayTeamName());
            yield return StartCoroutine(DisplayStartMenu());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && clickToContinueAvailable)
            {
                PlayMenuClickSound();
                clickToContinueAvailable = false;
                StopCoroutine(startTextCoroutine);
                StartCoroutine(DisplaySettingsMenu());
            }
        }

        private IEnumerator DisplayTeamName()
        {
            Debug.Log("display team name");
            yield return StartCoroutine(FadeTextInAndOut(teamName, teamNameFadeInAndOutTime, false));
        }

        private IEnumerator DisplayStartMenu()
        {
            AudioManager.Instance.PlayClip(petrRoar, false, false, GlobalSettings.VolumeSetting);
            yield return new WaitForSeconds(petrRoar.length - petrImageFadeInTime);
            StartCoroutine(FadeTextIn(petrImage, petrImageFadeInTime));
            yield return new WaitForSeconds(petrImageFadeInTime/2);
            AudioManager.Instance.PlayClip(menuBGM, true, false, GlobalSettings.MusicSetting);
            StartCoroutine(FadeTextIn(gameLogo, gameNameFadeInTime));
            startTextCoroutine = StartCoroutine(FadeTextInAndOut(startText, startTextFadeInAndOutTime, true));
            clickToContinueAvailable = true;
            yield break;
        }

        private IEnumerator DisplaySettingsMenu()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
            additionalButtons.SetActive(true);
            yield break;
        }

        private IEnumerator FadeTextIn(MaskableGraphic text, float totalFadeTime)
        {
            Color colorOpaque = new Color(text.color.r, text.color.g, text.color.b, 1);
            Color colorTransparent = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return StartCoroutine(FadeGraphicColor(text, colorTransparent, colorOpaque, totalFadeTime));
        }

        private IEnumerator FadeTextInAndOut(TMP_Text text, float totalFadeTime, bool repeat)
        {
            Debug.Log("Fade Text In and Out");
            Color colorOpaque = new Color(text.color.r, text.color.g, text.color.b, 1);
            Color colorTransparent = new Color(text.color.r, text.color.g, text.color.b, 0);
            do
            {
                yield return StartCoroutine(FadeGraphicColor(text, colorTransparent, colorOpaque, totalFadeTime / 2));
                yield return new WaitForSeconds(0.25f);
                yield return StartCoroutine(FadeGraphicColor(text, colorOpaque, colorTransparent, totalFadeTime / 2));
            } while (repeat);
        }

        private IEnumerator FadeGraphicColor(MaskableGraphic graphic, Color start, Color end, float totalTime)
        {
            Debug.Log("Fade Graphic Color");
            graphic.color = start;
            float time = 0;
            Debug.Log(time + " " + totalTime);
            while (time < totalTime)
            {
                graphic.color = Color.Lerp(start, end, time);
                yield return null;
                time += Time.deltaTime;
                Debug.Log(time + " " + Time.deltaTime);
            }
            graphic.color = end;
        }

        private void StartGameLevel()
        {
            StartCoroutine(_StartGameLevel());
        }

        private IEnumerator _StartGameLevel()
        {
            AudioManager.Instance.PlayClip(startGameClick, false, false, GlobalSettings.VolumeSetting);
            startButton.interactable = false;
            screenCover.gameObject.SetActive(true);
            yield return StartCoroutine(FadeGraphicColor(screenCover, new Color(0, 0, 0, 0), Color.black,
                screenFadeTime));
            AudioManager.Instance.StopClip(menuBGM);
            PauseManager.CanPause = true;
            StopAllCoroutines();
            SceneManager.LoadScene(startingLevelName);
        }

        public void PlayMenuClickSound()
        {
            AudioManager.Instance.PlayClip(menuButtonClick, false, true, GlobalSettings.VolumeSetting);
        }

        public void LoadCreditsScene()
        {
            AudioManager.Instance.StopClip(menuBGM);
            SceneManager.LoadScene(creditsLevelName);
        }

        public void QuitGame()
        {
            Debug.Log("Quit Game Button Pressed");
            Application.Quit();
        }
    }
}
