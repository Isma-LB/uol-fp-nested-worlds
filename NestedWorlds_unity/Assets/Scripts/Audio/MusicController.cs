using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    [System.Serializable]
    public class MusicTrack
    {
        public AudioClip clip;
        float playbackTime = 0;
        public float PlaybackTime => playbackTime;
        public void SetPlaybackTime(AudioSource audioSource)
        {
            if (audioSource.clip != clip) return;
            playbackTime = audioSource.time % clip.length;
        }
    }
    public class MusicController : MonoBehaviour
    {
        [SerializeField] AudioMixerGroup outputGroup;
        [SerializeField] FadeSettingsSO fadeIn;
        [SerializeField] FadeSettingsSO fadeOut;
        AudioSource trackA;
        AudioSource trackB;
        MusicTrack currentTrack;
        bool isTrackAActive = false;
        void Awake()
        {
            trackA = gameObject.AddComponent<AudioSource>();
            trackB = gameObject.AddComponent<AudioSource>();

            trackA.outputAudioMixerGroup = outputGroup;
            trackB.outputAudioMixerGroup = outputGroup;
        }

        public void Play(MusicTrack track)
        {
            if (currentTrack == track) return;
            currentTrack?.SetPlaybackTime(isTrackAActive ? trackA : trackB);
            currentTrack = track;
            StopAllCoroutines();

            if (isTrackAActive)
            {
                StartCoroutine(FadeOut(trackA, track));
                StartCoroutine(FadeIn(trackB, track));
                isTrackAActive = false;
            }
            else
            {
                StartCoroutine(FadeOut(trackB, track));
                StartCoroutine(FadeIn(trackA, track));
                isTrackAActive = true;
            }
        }

        IEnumerator FadeOut(AudioSource audioSource, MusicTrack track)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * fadeOut.Speed;
                float targetVol = fadeOut.EvaluateEase(t);

                // Fade volume out if the target value is smaller than the current
                if (audioSource.volume > targetVol)
                {
                    audioSource.volume = targetVol;
                }

                yield return null;
            }
            audioSource.Stop();
        }
        IEnumerator FadeIn(AudioSource audioSource, MusicTrack track)
        {
            audioSource.clip = track.clip;
            audioSource.volume = 0;
            audioSource.loop = true;
            audioSource.time = track.PlaybackTime;
            audioSource.Play();
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * fadeIn.Speed;
                float targetVol = fadeIn.EvaluateEase(t);

                // Fade volume in if the target value is greater than the current
                if (audioSource.volume < targetVol)
                {
                    audioSource.volume = targetVol;
                }

                yield return null;
            }
        }

    }
}
