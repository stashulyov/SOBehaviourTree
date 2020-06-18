using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Engine/SequenceNode", fileName = "SequenceNode")]
    public class SequenceNode : Node
    {
        public List<Node> Children = new List<Node>();

        public override void OnInitialize()
        {
            if (Children == null)
                throw new NullReferenceException();

            foreach (var item in Children)
            {
                if (item == null)
                    throw new NullReferenceException();
            }

            foreach (var item in Children)
                item.OnInitialize();
        }
    }
}