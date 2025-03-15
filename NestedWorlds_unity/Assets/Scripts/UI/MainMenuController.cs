using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] UIPanel mainPanel;
        [SerializeField] UIPanel settingsPanel;
        [SerializeField] UIPanel creditsPanel;
        [SerializeField] MainMenuStoryManager storyManager;
        bool isLoadingScene;

        void Start()
        {
            ActivatePanels();
            mainPanel.Open();
            // trigger main menu music
            AudioManager.QueueMusicTrack(MusicTrackType.Menu);
        }

        #region Button callbacks
        public void Play()
        {
            if (isLoadingScene) return;
            isLoadingScene = true;
            StartCoroutine(LoadGameScene());
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
        private IEnumerator LoadGameScene()
        {
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            sceneLoad.allowSceneActivation = false;
            yield return storyManager.StartStory();
            sceneLoad.allowSceneActivation = true;
        }
        private void ActivatePanels()
        {
            mainPanel.gameObject.SetActive(true);
            settingsPanel.gameObject.SetActive(true);
            creditsPanel.gameObject.SetActive(true);
        }
    }
}
