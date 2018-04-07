using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines.Animations
{
    public interface IAnimationState
    {
        void Animate(GameTime gameTime);
        void DrawAnimation(SpriteBatch spriteBatch);
        void ResetAnimation();
    }
}