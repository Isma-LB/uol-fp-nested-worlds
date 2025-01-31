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

        public void Init()
        {
            onLevelCompleted.onLevelEvent += HandleLevelCompleted;
            currentLevel = 0;
            ResetLevels();
        }
        public void Disable()
        {
            onLevelCompleted.onLevelEvent -= HandleLevelCompleted;
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
