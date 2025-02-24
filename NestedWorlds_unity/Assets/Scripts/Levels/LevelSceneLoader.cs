using System.Collections;
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
        [SerializeField] TransitionEventSO loadTransitionEvent;
        [SerializeField] TransitionEventSO unloadTransitionEvent;
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
            StartCoroutine(UnloadCurrentLevel());
        }

        private void HandlePuzzleQuit()
        {
            StartCoroutine(UnloadCurrentLevel());
        }

        private void LoadLevel(LevelSO level)
        {
            StartCoroutine(LoadCurrentLevel(level));
        }
        IEnumerator UnloadCurrentLevel()
        {
            if (currentLevel == null) yield break;
            LevelSO levelToUnload = currentLevel;
            currentLevel = null;

            AudioManager.QueueMusicTrack(MusicTrackType.Exploration);
            unloadTransitionEvent.RaiseStartTransition();
            // wait for the transition to complete 
            yield return new WaitForSeconds(unloadTransitionEvent.Duration);
            // unload level scene
            SceneManager.UnloadSceneAsync(levelToUnload.Scene.BuildIndex);
            unloadTransitionEvent.RaiseEndTransition();
        }

        IEnumerator LoadCurrentLevel(LevelSO levelToLoad)
        {
            currentLevel = levelToLoad;
            // Start transition
            loadTransitionEvent.RaiseStartTransition();
            AudioManager.QueueMusicTrack(MusicTrackType.Puzzle);
            float loadStartTime = Time.timeSinceLevelLoad;
            // Load scene async
            yield return SceneManager.LoadSceneAsync(currentLevel.Scene.BuildIndex, LoadSceneMode.Additive);
            // wait for the transition time
            float remainingTime = loadTransitionEvent.Duration - (loadStartTime - Time.timeSinceLevelLoad);
            if (remainingTime > 0)
            {
                yield return new WaitForSeconds(remainingTime);
            }
            loadTransitionEvent.RaiseEndTransition();
        }
    }
}
