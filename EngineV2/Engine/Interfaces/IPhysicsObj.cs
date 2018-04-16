using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface IPhysicsObj
    {
        void hasPhysics(IEntity obj);
        void gravity();

        List<IEntity> getPhysicsList();
    }
}
