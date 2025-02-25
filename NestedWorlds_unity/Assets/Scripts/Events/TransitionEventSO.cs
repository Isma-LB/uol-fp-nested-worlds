using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "TransitionEventSO", menuName = "Scriptable Objects/TransitionEventSO")]
    public class TransitionEventSO : ScriptableObject
    {
        [SerializeField, Min(0)] float duration;
        public event UnityAction OnTransitionStarted;
        public event UnityAction OnTransitionEnded;
        public float Duration => duration;

        public void RaiseStartTransition()
        {
            OnTransitionStarted?.Invoke();
        }
        public void RaiseEndTransition()
        {
            OnTransitionEnded?.Invoke();
        }
    }
}
