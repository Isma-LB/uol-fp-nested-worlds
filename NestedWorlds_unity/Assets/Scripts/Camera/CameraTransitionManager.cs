using UnityEngine;
using Unity.Cinemachine;
using IsmaLB.Levels;
using System;
using System.Collections;

namespace IsmaLB.Cameras
{
    public class CameraTransitionManager : MonoBehaviour
    {
        [SerializeField] Camera main3dCamera;
        [SerializeField] CinemachineBrain main3dCameraBrain;
        [SerializeField] CinemachineCamera mainVirtualCamera;
        [SerializeField] Animator transitionAnimator;
        [SerializeField] float fadeOutAnimationTime = 0.5f;

        [Header("Listens On")]
        [SerializeField] TransitionEventSO loadTransitionEvent;
        [SerializeField] TransitionEventSO unloadTransitionEvent;

        void OnEnable()
        {
            loadTransitionEvent.OnTransitionStarted += LevelLoadStarted;
            loadTransitionEvent.OnTransitionEnded += LevelLoadEnded;

            unloadTransitionEvent.OnTransitionStarted += LevelUnloadStarted;
            unloadTransitionEvent.OnTransitionEnded += LevelUnloadEnded;
        }


        void OnDisable()
        {
            loadTransitionEvent.OnTransitionEnded -= LevelLoadEnded;
            loadTransitionEvent.OnTransitionStarted -= LevelLoadStarted;

            unloadTransitionEvent.OnTransitionStarted -= LevelUnloadStarted;
            unloadTransitionEvent.OnTransitionEnded -= LevelUnloadEnded;
        }

        private void LevelUnloadEnded()
        {
            FadeIn();
            SwitchToExplorationCamera();
        }
        private void LevelUnloadStarted()
        {
            StartCoroutine(FadeOut());
        }

        private void LevelLoadStarted()
        {
            StartCoroutine(FadeOut(loadTransitionEvent.Duration - fadeOutAnimationTime));
        }
        private void LevelLoadEnded()
        {
            FadeIn();
            SwitchToPuzzleCamera();
        }
        void SwitchToPuzzleCamera()
        {
            main3dCamera.enabled = false;
        }
        void SwitchToExplorationCamera()
        {
            main3dCamera.enabled = true;

            CinemachineCamera currentVirtualCamera = main3dCameraBrain.ActiveVirtualCamera as CinemachineCamera;
            if (currentVirtualCamera != mainVirtualCamera)
            {
                currentVirtualCamera.gameObject.SetActive(false);
            }
        }
        void FadeIn()
        {
            transitionAnimator.SetBool("FadeOut", false);
        }
        IEnumerator FadeOut(float delay = 0)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            transitionAnimator.SetBool("FadeOut", true);
            yield return null;
        }
    }
}
