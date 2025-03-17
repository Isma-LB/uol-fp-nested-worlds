using System;
using IsmaLB.Levels;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    [CreateAssetMenu(fileName = "LoadLevelTutorialSO", menuName = "Scriptable Objects/Tutorial/LoadLevelTutorialSO")]
    public class LoadLevelTutorialSO : TutorialStepSO
    {
        [SerializeField] LevelEventSO levelEvent;
        bool eventTriggered = false;
        void OnEnable() => levelEvent.onLevelEvent += OnLevelEvent;
        void OnDisable() => levelEvent.onLevelEvent -= OnLevelEvent;

        private void OnLevelEvent(LevelSO SO)
        {
            eventTriggered = true;
        }

        public override bool Evaluate() => eventTriggered;
        public override void Init() => eventTriggered = false;
    }
}
