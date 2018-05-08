using Engine.Interfaces;

namespace Engine.Collision_Management
{
    public interface ICollidable
    {
        bool isTrigger { get; set; }
        void OnTriggerEnter(IEntity collision);
        void OnCollision(IEntity collision);
    }
}
