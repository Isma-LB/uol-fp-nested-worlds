using System;
using System.Collections.Generic;
using IsmaLB.Audio;
using UnityEngine;
using UnityEngine.Audio;
namespace IsmaLB
{
    public enum MusicTrackType { Exploration, Puzzle, Menu }
    public class AudioManager : MonoBehaviour
    {
        [Header("BG music")]
        [SerializeField] MusicController musicController;
        [SerializeField] MusicTrack explorationTrack;
        [SerializeField] MusicTrack puzzleTrack;
        [SerializeField] MusicTrack menuTrack;

        [Header("SXF")]
        [SerializeField] float poolSize;
        [SerializeField] AudioSource sfxPrefab;
        List<AudioSource> audioSourcesPool = new();

        static AudioManager instance;

        public static void QueueMusicTrack(MusicTrackType type)
        {
            if (instance) instance.PlayMusic(type);
        }

        public static void PlaySFX(AudioResource audio, Vector3 position)
        {
            if (instance) instance.PlaySfx(audio, position);
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
        void Start()
        {
            StartPool();
        }
        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        void PlayMusic(MusicTrackType type)
        {
            switch (type)
            {
                case MusicTrackType.Menu:
                    musicController.Play(menuTrack);
                    break;
                case MusicTrackType.Exploration:
                    musicController.Play(explorationTrack);
                    break;
                case MusicTrackType.Puzzle:
                    musicController.Play(puzzleTrack);
                    break;
                default:
                    Debug.LogWarning("PlayMusic: requested track type not found");
                    return;

            }
        }
        private void PlaySfx(AudioResource audio, Vector3 position)
        {
            AudioSource audioSource = GetSFXAudioSource();
            if (audioSource == null)
            {
                Debug.Log("Need more audio sources");
                return;
            }
            audioSource.transform.position = position;
            audioSource.resource = audio;
            audioSource.Play();
        }

        private AudioSource GetSFXAudioSource()
        {
            for (int i = 0; i < audioSourcesPool.Count; i++)
            {
                if (audioSourcesPool[i].isPlaying == false)
                {
                    return audioSourcesPool[i];
                }
            }
            return null;
        }
        void StartPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                AudioSource temp = Instantiate(sfxPrefab, transform);
                temp.playOnAwake = false;
                temp.loop = false;
                temp.gameObject.SetActive(true);
                audioSourcesPool.Add(temp);
            }
        }
    }
}
