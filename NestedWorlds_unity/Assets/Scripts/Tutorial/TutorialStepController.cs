using IsmaLB.UI;
using TMPro;
using UnityEngine;

namespace IsmaLB
{
    public class TutorialStepController : MonoBehaviour
    {
        [SerializeField] TutorialStepSO tutorialStep;
        [SerializeField] UIPanel panel;
        [SerializeField] TextMeshProUGUI tutorialText;

        void Start()
        {
            tutorialText.text = tutorialStep.Text;
        }

        void Update()
        {
            if (tutorialStep.isActive)
            {
                panel.Open();
            }
            else
            {
                panel.Close();
            }
        }
    }
}
