using System;
using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    public class TransitionSoundEffects : MonoBehaviour
    {
        [SerializeField] AudioResource puzzleInSFX;
        [SerializeField] AudioResource puzzleOutSFX;
        [SerializeField] AudioResource planetChangedSFX;
        [Header("Listens On")]
        [SerializeField] TransitionEventSO loadTransitionEvent;
        [SerializeField] TransitionEventSO unloadTransitionEvent;
        [SerializeField] PlanetEventSO planetChangedEvent;

        void OnEnable()
        {
            loadTransitionEvent.OnTransitionStarted += PlayPuzzleInSFX;
            unloadTransitionEvent.OnTransitionStarted += PlayPuzzleOutSFX;
            planetChangedEvent.OnEvent += PlayPlanetTransitionSFX;
        }

        void OnDisable()
        {
            loadTransitionEvent.OnTransitionStarted -= PlayPuzzleInSFX;
            unloadTransitionEvent.OnTransitionStarted -= PlayPuzzleOutSFX;
            planetChangedEvent.OnEvent -= PlayPlanetTransitionSFX;
        }
        private void PlayPuzzleInSFX() => AudioManager.PlaySFX(puzzleInSFX, transform.position);
        private void PlayPuzzleOutSFX() => AudioManager.PlaySFX(puzzleOutSFX, transform.position);
        private void PlayPlanetTransitionSFX(Vector3 planetPos) => AudioManager.PlaySFX(planetChangedSFX, planetPos);

    }
}
