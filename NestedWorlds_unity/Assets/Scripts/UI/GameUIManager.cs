using System;
using IsmaLB.Gameplay;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [Header("Manages:")]
        [SerializeField] PauseScreenController pauseScreenController;
        [SerializeField] UIPanel explorationUI;
        [SerializeField] UIPanel puzzleUI;
        [Header("Listens on:")]
        [SerializeField] InputReader inputReader;
        [SerializeField] TransitionEventSO loadPuzzleEvent;
        [SerializeField] TransitionEventSO unloadPuzzleEvent;
        void OnEnable()
        {
            inputReader.pauseMenuEvent += OpenPauseScreen;
            loadPuzzleEvent.OnTransitionStarted += explorationUI.Close;
            loadPuzzleEvent.OnTransitionEnded += puzzleUI.Open;
            unloadPuzzleEvent.OnTransitionStarted += puzzleUI.Close;
            unloadPuzzleEvent.OnTransitionEnded += explorationUI.Open;

        }
        void OnDisable()
        {
            inputReader.pauseMenuEvent -= OpenPauseScreen;
            loadPuzzleEvent.OnTransitionStarted -= explorationUI.Close;
            loadPuzzleEvent.OnTransitionEnded -= puzzleUI.Open;
            unloadPuzzleEvent.OnTransitionStarted -= puzzleUI.Close;
            unloadPuzzleEvent.OnTransitionEnded -= explorationUI.Open;
        }

        public void OpenPauseScreen()
        {
            pauseScreenController.Open(OnPauseScreenClose);
            Time.timeScale = 0;
        }
        private void OnPauseScreenClose()
        {
            Time.timeScale = 1;
        }
    }
}
