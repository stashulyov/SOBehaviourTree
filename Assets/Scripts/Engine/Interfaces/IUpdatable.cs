namespace Engine
{
    public interface IUpdatable
    {
        Result OnUpdate(float deltaTime);
    }
}