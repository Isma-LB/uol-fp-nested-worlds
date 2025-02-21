using System;
using UnityEngine;
using UnityEngine.UI;

namespace IsmaLB.UI
{
    public class FloatVariableSlider : MonoBehaviour
    {
        [SerializeField] FloatVariableEventSO variable;
        [SerializeField] Slider slider;
        void OnEnable()
        {
            variable.OnValueChanged += VariableValueChanged;
            slider.onValueChanged.AddListener(SliderValueChanged);
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            slider.SetValueWithoutNotify(variable.Value);
        }

        void OnDisable()
        {
            variable.OnValueChanged -= VariableValueChanged;
            slider.onValueChanged.RemoveListener(SliderValueChanged);
        }

        private void SliderValueChanged(float value)
        {
            variable.SetValue(value);
        }

        private void VariableValueChanged(float value)
        {
            slider.SetValueWithoutNotify(value);
        }
    }
}
