using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Physics;

namespace Engine.Interfaces
{
    public interface IPhysicsManager
    {
        void AddToList(IPhysics physicsObj);
        void Update();
    }
}
