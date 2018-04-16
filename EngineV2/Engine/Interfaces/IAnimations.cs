using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces
{
    public interface IAnimations
    {
        void Initialize(IEntity ent, int rows, int columns);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
