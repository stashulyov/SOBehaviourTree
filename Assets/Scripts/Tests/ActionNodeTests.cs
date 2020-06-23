using System;
using Engine;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ActionNodeTests
    {
        [Test]
        public void OnInitialize_ActionTaskIsNotAssigned_Throws()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();

            void Act() => node.OnInitialize();

            Assert.Throws<InvalidOperationException>(Act);
        }

        [Test]
        public void OnInitialize_CallsActionTask()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = Substitute.For<ActionTask>();
            node.ActionTask = actionTask;

            node.OnInitialize();

            actionTask.Received().OnInitialize();
        }

        [Test]
        public void OnExecute_CallsActionTask()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = Substitute.For<ActionTask>();
            node.ActionTask = actionTask;

            node.OnExecute();

            actionTask.Received().OnExecute();
        }

        [Test]
        public void OnUpdate_CallsActionTask()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = Substitute.For<ActionTask>();
            node.ActionTask = actionTask;

            node.OnUpdate(1f);

            actionTask.ReceivedWithAnyArgs().OnUpdate(default);
        }

        [TestCase(Result.Failure)]
        [TestCase(Result.Running)]
        [TestCase(Result.Success)]
        public void OnUpdate_NodeReturnsSameResult(Result expectedResult)
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = Substitute.For<ActionTask>();
            actionTask.OnUpdate(Arg.Any<float>()).Returns(expectedResult);
            node.ActionTask = actionTask;

            var actualResult = node.OnUpdate(1f);
            
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}