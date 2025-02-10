using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] UIPanel mainPanel;
        [SerializeField] UIPanel settingsPanel;
        [SerializeField] UIPanel creditsPanel;

        void Start()
        {
            ActivatePanels();
            mainPanel.Open();
        }

        #region Button callbacks
        public void Play()
        {
            LoadGameScene();
        }
        public void OpenSettings()
        {
            settingsPanel.Open(OnSubPanelClosedCallback);
            mainPanel.Close();
        }
        public void OpenCredits()
        {
            creditsPanel.Open(OnSubPanelClosedCallback);
            mainPanel.Close();
        }
        #endregion

        void OnSubPanelClosedCallback()
        {
            mainPanel.Open();
        }
        private void LoadGameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        private void ActivatePanels()
        {
            mainPanel.gameObject.SetActive(true);
            settingsPanel.gameObject.SetActive(true);
            creditsPanel.gameObject.SetActive(true);
        }
    }
}
