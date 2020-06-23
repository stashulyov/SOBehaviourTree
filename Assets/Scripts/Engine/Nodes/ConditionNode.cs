using System;
using UnityEngine;

namespace Engine
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Engine/ConditionNode", fileName = "ConditionNode")]
    public class ConditionNode : Node
    {
        public ConditionTask ConditionTask;

        public override void OnInitialize()
        {
            if (ConditionTask == null)
                throw new NullReferenceException();

            ConditionTask.OnInitialize();
        }

        public override void OnExecute()
        {
            ConditionTask.OnExecute();
        }

        public override Result OnUpdate(float deltaTime)
        {
            return ConditionTask.OnUpdate(deltaTime);
        }
    }
}