using UnityEngine;
using UnityEngine.Events;

namespace IsmaLB
{
    [CreateAssetMenu(fileName = "FloatVariableEventSO", menuName = "Scriptable Objects/FloatVariableEventSO")]
    public class FloatVariableEventSO : ScriptableObject
    {
        public event UnityAction<float> OnValueChanged;
        [SerializeField] float currentValue;
        public float Value => currentValue;
        public void SetValue(float value)
        {
            currentValue = value;
            OnValueChanged?.Invoke(value);
        }
    }
}
