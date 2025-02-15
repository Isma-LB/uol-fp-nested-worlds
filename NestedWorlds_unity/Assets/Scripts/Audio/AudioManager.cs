using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    public enum MusicTrack { Exploration, Puzzle, Menu }
    public class AudioManager : MonoBehaviour
    {
        [Header("BG music")]
        [SerializeField] AudioSource menuMusicAudioSource;
        [SerializeField] AudioSource puzzleMusicAudioSource;
        [SerializeField] AudioSource explorationMusicAudioSource;
        [SerializeField] FadeSettingsSO musicFadeIn;
        [SerializeField] FadeSettingsSO musicFadeOut;
        AudioSource current;
        AudioSource next = null;

        static AudioManager instance;

        public static void QueueMusicTrack(MusicTrack type)
        {
            if (instance) instance.PlayMusic(type);
        }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
        void Start()
        {
            PlayMusic(MusicTrack.Menu);

        }
        public void PlayMenu() => PlayMusic(MusicTrack.Menu);
        public void PlayExploration() => PlayMusic(MusicTrack.Exploration);
        public void PlayPuzzle() => PlayMusic(MusicTrack.Puzzle);
        void PlayMusic(MusicTrack type)
        {
            if (current)
                StartCoroutine(FadeMusic(current, musicFadeOut));

            switch (type)
            {
                case MusicTrack.Menu:
                    next = menuMusicAudioSource;
                    break;
                case MusicTrack.Exploration:
                    next = explorationMusicAudioSource;
                    break;
                case MusicTrack.Puzzle:
                    next = puzzleMusicAudioSource;
                    break;

            }
            if (next)
                StartCoroutine(FadeMusic(next, musicFadeIn, () => current = next));
        }

        IEnumerator FadeMusic(AudioSource targetAudioSource, FadeSettingsSO fade, UnityAction onCompletedCallback = null)
        {
            bool wasPlaying = targetAudioSource.isPlaying;
            if (wasPlaying == false)
            {
                targetAudioSource.Play();
            }
            // set value to evaluated value
            float t = 0;
            while (t < 1)
            {

                t += Time.deltaTime * fade.Speed;
                targetAudioSource.volume = Mathf.Clamp01(fade.EvaluateEase(t));
                yield return null;
            }
            if (wasPlaying)
            {
                targetAudioSource.Pause();
            }
            onCompletedCallback?.Invoke();
        }
    }
}
