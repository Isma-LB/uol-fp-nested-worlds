using Eflatun.SceneReference;
using UnityEditor.SearchService;
using UnityEngine;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] SceneReference puzzleScene;
        [SerializeField] LevelState state = LevelState.Locked;
        public LevelState State => state;
        public SceneReference Scene => puzzleScene;
        public void SetState(LevelState newState)
        {
            state = newState;
        }
    }
}
