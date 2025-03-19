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
        [Header("Listens on:")]
        [SerializeField] InputReader inputReader;
        [SerializeField] TransitionEventSO loadPuzzleEvent;
        [SerializeField] TransitionEventSO unloadPuzzleEvent;
        void OnEnable()
        {
            inputReader.pauseMenuEvent += OpenPauseScreen;
            loadPuzzleEvent.OnTransitionStarted += EnablePuzzleUI;
            unloadPuzzleEvent.OnTransitionEnded += EnableExplorationUI;

        }
        void OnDisable()
        {
            inputReader.pauseMenuEvent -= OpenPauseScreen;
            loadPuzzleEvent.OnTransitionStarted -= EnablePuzzleUI;
            unloadPuzzleEvent.OnTransitionEnded -= EnableExplorationUI;
        }

        private void EnablePuzzleUI()
        {
            explorationUI.Close();
        }
        private void EnableExplorationUI()
        {
            explorationUI.Open();
        }

        public void OpenPauseScreen()
        {
            pauseScreenController.Open(OnPauseScreenClose);
            Time.timeScale = 0;
            // set pause state
        }
        private void OnPauseScreenClose()
        {
            Time.timeScale = 1;
            // return to previous state
        }
    }
}
