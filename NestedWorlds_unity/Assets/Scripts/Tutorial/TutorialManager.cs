using System;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] TutorialStepSO[] steps;
        int currentStep = 0;

        void Start()
        {
            // TODO: save and restore the current step
            for (int i = 0; i < steps.Length; i++)
            {
                steps[i].Init();
                steps[i].SetStatus(false);
            }
            ActivateCurrentStep();
        }

        void Update()
        {
            if (IsValidStep(currentStep) && steps[currentStep].Evaluate())
            {
                steps[currentStep].SetStatus(false);
                currentStep++;
                ActivateCurrentStep();
            }
        }

        private bool IsValidStep(int currentStep) => (0 <= currentStep && currentStep < steps.Length);

        private void ActivateCurrentStep()
        {
            if (IsValidStep(currentStep))
            {
                steps[currentStep].SetStatus(true);
            }
        }
    }
}
