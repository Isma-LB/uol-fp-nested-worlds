using System;
using UnityEngine;

namespace IsmaLB.Input
{
    public class InputModeSwitcher : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        [Header("Reacts to:")]
        [SerializeField] TransitionEventSO puzzleLoadedEvent;
        [SerializeField] TransitionEventSO puzzleUnloadedEvent;

        void OnEnable()
        {
            puzzleLoadedEvent.OnTransitionStarted += HandlePuzzleLoad;
            puzzleUnloadedEvent.OnTransitionStarted += HandlePuzzleUnload;
        }

        void OnDisable()
        {
            puzzleLoadedEvent.OnTransitionStarted -= HandlePuzzleLoad;
            puzzleUnloadedEvent.OnTransitionStarted -= HandlePuzzleUnload;
        }
        void Start()
        {
            inputReader.EnableExplorationInput();

        }

        private void HandlePuzzleUnload()
        {
            Debug.Log("Set puzzle input");
            inputReader.EnableExplorationInput();
        }


        private void HandlePuzzleLoad()
        {
            Debug.Log("Set exploration input");
            inputReader.EnablePuzzleInput();
        }
    }
}
