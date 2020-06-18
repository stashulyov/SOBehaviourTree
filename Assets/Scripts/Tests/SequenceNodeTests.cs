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
}