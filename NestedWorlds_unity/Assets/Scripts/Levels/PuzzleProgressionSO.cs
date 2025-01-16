using System;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "PuzzleProgressionSO", menuName = "Scriptable Objects/PuzzleProgressionSO")]
    public class PuzzleProgressionSO : ScriptableObject
    {

        [SerializeField] List<LevelSO> levels;
        [Header("Listens on")]
        [SerializeField] LevelEventSO onLevelCompleted;
        int currentLevel = 0;

        void OnEnable()
        {
            Debug.Log("Progress controller enabled");
            Init();
            onLevelCompleted.onLevelEvent += HandleLevelCompleted;
        }
        void OnDisable()
        {
            onLevelCompleted.onLevelEvent -= HandleLevelCompleted;
        }

        private void HandleLevelCompleted(LevelSO level)
        {
            Debug.Log("level completed: " + level.Scene.Name);
            level.SetState(LevelState.Completed);
            // unlock next level
            currentLevel++;
            if (currentLevel < levels.Count)
            {
                levels[currentLevel].SetState(LevelState.Unlocked);
            }
            else
            {
                Debug.Log("All levels completed!");
            }
        }

        private void Init()
        {
            currentLevel = 0;
            ResetLevels();
        }
        private void ResetLevels()
        {
            foreach (LevelSO level in levels)
            {
                level.SetState(LevelState.Locked);
            }
            levels[0].SetState(LevelState.Unlocked);
        }
    }
}
