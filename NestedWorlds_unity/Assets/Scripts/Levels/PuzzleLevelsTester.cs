using System;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Levels
{
    public class PuzzleLevelsTester : MonoBehaviour
    {
        [Header("Listens on")]
        [SerializeField] LevelEventSO levelCompletedEvent;
        [Header("Broadcast on")]
        [SerializeField] LevelEventSO loadLevelEvent;
        [Header("Levels to test")]
        [SerializeField] List<LevelSO> testLevels;
        [SerializeField] int startLevel = 0;

        int currentLevel;
        void OnEnable()
        {
            levelCompletedEvent.onLevelEvent += HandleCompletedLevel;
        }
        void OnDisable()
        {
            levelCompletedEvent.onLevelEvent -= HandleCompletedLevel;
        }

        private void HandleCompletedLevel(LevelSO sO)
        {
            currentLevel++;
            if (currentLevel >= testLevels.Count)
            {
                Debug.Log("Test levels completed");
            }
            else
            {
                Invoke(nameof(LoadCurrentLevel), 0.01f);
            }
        }

        void Start()
        {
            currentLevel = startLevel;
            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            if (0 <= startLevel && startLevel < testLevels.Count)
            {
                loadLevelEvent.Raise(testLevels[currentLevel]);
            }
        }
    }
}
