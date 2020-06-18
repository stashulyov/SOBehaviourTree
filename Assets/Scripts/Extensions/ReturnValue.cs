using Engine;
using UnityEngine;

namespace Extensions
{
    [CreateAssetMenu(menuName = "SOBehaviourTree/Extensions/ReturnValue", fileName = "ReturnValue")]
    public class ReturnValue : ActionTask
    {
        public Result Result;

        public Result OnUpdate(float deltaTime)
        {
            return Result;
        }
    }
}