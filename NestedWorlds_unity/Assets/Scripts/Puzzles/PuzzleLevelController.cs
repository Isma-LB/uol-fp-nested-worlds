using System;
using System.Collections;
using System.Collections.Generic;
using IsmaLB.Input;
using UnityEngine;
using UnityEngine.InputSystem;
namespace IsmaLB.Puzzles
{
    public class PuzzleLevelController : MonoBehaviour
    {
        [SerializeField] InputReader inputReader;
        [SerializeField] PuzzleEventSO puzzleSolvedEvent;
        [SerializeField] PuzzleEventSO restartPuzzleEvent;
        [SerializeField] PuzzleEventSO quitPuzzleEvent;
        ParticlesTarget[] targets;
        bool isCompleted = false;
        // Start is called before the first frame update
        void Start()
        {
            targets = FindObjectsByType<ParticlesTarget>(FindObjectsSortMode.None);
            inputReader.EnablePuzzleInput();
        }
        void OnEnable()
        {
            inputReader.restartEvent += OnRestart;
            inputReader.quitEvent += OnQuit;
        }
        void OnDisable()
        {
            inputReader.restartEvent -= OnRestart;
            inputReader.quitEvent -= OnQuit;
        }

        private void OnQuit()
        {
            quitPuzzleEvent.Raise();
        }

        private void OnRestart()
        {
            restartPuzzleEvent.Raise();
        }

        // Update is called once per frame
        void Update()
        {
            if (GetCompletionScore() >= 1 && isCompleted == false)
            {
                Debug.Log("Puzzle Completed");
                isCompleted = true;
                puzzleSolvedEvent.Raise();
            }
        }

        float GetCompletionScore()
        {
            float total = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                total += targets[i].Value;
            }
            return total / targets.Length;
        }
    }
}
