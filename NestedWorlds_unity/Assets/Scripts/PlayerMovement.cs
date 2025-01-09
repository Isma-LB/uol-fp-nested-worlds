using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IsmaLB.Input;

namespace IsmaLB
{
    [RequireComponent(typeof(SphereCharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        SphereCharacterController controller;
        Vector2 input;
        bool jump = false;

        void OnEnable()
        {
            controller = GetComponent<SphereCharacterController>();
            inputReader.moveEvent += OnMove;
            inputReader.jumpEvent += OnJump;
        }


        void OnDisable()
        {
            inputReader.moveEvent -= OnMove;
            inputReader.jumpEvent -= OnJump;
        }


        private void FixedUpdate()
        {
            controller.Move(input.y, input.x, jump);
            jump = false;
        }

        private void OnJump() => jump = true;
        private void OnMove(Vector2 value) => input = value;
    }
}
