using IsmaLB.Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsmaLB.UI
{
    public class EraseGameDataUIController : MonoBehaviour
    {
        public void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
            PauseScreenController pauseScreenController = GetComponentInParent<PauseScreenController>();
            if (pauseScreenController)
            {
                pauseScreenController.Exit();
            }
            else
            {
                Debug.LogWarning("Could not find the pause screen controller, ensure the settings screen is inside a pause menu");
            }
        }
    }
}
