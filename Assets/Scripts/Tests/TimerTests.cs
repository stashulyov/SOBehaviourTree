using Engine;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class TimerTests
    {
        [Test]
        public void OnUpdate_ValueDoesNotSetAndDeltaIsPositive_ReturnsSuccess()
        {
            var task = ScriptableObject.CreateInstance<Timer>();

            var result = task.OnUpdate(1f);

            Assert.AreEqual(Result.Success, result);
        }

        [Test]
        public void OnUpdate_ValueDoesNotSetAndDeltaIsNegative_ReturnsRunning()
        {
            var task = ScriptableObject.CreateInstance<Timer>();

            var result = task.OnUpdate(-1f);

            Assert.AreEqual(Result.Running, result);
        }

        [Test]
        public void OnUpdate_PassedTimeLessThanAValue_ReturnsRunning()
        {
            var task = ScriptableObject.CreateInstance<Timer>();
            task.Value = 2f;

            var result = task.OnUpdate(1f);

            Assert.AreEqual(Result.Running, result);
        }

        [Test]
        public void OnUpdate_PassedTimeIsEqualToValue_ReturnsSuccess()
        {
            var task = ScriptableObject.CreateInstance<Timer>();
            task.Value = 2f;

            var result = task.OnUpdate(2f);

            Assert.AreEqual(Result.Success, result);
        }

        [Test]
        public void OnUpdate_PassedTimeMoreThanAValue_ReturnsSuccess()
        {
            var task = ScriptableObject.CreateInstance<Timer>();
            task.Value = 2f;

            var result = task.OnUpdate(4f);

            Assert.AreEqual(Result.Success, result);
        }

        [Test]
        public void OnExecute_CallAfterFinishingTimer_ResetsTimer()
        {
            var task = ScriptableObject.CreateInstance<Timer>();
            task.Value = 2f;

            var result = task.OnUpdate(4f);

            task.OnExecute();

            result = task.OnUpdate(1f);
            
            Assert.AreEqual(Result.Running, result);
        }
    }
}