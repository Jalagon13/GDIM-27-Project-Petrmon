using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public struct MainMenuSettings
    {
        public static float MusicSetting = 0.5f;
        public static float VolumeSetting = 0.5f;
    }
    
    public class MainMenu : MonoBehaviour, IPointerClickHandler
    {
        [Header("Main Menu")] [SerializeField] private GameObject mainMenu;
        [SerializeField] private TMP_Text teamName;
        [SerializeField] private float teamNameFadeInAndOutTime;
        [SerializeField] private RawImage gameLogo;
        [SerializeField] private float gameNameFadeInTime;
        [SerializeField] private TMP_Text startText;
        [SerializeField] private float startTextFadeInAndOutTime;

        [Header("Options Menu")] [SerializeField] private GameObject optionsMenu;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Button startButton;
        [SerializeField] private string startingLevelName;
        [SerializeField] private string creditsLevelName;
        [SerializeField] private Image screenCover;
        [SerializeField] private float screenFadeTime;

        [Header("Sounds")] [SerializeField] private AudioClip menuButtonClick;
        [SerializeField] private AudioClip startGameClick;
        [SerializeField] private AudioClip menuBGM;

        private Coroutine startTextCoroutine;
        private bool clickToContinueAvailable;

        // Start is called before the first frame update
        private IEnumerator Start()
        {
            musicSlider.value = MainMenuSettings.MusicSetting;
            volumeSlider.value = MainMenuSettings.VolumeSetting;
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
                StartCoroutine(DisplayOptionsMenu());
            }
        }

        private IEnumerator DisplayTeamName()
        {
            yield return StartCoroutine(FadeTextInAndOut(teamName, teamNameFadeInAndOutTime, false));
        }

        private IEnumerator DisplayStartMenu()
        {
            AudioManager.Instance.PlayClip(menuBGM, true, false, MainMenuSettings.MusicSetting);
            StartCoroutine(FadeTextIn(gameLogo, gameNameFadeInTime));
            startTextCoroutine = StartCoroutine(FadeTextInAndOut(startText, startTextFadeInAndOutTime, true));
            clickToContinueAvailable = true;
            yield break;
        }

        private IEnumerator DisplayOptionsMenu()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
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
            graphic.color = start;
            float time = 0;
            while (time < totalTime)
            {
                graphic.color = Color.Lerp(start, end, time);
                yield return null;
                time += Time.deltaTime;
            }
            graphic.color = end;
        }

        public void UpdateMusicSlider(float val)
        {
            MainMenuSettings.MusicSetting = val;
            AudioManager.Instance.UpdateClipVolume(menuBGM, MainMenuSettings.MusicSetting);
            Debug.Log(MainMenuSettings.MusicSetting);
        }

        public void UpdateVolumeSlider(float val)
        {
            MainMenuSettings.VolumeSetting = val;
            Debug.Log(MainMenuSettings.VolumeSetting);
        }
        private void StartGameLevel()
        {
            StartCoroutine(_StartGameLevel());
        }

        private IEnumerator _StartGameLevel()
        {
            AudioManager.Instance.PlayClip(startGameClick, false, false, MainMenuSettings.VolumeSetting);
            startButton.interactable = false;
            screenCover.gameObject.SetActive(true);
            yield return StartCoroutine(FadeGraphicColor(screenCover, new Color(0, 0, 0, 0), Color.black,
                screenFadeTime));
            AudioManager.Instance.StopClip(menuBGM);
            SceneManager.LoadScene(startingLevelName);
        }

        public void PlayMenuClickSound()
        {
            AudioManager.Instance.PlayClip(menuButtonClick, false, true, MainMenuSettings.VolumeSetting);
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
