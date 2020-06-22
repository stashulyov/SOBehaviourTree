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

        public override void OnExecute()
        {
            foreach (var item in Children)
                item.OnExecute();
        }

        public override Result OnUpdate(float deltaTime)
        {
            foreach (var item in Children)
            {
                var result = item.OnUpdate(deltaTime);

                if (result != Result.Success)
                    return result;
            }

            return Result.Success;
        }
    }
}