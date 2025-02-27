using UnityEngine;

namespace IsmaLB.Puzzles
{
    public class GoalProgress
    {
        float goal;
        float current = 0;

        public GoalProgress(float goal)
        {
            this.goal = goal;
        }
        public void Decrease(float amount)
        {
            current -= amount;
            current = Mathf.Max(0, current);
        }
        public void Increase(float amount)
        {
            current += amount;
            current = Mathf.Min(current, goal);
        }
        // goal is considered complete when it reaches 90% to smooth out rounding issues
        public bool IsGoalReached => current >= goal * 0.95f;
        public float Progress { get => IsGoalReached ? 1 : current / goal; }
    }
}
