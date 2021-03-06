﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Engine.Interfaces
{
    public interface IEntity
    {
        void Initialize(Texture2D Tex, Vector2 Posn, IBehaviourManager behaviours);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime game);
        void SetPoints(int Columns, int Rows);
        void BuildEdges();
        Vector2 Position { get; set; }
        Texture2D Texture { get; set; }
        Rectangle Hitbox { get; set; }
        string Tag { get; set; }
        float Direction { get; set; }
        void UniqueData();
        void OnColiEnter(IEntity ColiEnt, ISAT SAT);



        List<Vector2> Point();
        List<Vector2> Edges();
        Vector2 Center();

    }
}
