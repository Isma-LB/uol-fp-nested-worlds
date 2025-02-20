using System.Collections;
using IsmaLB.Audio;
using UnityEngine;
using UnityEngine.Events;

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

        static AudioManager instance;

        public static void QueueMusicTrack(MusicTrackType type)
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
    }
}
