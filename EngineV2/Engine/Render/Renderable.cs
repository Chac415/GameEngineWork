using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces;

namespace Engine.Render
{
    class Renderable : IRenderable
    {

        public void Draw(IScene scene, SpriteBatch sprite)
        {

            sprite.Begin();

            scene.Draw(sprite);
            

            sprite.End();

        }

    }
}
