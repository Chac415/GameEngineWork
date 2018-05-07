﻿using Engine.Animations;
using Engine.Collision_Management;
using Engine.Interfaces;
using Engine.Physics;
using Engine.Service_Locator;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Engine.State_Machines.Test_States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectHastings.Behaviours;

namespace ProjectHastings.Entities.Enemies
{
    class Thug : GamePhysicsEntity
    {

        public IEntity CollisionObj { get; private set; }

        public EnemyMind Mind { get; private set; }


        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "thug";

            //Create the Mind and pass the state machine and this entity
            Mind = new EnemyMind(this);

        }

        public override void Update(GameTime game)
        {
            // Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            Mind.Update(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Mind.Animate(spriteBatch);
        }
    }
}