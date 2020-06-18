using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Engine/SequenceNode", fileName = "SequenceNode")]
    public class SequenceNode : Node
    {
        public List<Node> Children;
    }
}