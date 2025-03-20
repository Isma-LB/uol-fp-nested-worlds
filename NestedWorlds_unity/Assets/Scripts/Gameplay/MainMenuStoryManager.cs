using System;
using System.Collections;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB
{
    public class MainMenuStoryManager : MonoBehaviour
    {
        [Header("Manages: ")]
        [SerializeField] GameObject storyPanel;
        [SerializeField] GameObject thanksMessage;
        [SerializeField] Animator planetsAnimator;
        [SerializeField] ParticleSystem startParticleSystem;
        [Header("Intro story timings")]
        [SerializeField, Range(0, 30)] float storyMaxDuration;
        [SerializeField, Range(0, 10)] float planetsAnimationDuration;
        [Header("Game completed timings")]
        [SerializeField, Range(0, 10)] float endAnimationDuration;
        [SerializeField, Range(0, 10)] float thanksMessageDelay;
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
            thanksMessage.SetActive(false);
        }
        public IEnumerator StartStory()
        {
            thanksMessage.SetActive(false);
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
        public IEnumerator ShowStoryEnd()
        {
            planetsAnimator.SetTrigger("GameCompleted");
            yield return new WaitForSeconds(thanksMessageDelay);
            thanksMessage.SetActive(true);
            yield return new WaitForSeconds(endAnimationDuration - thanksMessageDelay);

        }
        private void OnInteractInput()
        {
            Debug.Log("Skip story");
            shouldContinue = true;
        }

    }
}
