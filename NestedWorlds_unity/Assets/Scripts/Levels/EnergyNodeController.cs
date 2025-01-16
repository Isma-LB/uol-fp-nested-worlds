using System;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Levels
{
    public class EnergyNodeController : PlayerRangeItem
    {
        [SerializeField] InputReader inputReader;
        [SerializeField] LevelSO puzzleLevel;
        [Header("Visuals")]
        [SerializeField] GameObject particles;
        [SerializeField] GameObject target;

        void OnEnable()
        {
            inputReader.interactEvent += OnInteract;
        }
        void OnDisable()
        {
            inputReader.interactEvent -= OnInteract;
        }

        void Update()
        {
            UpdateVisuals(puzzleLevel.State);
        }

        private void OnInteract()
        {
            if (IsPlayerInRange == false) return;
            if (puzzleLevel.State == LevelState.Unlocked)
            {
                Debug.Log("Loading puzzle level");
            }
        }
        private void UpdateVisuals(LevelState state)
        {
            switch (state)
            {
                case LevelState.Locked:
                    target.SetActive(false);
                    particles.SetActive(false);
                    return;
                case LevelState.Unlocked:
                    target.SetActive(true);
                    particles.SetActive(true);
                    return;
                case LevelState.Completed:
                    target.SetActive(true);
                    particles.SetActive(false);
                    return;
            }
        }
        protected override void HandleRangeIndicatorGraphic(bool on)
        {
            base.HandleRangeIndicatorGraphic(puzzleLevel.State == LevelState.Unlocked && on);
        }

    }
}
