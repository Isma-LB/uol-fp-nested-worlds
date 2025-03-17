using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB.Tutorial
{
    public abstract class TutorialStepSO : ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField, Min(0)] float delay;
        public string Text => text;
        public float Delay => delay;
        public bool IsActive => active;
        public event UnityAction OnStatusChanged;
        bool active = false;
        public void SetStatus(bool isActive)
        {
            if (active != isActive)
            {
                active = isActive;
                OnStatusChanged?.Invoke();
            }
        }
        public abstract bool Evaluate();
        public virtual void Init() { }
    }
}
