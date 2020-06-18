using UnityEngine;

namespace Engine
{
    public abstract class Node : ScriptableObject, INode
    {
        public virtual void OnInitialize()
        {
        }

        public virtual void OnExecute()
        {
        }

        public virtual Result OnUpdate(float deltaTime)
        {
            return Result.Success;
        }
    }
}