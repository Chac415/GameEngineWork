using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces;


namespace Engine.Entity_Management
{
    public abstract class Entity : IEntity
    {
        public abstract void Initialize(Texture2D Tex, Vector2 Posn, IBehaviourManager behaviours);

        public abstract void Update(GameTime game);
        public abstract Rectangle Hitbox { get; set; }
        public abstract string Tag { get; set; }
        public abstract void SetPoints(int Columns, int Rows);
        public abstract void BuildEdges();
        public abstract void UniqueData();
        public abstract void OnColiEnter(IEntity ColiEnt, ISAT SAT);

        //Animations
        public abstract float Direction { get; set; }
        public abstract List<Vector2> Point();
        public abstract List<Vector2> Edges();
        public abstract Vector2 Center();

        //Virtual Variables
        public abstract Vector2 Position { get; set; }
        public abstract Texture2D Texture { get; set; }

        //Virtual Methods
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}

