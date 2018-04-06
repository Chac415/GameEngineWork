using Microsoft.Xna.Framework.Graphics;

namespace Engine.State_Machines.Animations
{
    public interface IAnimationState<T>
    {
        void Animate();
        void DrawAnimation(SpriteBatch spriteBatch);
        void ResetAnimation();
    }
}