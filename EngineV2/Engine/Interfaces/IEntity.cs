using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Engine.Interfaces
{
    public interface IEntity
    {
        void Initialize(Texture2D Tex, Vector2 Posn, IBehaviourManager behaviours);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime game);
        void SetPoints(int spriteWidth, int spriteHeight);
        void BuildEdges();
        Vector2 Position { get; set; }
        Texture2D Texture { get; set; }
        Rectangle Hitbox { get; set; }
        string Tag { get; set; }
        float Direction { get; set; }
        void UniqueData();
        List<Vector2> Point();
        List<Vector2> Edges();
        Vector2 Center();


        #region Physics content

        Vector2 Velocity { get; set; }
        Vector2 Acceleration { get; set; }
        Vector2 Gravity { get; set; }
        bool GravityBool { get; set; }
        void ApplyForce(Vector2 force);
        void ApplyImpulse(Vector2 closingVelo);
        void UpdatePhysics();
        #endregion

    }
}
