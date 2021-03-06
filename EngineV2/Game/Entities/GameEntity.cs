﻿using System;
using System.Collections.Generic;
using Engine.Entity_Management;
using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectHastings.Entities
{
    /// <summary>
    /// Game Entity Class, To be inherited by game entities, adding them to the scene
    /// Author: Nathan Roberson & Carl Chalmers
    /// Date of Change: 03/02/18
    /// Version: 0.4
    /// </summary>
    public class GameEntity : Entity
    {
        public override string Tag { get; set; }
        public override Vector2 Position { get; set; }
        public override Texture2D Texture { get; set; }
        public override Rectangle Hitbox { get; set; }
        public int row;
        public bool onTerrain;
        public float speed;

        protected List<Vector2> Points = new List<Vector2>();
        protected List<Vector2> edges = new List<Vector2>();
        protected Vector2 _point1;
        protected Vector2 _point2;
        protected Vector2 _point3;
        protected Vector2 _point4;


        //References to the Managers
        public IBehaviourManager _BehaviourManager;


        public override void Initialize(Texture2D tex, Vector2 posn, IBehaviourManager behaviours)
        {
            Position = posn;
            Texture = tex;
            _BehaviourManager = behaviours;
            UniqueData();
            Gravity = new Vector2(0, 2);
        }   //Initialises the objects, catching references to the managers
        public override void UniqueData()
        {
        }                    // Used to apply the specific variables such as animations onto the entity

        public override void OnColiEnter(IEntity ColiEnt, ISAT SAT)
        {
        }

        public override void OnCollision(IEntity collision)
        {
        }
        public override void OnTriggerEnter(IEntity collision)
        {
        }

        public override void Update(GameTime game)
        {
        }           //Update method, called every fram

        /// <summary>
        /// Calculate the vertices of an entity for a spritesheet
        /// </summary>
        /// <param name="Columns"></param>
        /// <param name="Rows"></param>
        public override void SetPoints(int Columns, int Rows)
        {
            Points.Clear();
            //Top Left
            _point1 = new Vector2(Position.X, Position.Y);
            //Top Right
            _point2 = new Vector2((Position.X + Texture.Width/Columns), Position.Y);
            //Bottom Right
            _point3 = new Vector2((Position.X + Texture.Width/Columns), (Position.Y + Texture.Height/Rows));
            //Bottom Left
            _point4 = new Vector2(Position.X, (Position.Y + Texture.Height/Rows));


            Points.Add(_point1);
            Points.Add(_point2);
            Points.Add(_point3);
            Points.Add(_point4);

            BuildEdges();
        }
        public override void BuildEdges()
        {
            Vector2 p1;
            Vector2 p2;
            edges.Clear();
            for (int i = 0; i < Points.Count; i++)
            {
                p1 = Points[i];
                if (i + 1 >= Points.Count)
                {
                    p2 = Points[0];
                }
                else
                {
                    p2 = Points[i + 1];
                }
                edges.Add(p2 - p1);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture , Position, Color.AntiqueWhite);
        }

        #region get/sets

        public override float Direction { get; set; }

        public override List<Vector2> Point()
        {
            return Points;
        }

        public override List<Vector2> Edges()
        {
            return edges;
        }

        public override Vector2 Center()
        {
            float totalX = 0;
            float totalY = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                totalX += Points[i].X;
                totalY += Points[i].Y;
            }
            return new Vector2(totalX / Points.Count, totalY / Points.Count);
        }
        #endregion



    }


}
