using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Engine/ActionNode", fileName = "ActionNode")]
    public class ActionNode : Node
    {
        public ActionTask ActionTask;
    }
}