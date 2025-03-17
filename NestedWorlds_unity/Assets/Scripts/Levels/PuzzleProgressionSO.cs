// using System;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "PuzzleProgressionSO", menuName = "Scriptable Objects/PuzzleProgressionSO")]
    public class PuzzleProgressionSO : ScriptableObject
    {

        [SerializeField] List<LevelSO> levels;
        [SerializeField] List<EnergyNodeSO> nodes;
        [Header("Listens on")]
        [SerializeField] LevelEventSO onLevelCompleted;
        int currentLevel = 0;

        void OnValidate()
        {
            if (levels.Count != nodes.Count)
            {
                Debug.LogWarning("Please ensure there are as many nodes as levels");
            }
        }

        private void HandleLevelCompleted(LevelSO level)
        {
            Debug.Log("level completed: " + level.Scene.Name);

            SetLevelState(currentLevel, LevelState.Completed);
            // unlock next level
            currentLevel++;
            SetLevelState(currentLevel, LevelState.Unlocked);

            if (currentLevel >= levels.Count)
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
            for (int i = 0; i < levels.Count; i++)
            {
                LevelSO level = levels[i];
                EnergyNodeSO node = nodes[i];
                node.level = level;
                node.SetState(LevelState.Locked);
            }
            // unlock the first node
            nodes[0].SetState(LevelState.Unlocked);
        }
        private void SetLevelState(int index, LevelState state)
        {
            if (0 <= index && index < nodes.Count)
            {
                nodes[index].SetState(state);
            }
        }
    }
}
