using Engine;

namespace Tests
{
    public class Timer : ActionTask
    {
        public float Value;

        private float _counter;

        public override void OnExecute()
        {
            _counter = 0f;
        }

        public override Result OnUpdate(float deltaTime)
        {
            _counter += deltaTime;

            if (_counter >= Value)
                return Result.Success;

            return Result.Running;
        }
    }
}