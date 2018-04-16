namespace Engine.Interfaces
{
    public interface IBehaviourManager
    {
        void Update();
        T createMind<T>(IEntity ent) where T : IBehaviour, new();
    }
}
