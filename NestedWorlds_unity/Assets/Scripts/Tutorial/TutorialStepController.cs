using System;
using System.Collections;
using IsmaLB.UI;
using TMPro;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    public class TutorialStepController : MonoBehaviour
    {
        [SerializeField] TutorialStepSO tutorialStep;
        [SerializeField] UIPanel panel;
        [SerializeField] TextMeshProUGUI tutorialText;
        [SerializeField] GameObject effectObject;

        void Start()
        {
            tutorialText.text = tutorialStep.Text;
            UpdateTutorial();
        }
        void OnEnable()
        {
            tutorialStep.OnStatusChanged += UpdateTutorial;
        }
        void OnDisable()
        {
            tutorialStep.OnStatusChanged -= UpdateTutorial;
        }

        private void UpdateTutorial()
        {
            StopAllCoroutines();
            if (tutorialStep.IsActive)
            {
                StartCoroutine(OpenPanelDelayed(tutorialStep.Delay));
            }
            else
            {
                if (effectObject) effectObject.SetActive(false);
                panel.Close();
            }
        }
        IEnumerator OpenPanelDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (effectObject) effectObject.SetActive(true);
            panel.Open();
        }
    }
}
