using System;
using IsmaLB.Levels;
using UnityEngine;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "LevelEventSO", menuName = "Scriptable Objects/LevelEventSO")]
    public class LevelEventSO : ScriptableObject
    {
        public event Action<LevelSO> onLevelEvent;
        public void Raise(LevelSO level)
        {
            Debug.Log("Event raised for level: " + level.Scene.Name);
            onLevelEvent?.Invoke(level);
        }
    }
}
