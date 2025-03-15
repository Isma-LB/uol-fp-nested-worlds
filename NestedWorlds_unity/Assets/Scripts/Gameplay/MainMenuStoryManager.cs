using System;
using System.Collections;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB
{
    public class MainMenuStoryManager : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;
        [SerializeField, Range(0, 30)] float storyMaxDuration;
        [SerializeField] Animator planetsAnimator;
        [SerializeField, Range(0, 10)] float planetsAnimationDuration;
        [SerializeField] ParticleSystem startParticleSystem;
        [SerializeField] InputReader inputReader;

        bool shouldContinue = false;

        void OnEnable()
        {
            inputReader.interactEvent += OnInteractInput;
        }
        void OnDisable()
        {
            inputReader.interactEvent -= OnInteractInput;
        }
        void Start()
        {
            storyPanel.SetActive(false);
        }
        public IEnumerator StartStory()
        {
            startParticleSystem.Play();
            planetsAnimator.SetTrigger("CollapsePlanets");
            yield return new WaitForSeconds(planetsAnimationDuration);
            storyPanel.SetActive(true);
            float autoContinueTime = storyMaxDuration;
            while (autoContinueTime > 0)
            {
                yield return null;
                autoContinueTime -= Time.deltaTime;
                if (shouldContinue) yield break;
            }
        }
        private void OnInteractInput()
        {
            Debug.Log("Skip story");
            shouldContinue = true;
        }

    }
}
