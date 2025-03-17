using System;
using IsmaLB.Cameras;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Levels
{
    public class EnergyNodeController : MonoBehaviour, IIntractable
    {
        [SerializeField] LevelSO puzzleLevel;
        [SerializeField] LevelEventSO loadPuzzleLevelEvent;
        [Header("Visuals")]
        [SerializeField] GameObject particles;
        [SerializeField] GameObject target;
        [SerializeField] EnergyNodeCamera nodeCamera;
        [SerializeField] GameObject rangeIndicator;
        [SerializeField] GameObject rangeLockedIndicator;

        void Start()
        {
            rangeIndicator.SetActive(false);
            rangeLockedIndicator.SetActive(false);
        }
        void Update()
        {
            UpdateVisuals(puzzleLevel.State);
        }
        public void Interact()
        {
            OnInteract();
        }
        public void Select()
        {
            if (puzzleLevel.State == LevelState.Unlocked)
            {
                rangeIndicator.SetActive(true);
            }
            else if (puzzleLevel.State == LevelState.Locked)
            {
                rangeLockedIndicator.SetActive(true);
            }
        }

        public void Deselect()
        {
            rangeIndicator.SetActive(false);
            rangeLockedIndicator.SetActive(false);
        }
        private void OnInteract()
        {
            if (puzzleLevel.State == LevelState.Unlocked)
            {
                Debug.Log("Loading puzzle level");
                loadPuzzleLevelEvent.Raise(puzzleLevel);
                nodeCamera.EnableCamera();
            }
        }
        private void UpdateVisuals(LevelState state)
        {
            switch (state)
            {
                case LevelState.Locked:
                    target.SetActive(true);
                    particles.SetActive(false);
                    return;
                case LevelState.Unlocked:
                    target.SetActive(true);
                    particles.SetActive(true);
                    return;
                case LevelState.Completed:
                    target.SetActive(false);
                    particles.SetActive(false);
                    Deselect();
                    return;
            }
        }

    }
}
