﻿using System.Collections.Generic;
using Engine.Collision_Manager;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Behaviours.Player_Behaviours;

namespace ProjectHastings.Entities.Player
{
    /// <summary>
    /// Class for Player Entity
    /// 
    /// Author: Nathan Robertson & Carl Chalmers
    /// Date of change: 03/02/18
    /// Version 0.5
    /// 
    /// </summary>
    public class Player : GamePhysicsEntity, ICollidable
    {
        #region Properties
        //Animation Variable
        public IAnimations ani;
        public new static int row = 1;
        public static bool Animate = false;


        //Movement
        public static bool canClimb = false;
        public bool sprint = false;

        //Jump Variables
        private float jumpForce = 10;
        private float maxJump = 120;
        private bool canJump = false;
        private bool isJumping = false;
        private float jumpHeight = 0;

        //Input Management
        private KeyboardState keyState;

        //Collision Lists
        private List<IEntity> collisionObjs;
        private List<IEntity> interactiveObjs;
        private List<IEntity> environment;

        private IEntity collision;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;

        #endregion

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "Player";
            speed = 3;
            _BehaviourManager.createMind<PlayerMind>(this);


            // CollisionManager.GetColliderInstance.subscribe(onCollision);
            input.AddKeyListener(OnNewKeyInput);
        }

        /// <summary>
        /// Event Handler for Keyboard Input
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;


            #region SPACEBAR
            if (keyState.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                jump();
            }
            #endregion

            #region LEFT SHIFT
            if (keyState.IsKeyDown(Keys.LeftShift))
            {
                sprint = true;
            }
            else if (keyState.IsKeyUp(Keys.LeftShift))
            {
                sprint = false;
            }
            #endregion


            if (keyState.GetPressedKeys().Length == 0)
            {
                sound.Stopsnd("Walk");
                sound.Stopsnd("Ladder");
            }
        }


        #region Behaviours
        /// <summary>
        /// Moves the Player up onthe Y axis up to a maximum point
        /// </summary>
        public void jump()
        {

            if (canJump)
            {
                //            gravity = false;
                if (isJumping)
                {
                    Position -= new Vector2(0, jumpForce);
                    jumpHeight += jumpForce;
                    Position += new Vector2(0, jumpForce);
                }

                if (jumpHeight >= maxJump)
                {

                    canJump = false;
                    jumpHeight = 0;
                }
            }
        }

        #endregion

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
            Hitbox = new Rectangle((int)Position.X - 25, (int)Position.Y - 25, Texture.Width/2, Texture.Height/2);
            SetPoints();
        }

        #region get/sets
        public override int getRows()
        {
            return row;
        }
        public override void setRow(int rows)
        {
            row = rows;
        }

        #endregion


    }

}