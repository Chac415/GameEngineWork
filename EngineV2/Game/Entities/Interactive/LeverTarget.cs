using System.Collections.Generic;
using Engine.Collision_Management;
using Engine.Interfaces;
using Engine.Physics;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectHastings.Entities.Interactive
{
    /// <summary>
    /// ojbect which is triggered by lever
    /// </summary>
    class LeverTarget : GameEntity
    {
        //Tag Identifier
        public string tag = "LeverTarget";

        //COLLISIONS


        //PHYSICS
        private IPhysics physics;

        //LISTS
        private List<IEntity> physicsObjs;


        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            //physicsObjs = _PhysicsObj.getPhysicsList();

        }

        /// <summary>
        /// Draws the entty based on the texture and position obtained from the animation class
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.AntiqueWhite);
        }

        /// <summary>
        /// Called Every Frame
        /// </summary>
        /// <param name="game"></param>
        public override void Update(GameTime game)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}

