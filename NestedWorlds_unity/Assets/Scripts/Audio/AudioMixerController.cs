using System;
using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    public class AudioMixerController : MonoBehaviour
    {
        [SerializeField] AudioMixer mainAudioMixer;

        [Header("Uses")]
        [SerializeField] FloatVariableEventSO musicVolumeVariable;
        [SerializeField] FloatVariableEventSO sfxVolumeVariable;

        const string MUSIC_VOL_KEY = "music_vol";
        const string SFX_VOL_KEY = "sfx_vol";

        void OnEnable()
        {
            musicVolumeVariable.OnValueChanged += SetMusicVolume;
            sfxVolumeVariable.OnValueChanged += SetSoundFXVolume;
        }

        void Start()
        {
            LoadSavedValues();
        }
        void OnDisable()
        {
            musicVolumeVariable.OnValueChanged -= SetMusicVolume;
            sfxVolumeVariable.OnValueChanged -= SetSoundFXVolume;
        }

        void SetMusicVolume(float value)
        {
            Debug.Log("set music vol: " + value);
            float vol = CalculateDB(value);
            mainAudioMixer.SetFloat("MusicVolume", vol);
            // Persist value
            PlayerPrefs.SetFloat(MUSIC_VOL_KEY, value);
        }
        void SetSoundFXVolume(float value)
        {
            float vol = CalculateDB(value);
            mainAudioMixer.SetFloat("SFXVolume", vol);
            // Persist value
            PlayerPrefs.SetFloat(SFX_VOL_KEY, value);
        }
        private float CalculateDB(float value)
        {
            // limit value to be above zero as log10 of 0 is undefined
            if (value <= 0) value = 0.0001f;
            // Formula from 0-1 to DB -80 - 0
            return Mathf.Log10(value) * 20f;
        }
        private void LoadSavedValues()
        {
            if (PlayerPrefs.HasKey(MUSIC_VOL_KEY))
            {
                float musicVol = PlayerPrefs.GetFloat(MUSIC_VOL_KEY);
                musicVolumeVariable.SetValue(musicVol);
                Debug.Log("loaded music vol: " + musicVol);
            }
            if (PlayerPrefs.HasKey(SFX_VOL_KEY))
            {
                float sxfVol = PlayerPrefs.GetFloat(SFX_VOL_KEY);
                sfxVolumeVariable.SetValue(sxfVol);
            }
        }
    }
}
