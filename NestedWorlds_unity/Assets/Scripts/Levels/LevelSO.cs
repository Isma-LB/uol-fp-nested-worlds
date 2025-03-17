using Eflatun.SceneReference;
using UnityEngine;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] SceneReference puzzleScene;
        public SceneReference Scene => puzzleScene;
    }
}
