using Engine.Physics;

namespace Engine.Interfaces
{
    public interface IPhysicsManager
    {
        void AddToList(IPhysics physicsObj);
        void Update();
    }
}
