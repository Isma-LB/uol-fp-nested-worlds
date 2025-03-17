using System;
using IsmaLB.Input;
using UnityEngine;

namespace IsmaLB.Tutorial
{
    [CreateAssetMenu(fileName = "MoveTutorialSO", menuName = "Scriptable Objects/Tutorial/MoveTutorialSO")]
    public class MoveTutorialSO : TutorialStepSO
    {
        [SerializeField] InputReader inputReader;
        bool moveInput = false;

        void OnEnable() => inputReader.moveEvent += OnMove;
        void OnDisable() => inputReader.moveEvent -= OnMove;

        private void OnMove(Vector2 move)
        {
            if (move != Vector2.zero)
            {
                moveInput = true;
            }
        }
        public override bool Evaluate() => moveInput;
        public override void Init() => moveInput = false;
    }
}
