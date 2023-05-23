using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    // Global settings that can be accessed anywhere
    public struct GlobalSettings
    {
        public static float MusicSetting = 0.65f;
        public static float VolumeSetting = 0.65f;
    }

    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private AudioClip buttonClickSound;

        private void Start()
        {
            musicSlider.value = GlobalSettings.MusicSetting;
            volumeSlider.value = GlobalSettings.VolumeSetting;
        }
        
        public void UpdateMusicSlider(float val)
        {
            GlobalSettings.MusicSetting = val;
            AudioManager.Instance.UpdateAllLoopingClipVolume(GlobalSettings.MusicSetting);
            Debug.Log(GlobalSettings.MusicSetting);
        }

        public void UpdateVolumeSlider(float val)
        {
            GlobalSettings.VolumeSetting = val;
            Debug.Log(GlobalSettings.VolumeSetting);
        }
        
        public void PlayMenuClickSound()
        {
            AudioManager.Instance.PlayClip(buttonClickSound, false, true, GlobalSettings.VolumeSetting);
        }
    }
}
