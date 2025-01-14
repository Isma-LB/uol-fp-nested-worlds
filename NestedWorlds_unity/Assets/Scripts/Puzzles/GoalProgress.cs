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
            if (IsGoalReached) return;
            current -= amount;
            current = Mathf.Max(0, current);
        }
        public void Increase(float amount)
        {
            current += amount;
            current = Mathf.Min(current, goal);
        }
        public bool IsGoalReached => current == goal;
        public float Progress { get => current / goal; }
    }
}
