using System;
using UnityEngine;

namespace IsmaLB.Levels
{
    public class EnergyNodeController : PlayerRangeItem
    {
        [SerializeField] LevelSO puzzleLevel;
        [Header("Visuals")]
        [SerializeField] GameObject particles;
        [SerializeField] GameObject target;

        void Update()
        {
            UpdateVisuals(puzzleLevel.State);
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
    }
}
