using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Interfaces
{
    public interface IQuad
    {
        void Clear();
        void Insert(IEntity Entity);
        IList<IEntity> Retrieve(IList<IEntity> returnObjects, IEntity Entity);
    }
}
