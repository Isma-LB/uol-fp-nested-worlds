using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.Puzzles
{

    public class PuzzleCompletedUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // PuzzleLevelController.OnGameCompleted += ShowPanel;
            gameObject.SetActive(false);
        }
        void OnDestroy()
        {
            // PuzzleLevelController.OnGameCompleted -= ShowPanel;
        }
        void ShowPanel()
        {
            Debug.Log("ShowPanelCalled");
            gameObject.SetActive(true);
        }
        public void RestartScene()
        {
            Debug.Log("Restart puzzle scene");
            // FindObjectOfType<SceneLoader>().SendMessage("UnloadTestPuzzle");
        }
    }
}