using System;
using IsmaLB.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PuzzleProgressionSO puzzleProgression;
        public const string GAME_COMPLETED_KEY = "GameCompleted";
        void OnEnable()
        {
            PlayerPrefs.SetInt(GAME_COMPLETED_KEY, 0);
            puzzleProgression.Init();
            puzzleProgression.OnProgressionCompleted += OnLevelsCompleted;
            AudioManager.QueueMusicTrack(MusicTrackType.Exploration);
        }
        void OnDisable()
        {
            puzzleProgression.Disable();
            puzzleProgression.OnProgressionCompleted -= OnLevelsCompleted;
        }

        private void OnLevelsCompleted()
        {
            // set the game as completed
            PlayerPrefs.SetInt(GAME_COMPLETED_KEY, 1);
            // load main menu scene
            SceneManager.LoadScene(0);
        }
    }
}
