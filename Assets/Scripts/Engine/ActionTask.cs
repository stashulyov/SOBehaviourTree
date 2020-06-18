using UnityEngine;

namespace Engine
{
    public abstract class ActionTask : ScriptableObject, IUpdatable, IExecutable
    {
        public virtual void OnExecute()
        {
        }

        public virtual Result OnUpdate(float deltaTime)
        {
            return Result.Success;
        }
    }
}