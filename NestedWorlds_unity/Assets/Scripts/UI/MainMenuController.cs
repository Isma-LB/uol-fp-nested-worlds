using System;
using System.Collections;
using IsmaLB.Gameplay;
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
            if (IsGameCompleted())
            {
                StartCoroutine(ShowCredits());
            }
            else
            {
                mainPanel.Open();
            }
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
        private IEnumerator ShowCredits()
        {
            yield return storyManager.ShowStoryEnd();
            OpenCredits();
        }
        private void ActivatePanels()
        {
            mainPanel.gameObject.SetActive(true);
            settingsPanel.gameObject.SetActive(true);
            creditsPanel.gameObject.SetActive(true);
        }
        private bool IsGameCompleted()
        {
            if (PlayerPrefs.GetInt(GameManager.GAME_COMPLETED_KEY, 0) != 0)
            {
                PlayerPrefs.DeleteKey(GameManager.GAME_COMPLETED_KEY);
                return true;
            }
            return false;
        }
    }
}
