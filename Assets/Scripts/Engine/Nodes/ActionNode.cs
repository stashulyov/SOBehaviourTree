using System;
using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Engine/ActionNode", fileName = "ActionNode")]
    public class ActionNode : Node
    {
        public ActionTask ActionTask;

        public override void OnInitialize()
        {
            if (ActionTask == null)
                throw new InvalidOperationException();

            ActionTask.OnInitialize();
        }

        public override void OnExecute()
        {
            ActionTask.OnExecute();
        }

        public override Result OnUpdate(float deltaTime)
        {
            return ActionTask.OnUpdate(deltaTime);
        }
    }
}