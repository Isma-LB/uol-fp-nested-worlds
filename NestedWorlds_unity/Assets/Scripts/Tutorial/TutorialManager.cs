using System;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] TutorialStepSO[] steps;
        int currentStep = 0;
        const string TUTORIAL_STEP_KEY = "tutorial_step";

        void Start()
        {
            // load the last step stored to resume the tutorial there
            currentStep = PlayerPrefs.GetInt(TUTORIAL_STEP_KEY, 0);
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
                // save updated step
                PlayerPrefs.SetInt(TUTORIAL_STEP_KEY, currentStep);
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
        public void RestartTutorial()
        {
            PlayerPrefs.DeleteKey(TUTORIAL_STEP_KEY);
        }
    }
}
