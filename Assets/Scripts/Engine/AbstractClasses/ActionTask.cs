using UnityEngine;

namespace Engine
{
    public abstract class ActionTask : ScriptableObject, IInitializable, IExecutable, IUpdatable
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