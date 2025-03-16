using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IsmaLB
{
    public abstract class TutorialStepSO : ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField, Min(0)] float delay;
        public string Text => text;
        public float Delay => delay;
        public bool isActive = false;
        public abstract bool Evaluate();
        public virtual void Init() { }
    }
}
