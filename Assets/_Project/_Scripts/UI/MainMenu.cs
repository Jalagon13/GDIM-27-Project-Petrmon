using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

namespace ProjectPetrmon
{
    public class MainMenu : MonoBehaviour, IPointerClickHandler
    {
        public static float MusicSetting = 1;
        public static float VolumeSetting = 1;
        
        [Header("Main Menu")] [SerializeField] private GameObject mainMenu;
        [SerializeField] private TMP_Text teamName;
        [SerializeField] private float teamNameFadeInAndOutTime;
        [SerializeField] private TMP_Text gameName;
        [SerializeField] private float gameNameFadeInTime;
        [SerializeField] private TMP_Text startText;
        [SerializeField] private float startTextFadeInAndOutTime;

        [Header("Options Menu")] [SerializeField] private GameObject optionsMenu;

        private Coroutine startTextCoroutine;
        private bool clickToContinueAvailable = false;

        // Start is called before the first frame update
        private IEnumerator Start()
        {
            yield return StartCoroutine(DisplayTeamName());
            yield return StartCoroutine(DisplayStartMenu());
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && clickToContinueAvailable)
            {
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
            StartCoroutine(FadeTextIn(gameName, gameNameFadeInTime));
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

        private IEnumerator FadeTextIn(TMP_Text text, float totalFadeTime)
        {
            Color colorOpaque = new Color(text.color.r, text.color.g, text.color.b, 1);
            Color colorTransparent = new Color(text.color.r, text.color.g, text.color.b, 0);
            yield return StartCoroutine(FadeTextColor(text, colorTransparent, colorOpaque, totalFadeTime));
        }

        private IEnumerator FadeTextInAndOut(TMP_Text text, float totalFadeTime, bool repeat)
        {
            Color colorOpaque = new Color(text.color.r, text.color.g, text.color.b, 1);
            Color colorTransparent = new Color(text.color.r, text.color.g, text.color.b, 0);
            do
            {
                yield return StartCoroutine(FadeTextColor(text, colorTransparent, colorOpaque, totalFadeTime / 2));
                yield return new WaitForSeconds(0.25f);
                yield return StartCoroutine(FadeTextColor(text, colorOpaque, colorTransparent, totalFadeTime / 2));
            } while (repeat);
        }

        private IEnumerator FadeTextColor(TMP_Text text, Color start, Color end, float totalTime)
        {
            text.color = start;
            float time = 0;
            while (time < totalTime)
            {
                text.color = Color.Lerp(start, end, time);
                yield return null;
                time += Time.deltaTime;
            }
            text.color = end;
        }

        public void UpdateMusicSlider(float val)
        {
            MainMenu.MusicSetting = val;
            Debug.Log(MainMenu.MusicSetting);
        }

        public void UpdateVolumeSlider(float val)
        {
            MainMenu.VolumeSetting = val;
            Debug.Log(MainMenu.VolumeSetting);
        }
    }
}
