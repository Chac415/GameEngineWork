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

        //Lists
        private List<IEntity> player;
        private List<IEntity> environment;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;

        public override void UniqueData()
        {
            Tag = "Crate";
            input.AddKeyListener(OnNewKeyInput);
            GravityBool = true;
            isTrigger = false;
        }

        /// <summary>
        /// Event Handler for Keyboard Input
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;
            if (crateContact && keyState.IsKeyDown(Keys.H) || crateContact && keyState.IsKeyDown(Keys.E))
            {

                moveObject = true;

                if (crateContact && keyState.IsKeyDown(Keys.D) || moveObject && keyState.IsKeyDown(Keys.Right))
                {
                    Position += new Vector2(3, 0);
                    //sound.Playsnd("Crate", 0.2f, true);
                }
                if (crateContact && keyState.IsKeyDown(Keys.A) || moveObject && keyState.IsKeyDown(Keys.Left))
                {
                    Position += new Vector2(-3, 0);
                    //sound.Playsnd("Crate", 0.2f, true);
                }

            }

            if (crateContact == false)
            {
               // sound.Stopsnd("Crate");
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
