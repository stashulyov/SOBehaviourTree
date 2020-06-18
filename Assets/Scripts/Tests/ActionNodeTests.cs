using System;
using Engine;
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