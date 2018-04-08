using System.Collections.Generic;
using Engine.Collision_Manager;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectHastings.Entities.Interactive
{
    /// <summary>
    /// Lever Used to Trigger the event on an object
    /// </summary>
    class Lever : GameEntity
    {
        #region Instance Variables

        //BEHAVIOURS
        //LEVER RESPONSIBILITY CLASS
        IEntity target;

        private bool canTrigger = false;
        //Input Management
        private KeyboardState keyState;

        //Collision Management
        private IEntity collisionObj;
        private List<IEntity> playerObj;
        private List<IEntity> targetObjs;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;

        #endregion

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            //SUBSCRIBERS
            input.AddKeyListener(OnNewKeyInput);

        }

        /// <summary>
        /// Trigger Input Event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;
            if (canTrigger && keyState.IsKeyDown(Keys.H) || canTrigger && keyState.IsKeyDown(Keys.E))
            {
                for (int i = 0; i < targetObjs.Count; i++)
                {
                    //if (targetObjs[i].Tag == "leverObj")
                    //{
                    //        targetObjs[i].setYPos(105);

                    //}
                }
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

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }


        #region GET/SETS
        public override void setRow(int rows)
        {
            row = rows;
        }
        #endregion
    }
}
