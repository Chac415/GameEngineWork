using System.Collections.Generic;
using Engine.Collision_Manager;
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
    class Key : GameEntity
    {
        #region Instance Variables
        public static bool Unlock = false;
        public bool gravity = true;


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
            //_PhysicsObj.hasPhysics(this);
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
