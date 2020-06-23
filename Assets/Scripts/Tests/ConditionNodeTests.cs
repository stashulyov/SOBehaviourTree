using System;
using Engine;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ConditionNodeTests
    {
        [Test]
        public void OnInitialize_ConditionTaskIsNotAssigned_Throws()
        {
            var node = ScriptableObject.CreateInstance<ConditionNode>();

            void Act() => node.OnInitialize();

            Assert.Throws<NullReferenceException>(Act);
        }

        [Test]
        public void OnInitialize_CallsConditionTask()
        {
            var node = ScriptableObject.CreateInstance<ConditionNode>();
            var conditionTask = Substitute.For<ConditionTask>();
            node.ConditionTask = conditionTask;

            node.OnInitialize();

            conditionTask.Received().OnInitialize();
        }

        [Test]
        public void OnExecute_CallsConditionTask()
        {
            var node = ScriptableObject.CreateInstance<ConditionNode>();
            var conditionTask = Substitute.For<ConditionTask>();
            node.ConditionTask = conditionTask;

            node.OnExecute();

            conditionTask.Received().OnExecute();
        }

        [Test]
        public void OnUpdate_CallsConditionTask()
        {
            var node = ScriptableObject.CreateInstance<ConditionNode>();
            var conditionTask = Substitute.For<ConditionTask>();
            node.ConditionTask = conditionTask;

            node.OnUpdate(1f);

            conditionTask.ReceivedWithAnyArgs().OnUpdate(default);
        }

        [TestCase(Result.Failure)]
        [TestCase(Result.Running)]
        [TestCase(Result.Success)]
        public void OnUpdate_NodeReturnsSameResult(Result expectedResult)
        {
            var node = ScriptableObject.CreateInstance<ConditionNode>();
            var conditionTask = Substitute.For<ConditionTask>();
            conditionTask.OnUpdate(Arg.Any<float>()).Returns(expectedResult);
            node.ConditionTask = conditionTask;

            var actualResult = node.OnUpdate(1f);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}