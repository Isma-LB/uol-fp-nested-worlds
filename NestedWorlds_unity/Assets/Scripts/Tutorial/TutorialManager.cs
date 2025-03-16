using UnityEngine;

namespace IsmaLB
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
                steps[i].isActive = false;
                steps[i].Init();
            }
        }

        void Update()
        {
            for (int i = currentStep; i < steps.Length; i++)
            {
                if (i == currentStep)
                {
                    steps[i].isActive = true;
                    if (steps[i].Evaluate())
                    {
                        steps[currentStep].isActive = false;
                        currentStep++;
                        Debug.Log("Next step");
                    }
                }
                else
                {
                    steps[i].isActive = false;
                }
            }
        }
    }
}
