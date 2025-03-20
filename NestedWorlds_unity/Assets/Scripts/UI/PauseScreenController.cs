using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace IsmaLB.UI
{
    public class PauseScreenController : MonoBehaviour
    {
        [SerializeField] UIPanel bgPanel;
        [SerializeField] UIPanel pausePanel;
        [SerializeField] SettingsScreenController settingsPanel;
        public bool IsOpen => bgPanel.IsOpen;

        // pause screen buttons
        public void Resume()
        {
            Close();
        }
        public void Settings()
        {
            OpenSettings();
        }
        public void Exit()
        {
            Close();
            LoadMainMenuScene();
        }

        public void Open(UnityAction closeCallback)
        {
            Debug.Log(" Open pause screen");
            bgPanel.Open(closeCallback);
            pausePanel.Open();
        }
        void Close()
        {
            Debug.Log(" close pause screen");
            bgPanel.Close();
        }
        void OpenSettings()
        {
            pausePanel.Close();
            settingsPanel.Open(OnCloseSettings);
        }
        void OnCloseSettings()
        {
            pausePanel.Open();
        }

        void LoadMainMenuScene()
        {
            SceneManager.LoadScene(0); // load the main menu
        }
    }
}
