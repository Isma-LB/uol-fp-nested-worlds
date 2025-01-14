using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IsmaLB.Puzzles
{
    public class PuzzleLevelController : MonoBehaviour
    {
        ParticlesTarget[] targets;
        public static event Action OnGameCompleted;
        bool isCompleted = false;
        // Start is called before the first frame update
        void Start()
        {
            targets = FindObjectsByType<ParticlesTarget>(FindObjectsSortMode.None);
        }

        // Update is called once per frame
        void Update()
        {
            if (GetCompletionScore() >= 1 && isCompleted == false)
            {
                Debug.Log("Game completed");
                isCompleted = true;
                OnGameCompleted?.Invoke();
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
