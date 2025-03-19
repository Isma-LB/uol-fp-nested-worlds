using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    public abstract class EventSO : ScriptableObject
    {
        public event UnityAction OnEvent;
        protected void RaiseOnEvent()
        {
            OnEvent?.Invoke();
        }
    }
}
