using System;
using UnityEngine;
using UnityEngine.Audio;

namespace IsmaLB.Audio
{
    public class PuzzleSoundEffects : MonoBehaviour
    {
        [SerializeField] AudioResource puzzleCompletedSFX;
        [SerializeField] PuzzleEventSO puzzleCompletedEvent;
        void OnEnable()
        {
            puzzleCompletedEvent.onEvent += PlayPuzzleCompletedSFX;
        }
        void OnDisable()
        {
            puzzleCompletedEvent.onEvent -= PlayPuzzleCompletedSFX;
        }

        private void PlayPuzzleCompletedSFX()
        {
            AudioManager.PlaySFX(puzzleCompletedSFX, transform.position);
        }
    }
}
