using Engine.Physics;

namespace Engine.Interfaces
{
    public interface IPhysicsManager
    {
        void AddToList(IEntity physicsObj);
        void Update();
    }
}
