using System;
using System.Collections;
using IsmaLB.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.Levels
{
    public class LevelSceneLoader : MonoBehaviour
    {
        [Header("Listens on")]
        [SerializeField] PuzzleEventSO puzzleSolvedEvent;
        [SerializeField] PuzzleEventSO quitPuzzleEvent;
        [SerializeField] PuzzleEventSO restartPuzzleEvent;
        [SerializeField] LevelEventSO loadLevelEvent;
        [Header("Broadcasts on")]
        [SerializeField] LevelEventSO levelCompletedEvent;

        LevelSO currentLevel;

        void OnEnable()
        {
            quitPuzzleEvent.onEvent += HandlePuzzleQuit;
            puzzleSolvedEvent.onEvent += HandleLevelCompleted;
            restartPuzzleEvent.onEvent += HandlePuzzleRestart;
            loadLevelEvent.onLevelEvent += LoadLevel;
        }
        void OnDisable()
        {
            quitPuzzleEvent.onEvent += HandlePuzzleQuit;
            puzzleSolvedEvent.onEvent -= HandleLevelCompleted;
            restartPuzzleEvent.onEvent -= HandlePuzzleRestart;
            loadLevelEvent.onLevelEvent -= LoadLevel;
        }

        private void HandlePuzzleRestart()
        {
            StartCoroutine(RestartCurrentLevel());
        }

        private IEnumerator RestartCurrentLevel()
        {
            yield return SceneManager.UnloadSceneAsync(currentLevel.Scene.BuildIndex);
            LoadLevel(currentLevel);
        }

        private void HandleLevelCompleted()
        {
            levelCompletedEvent.Raise(currentLevel);
            UnloadCurrentLevel();
        }

        private void HandlePuzzleQuit()
        {
            UnloadCurrentLevel();
        }

        private void UnloadCurrentLevel()
        {
            if (currentLevel == null) return;
            SceneManager.UnloadSceneAsync(currentLevel.Scene.BuildIndex);
            currentLevel = null;
        }

        private void LoadLevel(LevelSO level)
        {
            SceneManager.LoadSceneAsync(level.Scene.BuildIndex, LoadSceneMode.Additive);
            currentLevel = level;
        }

    }
}
