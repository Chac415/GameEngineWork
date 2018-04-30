using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces
{
    public interface IRenderable
    {
        void Draw(IScene scene, SpriteBatch sprite);
    }
}
