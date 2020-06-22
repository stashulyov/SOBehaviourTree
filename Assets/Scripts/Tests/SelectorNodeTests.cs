using System;
using System.Collections.Generic;
using Engine;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class SelectorNodeTests
    {
        [Test]
        public void OnInitialize_ChildrenListIsNull_Throws()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            node.Children = null;

            void Act() => node.OnInitialize();

            Assert.Throws<NullReferenceException>(Act);
        }

        [Test]
        public void OnInitialize_OneOfChildrenIsNull_Throws()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            node.Children = new List<Node> {null};

            void Act() => node.OnInitialize();

            Assert.Throws<NullReferenceException>(Act);
        }

        [Test]
        public void OnInitialize_SeveralChildren_CallsEach()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnInitialize();

            child1.Received().OnInitialize();
            child2.Received().OnInitialize();
        }

        [Test]
        public void OnExecute_SeveralChildren_CallsEach()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnExecute();

            child1.Received().OnExecute();
            child2.Received().OnExecute();
        }

        [Test]
        public void OnUpdate_SeveralChildren_CallsFirst()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnUpdate(1f);

            child1.ReceivedWithAnyArgs().OnUpdate(default);
        }

        [Test]
        public void OnUpdate_EmptyChildrenList_ReturnsFailure()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            node.Children = new List<Node>();

            var result = node.OnUpdate(1f);

            Assert.AreEqual(Result.Failure, result);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsFailure_CallsSecondChild()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Failure);
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnUpdate(1f);

            child2.ReceivedWithAnyArgs().OnUpdate(default);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsSuccess_ReturnsSuccess()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Success);
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            var result = node.OnUpdate(1f);

            Assert.AreEqual(Result.Success, result);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsSuccess_DoesNotCallSecondChild()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Success);
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnUpdate(1f);

            child2.DidNotReceiveWithAnyArgs().OnUpdate(default);
        }

        [Test]
        public void OnUpdate_FirstAndSecondChildrenReturnFailure_ReturnsFailure()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Failure);
            child2.OnUpdate(Arg.Any<float>()).Returns(Result.Failure);
            node.Children = new List<Node> {child1, child2};

            var result = node.OnUpdate(1f);

            Assert.AreEqual(Result.Failure, result);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsRunning_DoesNotCallSecondChild()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Running);
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            node.OnUpdate(1f);

            child2.DidNotReceiveWithAnyArgs().OnUpdate(default);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsRunning_ReturnsRunning()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Running);
            var child2 = Substitute.For<Node>();
            node.Children = new List<Node> {child1, child2};

            var result = node.OnUpdate(1f);

            Assert.AreEqual(Result.Running, result);
        }

        [Test]
        public void OnUpdate_FirstChildReturnsFailureAndSecondReturnsSuccess_ReturnsSuccess()
        {
            var node = ScriptableObject.CreateInstance<SelectorNode>();
            var child1 = Substitute.For<Node>();
            var child2 = Substitute.For<Node>();
            child1.OnUpdate(Arg.Any<float>()).Returns(Result.Failure);
            child2.OnUpdate(Arg.Any<float>()).Returns(Result.Success);
            node.Children = new List<Node> {child1, child2};

            var result = node.OnUpdate(1f);

            Assert.AreEqual(Result.Success, result);
        }
    }
}