using System.Collections;
using IsmaLB.Puzzles;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
namespace Puzzles
{
    public class GoalProgressTest
    {

        [Test]
        public void Progress_StartsAtZero()
        {
            GoalProgress goal = new(1);
            Assert.AreEqual(0, goal.Progress);
        }
        [Test]
        public void Progress_IncreasesByAmount()
        {
            GoalProgress value = new(1);
            value.Increase(0.5f);
            Assert.AreEqual(0.5f, value.Progress);
        }
        [Test]
        public void Progress_IncreasesProportionallyByAmount()
        {
            GoalProgress value = new(3);
            value.Increase(1.5f);
            Assert.AreEqual(0.5f, value.Progress);
        }
        [Test]
        public void Progress_IncreasesUntilOne()
        {
            GoalProgress value = new(1);
            value.Increase(2);
            Assert.AreEqual(1, value.Progress);
        }
        [Test]
        public void Progress_DecreaseByAmount()
        {
            GoalProgress value = new(1);
            value.Increase(0.5f);
            value.Decrease(0.25f);
            Assert.AreEqual(0.25f, value.Progress);
        }
        [Test]
        public void Progress_DecreasesProportionallyByAmount()
        {
            GoalProgress value = new(3);
            value.Increase(2);
            value.Decrease(0.5f);
            Assert.AreEqual(0.5f, value.Progress);
        }
        [Test]
        public void Progress_DoesNotDecreaseUnderZero()
        {
            GoalProgress value = new(1);
            value.Decrease(10);
            Assert.AreEqual(0, value.Progress);
        }
        [Test]
        public void Progress_DoesNotDecreaseAfterGoalIsReached()
        {
            GoalProgress value = new(1);
            value.Increase(1);
            value.Decrease(1);
            Assert.AreEqual(1, value.Progress);
        }
        [Test]
        public void IsGoalReached_StartsAsFalse()
        {
            GoalProgress value = new(1);
            Assert.IsFalse(value.IsGoalReached);
        }
        [Test]
        public void IsGoalReached_EndsAsTrue()
        {
            GoalProgress value = new(1);
            value.Increase(1);
            Assert.IsTrue(value.IsGoalReached);
        }
        [Test]
        public void IsGoalReached_IsNotTreForPartialIncrease()
        {
            GoalProgress value = new(1);
            value.Increase(0.5f);
            Assert.IsFalse(value.IsGoalReached);
        }
    }
}
