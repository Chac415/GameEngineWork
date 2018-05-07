using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Engine.Interfaces
{
    public interface IScene
    {
        void LoadContent();
        void update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
