using System;
using IsmaLB.Gameplay;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        [SerializeField] PauseScreenController pauseScreenController;
        void OnEnable()
        {
            inputReader.pauseMenuEvent += OpenPauseScreen;
        }
        void OnDisable()
        {
            inputReader.pauseMenuEvent -= OpenPauseScreen;
        }

        private void OpenPauseScreen()
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
