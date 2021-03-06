﻿using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectHastings.Entities.Interactive
{
    /// <summary>
    /// Interactive object for the player allowing them to go up and down the y axis
    /// Author: Nathan Roberson & Carl Chalmers
    /// Date of Change: 03/02/18
    /// Version: 0.4
    /// </summary>
    class Ladder : GameEntity, ICollidable
    {
        #region Instance Variables

        //Input Management
        private KeyboardState keyState;

        //Collision Management Variables

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        public bool isTrigger { get; set; }

        #endregion

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "Ladder";
            isTrigger = true;
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
            SetPoints(1,1);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}
