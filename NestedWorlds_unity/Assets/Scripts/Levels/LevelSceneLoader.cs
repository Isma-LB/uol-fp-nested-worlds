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
        bool isReloadingLevel = false;
        bool isInTransition = false;

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
            if (isReloadingLevel == true) return;
            StartCoroutine(RestartCurrentLevel());
        }

        private IEnumerator RestartCurrentLevel()
        {
            isReloadingLevel = true;
            LevelSO levelToReload = currentLevel;
            yield return SceneManager.LoadSceneAsync(levelToReload.Scene.BuildIndex, LoadSceneMode.Additive);
            yield return SceneManager.UnloadSceneAsync(levelToReload.Scene.BuildIndex);
            isReloadingLevel = false;
        }

        private void HandleLevelCompleted()
        {
            levelCompletedEvent.Raise(currentLevel);
            StartCoroutine(UnloadCurrentLevel());
        }

        private void HandlePuzzleQuit()
        {
            if (isInTransition) return;
            StartCoroutine(UnloadCurrentLevel());
        }

        private void LoadLevel(LevelSO level)
        {
            if (isInTransition) return;
            StartCoroutine(LoadCurrentLevel(level));
        }
        IEnumerator UnloadCurrentLevel()
        {
            if (currentLevel == null) yield break;
            LevelSO levelToUnload = currentLevel;
            currentLevel = null;

            // start transition
            isInTransition = true;
            unloadTransitionEvent.RaiseStartTransition();
            AudioManager.QueueMusicTrack(MusicTrackType.Exploration);
            // wait for the transition to complete 
            yield return new WaitForSeconds(unloadTransitionEvent.Duration);
            // unload level scene
            SceneManager.UnloadSceneAsync(levelToUnload.Scene.BuildIndex);
            unloadTransitionEvent.RaiseEndTransition();
            isInTransition = false;
        }

        IEnumerator LoadCurrentLevel(LevelSO levelToLoad)
        {
            if (currentLevel != null) yield break;
            currentLevel = levelToLoad;
            // Start transition
            isInTransition = true;
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
            isInTransition = false;
        }
    }
}
