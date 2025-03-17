using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB.Levels
{
    [CreateAssetMenu(fileName = "EnergyNodeSO", menuName = "Scriptable Objects/EnergyNodeSO")]
    public class EnergyNodeSO : ScriptableObject
    {
        [SerializeField] LevelState state = LevelState.Locked;
        public LevelSO level;
        public LevelState State => state;
        public event UnityAction OnStateChanged;
        public void SetState(LevelState newState)
        {
            if (state != newState)
            {
                state = newState;
                OnStateChanged?.Invoke();
            }
        }
    }
}
