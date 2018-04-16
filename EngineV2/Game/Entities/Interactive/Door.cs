using System.Collections.Generic;
using Engine.Buttons;
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
    /// INteractive object that changes the scene
    /// Author: Nathan Roberson & Carl Chalmers
    /// Date of Change: 03/02/18
    /// Version: 0.4
    /// </summary>
    class Door : GameEntity
    {
        #region Instance Variables
        public bool doorContact = false;


        //Input Management
        private KeyboardState keyState;

        //Collision Management Variables
        private IEntity collisionObj;


        //Lists
        private List<IEntity> interactiveObjs;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;

        #endregion

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            input.AddKeyListener(OnNewKeyInput);
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

        /// <summary>
        /// Trigger Input Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;

            //if Plyaer has unlocked the door, change scene and play audio instance
            if (doorContact && keyState.IsKeyDown(Keys.W) && Key.Unlock || doorContact && keyState.IsKeyDown(Keys.Up) && Key.Unlock)
            {
                sound.Playsnd("Exit", 0.5f, false);
                EntityManager.Entities.Clear();
                BehaviourManager.behaviours.Clear();
                ButtonList.Buttons.Clear();


                //doorContact = false;

            }
            if (doorContact == false)
            {
                sound.Stopsnd("Exit");
            }
        }


    }
}
