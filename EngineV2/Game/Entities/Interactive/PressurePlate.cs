using System.Collections.Generic;
using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Physics;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectHastings.Entities.Interactive
{
    /// <summary>
    /// Trigger Object
    /// </summary>
    class PressurePlate : GameEntity, ICollidable, IPhysics
    {
        private float moveDirec = 3;
        private bool canMove = true;
        private bool crateContact = false;

        public bool isTrigger { get; set; }

        //Input Management
        private KeyboardState keyState;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "PressurePlate";
            isTrigger = true;
            GravityBool = false;
        }

        public override void OnTriggerEnter(IEntity collision)
        {
            if (collision.Tag == "Crate")
            {
                //DO STUFF
            }
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
            SetPoints(1, 1);
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}