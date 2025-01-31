using IsmaLB.Levels;
using UnityEngine;

namespace IsmaLB
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] PuzzleProgressionSO puzzleProgression;

        void OnEnable()
        {
            puzzleProgression.Init();
        }
        void OnDisable()
        {
            puzzleProgression.Disable();
        }
    }
}
