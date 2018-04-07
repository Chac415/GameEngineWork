using Microsoft.Xna.Framework.Graphics;

namespace Engine.Animations
{
    public interface IAnimation
    {
        Texture2D GetSpriteSheet();
        int GetRows();
        int GetColumns();
    }
}