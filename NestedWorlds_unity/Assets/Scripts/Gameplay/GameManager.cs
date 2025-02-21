using IsmaLB.Levels;
using UnityEngine;

namespace IsmaLB.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PuzzleProgressionSO puzzleProgression;

        void OnEnable()
        {
            puzzleProgression.Init();
            AudioManager.QueueMusicTrack(MusicTrackType.Exploration);
        }
        void OnDisable()
        {
            puzzleProgression.Disable();
        }
    }
}
