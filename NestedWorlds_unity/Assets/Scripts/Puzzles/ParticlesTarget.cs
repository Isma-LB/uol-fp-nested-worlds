using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsmaLB.Puzzles
{

    public class ParticlesTarget : MonoBehaviour
    {
        [SerializeField, TagSelector] string particlesTag = "Particles";
        [SerializeField] float energyGoal = 50;
        [SerializeField] float energyDecay = 5;

        [Header("Visuals")]
        [SerializeField] SpriteRenderer graphic;
        [SerializeField] Color emptyColor = Color.gray;
        [SerializeField] Color fullColor = Color.white;
        GoalProgress goal;

        public float Value { get => goal.Progress; }
        void Awake()
        {
            goal = new(energyGoal);
        }
        void Update()
        {
            goal.Decrease(energyDecay * Time.deltaTime);
            HandleGraphic();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(particlesTag))
            {
                goal.Increase(1);
                // disable particle and return to pool
                other.gameObject.SetActive(false);
            }
        }
        void HandleGraphic()
        {
            if (graphic == null) return;
            graphic.color = Color.Lerp(emptyColor, fullColor, goal.Progress);
        }
    }
}
