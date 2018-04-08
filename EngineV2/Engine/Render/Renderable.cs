using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces;

namespace Engine.Render
{
    class Renderable : IRenderable
    {

        public void Draw(IList<IScene> scene, SpriteBatch sprite)
        {

            sprite.Begin();
            foreach (IScene screen in scene)
            {
                screen.Draw(sprite);
            }
            

            sprite.End();

        }

    }
}
