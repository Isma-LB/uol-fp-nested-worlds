using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "PuzzleProgressionSO", menuName = "Scriptable Objects/PuzzleProgressionSO")]
    public class PuzzleProgressionSO : ScriptableObject
    {

        [SerializeField] List<LevelSO> levels;
        [SerializeField] List<EnergyNodeSO> nodes;
        public List<EnergyNodeSO> orderedNodes { get; private set; } = new();
        [SerializeField] int keepOrderUntilIndex = 2;
        [Header("Listens on")]
        [SerializeField] LevelEventSO onLevelCompleted;
        int currentLevel = 0;

        void OnValidate()
        {
            if (levels.Count > nodes.Count)
            {
                Debug.LogWarning("Please ensure there are as many nodes as levels");
            }
        }

        private void HandleLevelCompleted(LevelSO level)
        {
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
            RandomizeNodes();
            // assign levels
            for (int i = 0; i < levels.Count; i++)
            {
                LevelSO level = levels[i];
                EnergyNodeSO node = orderedNodes[i];
                node.level = level;
                node.SetState(LevelState.Locked);
            }
            // unlock the first node
            orderedNodes[0].SetState(LevelState.Unlocked);
        }

        private void RandomizeNodes()
        {
            orderedNodes = nodes.GetRange(0, keepOrderUntilIndex);
            // randomize remaining nodes
            List<EnergyNodeSO> randomNodes = nodes.GetRange(keepOrderUntilIndex, nodes.Count - keepOrderUntilIndex);
            while (randomNodes.Count > 0)
            {
                int randomIndex = Random.Range(0, randomNodes.Count - 1);
                orderedNodes.Add(randomNodes[randomIndex]);
                randomNodes.RemoveAt(randomIndex);
            }
        }

        private void SetLevelState(int index, LevelState state)
        {
            if (0 <= index && index < orderedNodes.Count)
            {
                orderedNodes[index].SetState(state);
            }
        }
    }
}
