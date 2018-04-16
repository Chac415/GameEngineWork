using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine.Interfaces
{
    public interface ISAT
    {
        void PolygonVsPolygon(IEntity _ent1, IEntity _ent2);
        bool Intersect { get; set; }
        Vector2 MTV { get; set; }
    }
}
