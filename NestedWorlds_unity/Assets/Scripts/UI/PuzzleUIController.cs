using UnityEngine;

namespace IsmaLB
{
    public class PuzzleUIController : MonoBehaviour
    {
        [Header("Broadcast on: ")]
        [SerializeField] PuzzleEventSO restartPuzzleEvent;
        [SerializeField] PuzzleEventSO quitPuzzleEvent;

        public void RestartPuzzle()
        {
            restartPuzzleEvent.Raise();
        }
        public void QuitPuzzle()
        {
            quitPuzzleEvent.Raise();
        }
    }
}
