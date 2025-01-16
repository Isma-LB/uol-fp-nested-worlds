using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "PuzzleEventSO", menuName = "Scriptable Objects/PuzzleEventSO")]
    public class PuzzleEventSO : ScriptableObject
    {
        public event UnityAction onEvent;
        public void Raise()
        {
            onEvent?.Invoke();
        }
        public bool HasListeners => onEvent != null;
    }
}
