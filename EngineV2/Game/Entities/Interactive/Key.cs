﻿using System;
using System.Collections.Generic;
using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectHastings.Entities.Interactive
{
    /// <summary>
    /// Interactive object used to Unlock Doors
    /// Author: Nathan Roberson & Carl Chalmers
    /// Date of Change: 03/02/18
    /// Version: 0.4
    /// </summary>
    class Key : GameEntity, ICollidable
    {
        #region Instance Variables

        public static bool Unlock = false;
        public bool gravity = true;

        public bool isTrigger { get; set; }

        //Input Management
        private KeyboardState keyState;

        //Collision Management Variables




        //Lists
        private List<IEntity> interactiveObjs;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;



        #endregion

        public override void UniqueData()
        {
            Tag = "Key";
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

        public override void OnTriggerEnter(IEntity collision)
        {
            if (collision.Tag == "Player")
            {
                Unlock = true;
                sound.Playsnd("Key", 0.2f, false);
                EntityManager.Entities.Remove(this);
            }
        }

        /// <summary>
        /// Called Every Frame
        /// </summary>
        /// <param name="game"></param>
        public override void Update(GameTime game)
        {
            SetPoints(1, 1);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }


    }
}
