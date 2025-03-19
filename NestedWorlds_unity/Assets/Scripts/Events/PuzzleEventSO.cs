using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "PuzzleEventSO", menuName = "Scriptable Objects/PuzzleEventSO")]
    public class PuzzleEventSO : EventSO
    {
        public void Raise()
        {
            base.RaiseOnEvent();
        }
    }
}
