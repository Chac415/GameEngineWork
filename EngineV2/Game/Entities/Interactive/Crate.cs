using System.Collections.Generic;
using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine.Physics;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Entities.Interactive
{
    class Crate : GameEntity, ICollidable, IPhysics
    {
        private float moveDirec = 3;
        private bool moveObject = false;
        private bool canMove = true;
        private bool crateContact = false;


        //Input Management
        private KeyboardState keyState;
        public bool isTrigger { get; set; }
        //Collision Management

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;

        public override void UniqueData()
        {
            Tag = "Crate";
            GravityBool = true;
            isTrigger = true;
            input.AddKeyListener(OnNewKeyInput);
        }

        public override void OnTriggerEnter(IEntity collision)
        {
            if (collision.Tag == "Environment")
            {
                GravityBool = false;
            }
            else
            {
                //  GravityBool = true;
            }

            if (collision.Tag == "Player" && keyState.IsKeyDown(Keys.E))
            {
                ApplyForce(new Vector2(((Player.Player)collision).mind.speed * -1, 0));
            }

        }
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;
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
