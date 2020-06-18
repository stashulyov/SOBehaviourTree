using System;
using System.Collections.Generic;
using Engine;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class SequenceNodeTests
    {
        [Test]
        public void OnInitialize_ChildrenListIsNull_Throws()
        {
            var node = ScriptableObject.CreateInstance<SequenceNode>();
            node.Children = null;

            void Act() => node.OnInitialize();

            Assert.Throws<NullReferenceException>(Act);
        }

        [Test]
        public void OnInitialize_OneOfChildrenIsNull_Throws()
        {
            var node = ScriptableObject.CreateInstance<SequenceNode>();
            node.Children = new List<Node> {null};

            void Act() => node.OnInitialize();

            Assert.Throws<NullReferenceException>(Act);
        }

        [Test]
        public void OnInitialize_SeveralChildren_CallsEach()
        {
            var node = ScriptableObject.CreateInstance<SequenceNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnInitialize();

            child1.Received().OnInitialize();
            child2.Received().OnInitialize();
        }
    }

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
            var actionTask = ScriptableObject.CreateInstance<ActionTaskMock>();
            node.ActionTask = actionTask;

            node.OnInitialize();

            Assert.IsTrue(actionTask.ReceivedOnInitialize);
        }

        [Test]
        public void OnExecute_CallsActionTask()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = ScriptableObject.CreateInstance<ActionTaskMock>();
            node.ActionTask = actionTask;

            node.OnExecute();

            Assert.IsTrue(actionTask.ReceivedOnExecute);
        }

        [Test]
        public void OnUpdate_CallsActionTask()
        {
            var node = ScriptableObject.CreateInstance<ActionNode>();
            var actionTask = ScriptableObject.CreateInstance<ActionTaskMock>();
            node.ActionTask = actionTask;

            node.OnUpdate(1f);

            Assert.IsTrue(actionTask.ReceivedOnUpdate);
        }

        private class ActionTaskMock : ActionTask
        {
            public bool ReceivedOnExecute;
            public bool ReceivedOnUpdate;
            public bool ReceivedOnInitialize;

            public override void OnInitialize()
            {
                ReceivedOnInitialize = true;
            }

            public override void OnExecute()
            {
                ReceivedOnExecute = true;
            }

            public override Result OnUpdate(float deltaTime)
            {
                ReceivedOnUpdate = true;
                return Result.Success;
            }
        }
    }
}