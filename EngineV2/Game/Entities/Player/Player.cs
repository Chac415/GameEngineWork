using System.Collections.Generic;
using Engine.Animations;
using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Physics;
using Engine.Service_Locator;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Engine.State_Machines.Test_States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectHastings.Behaviours.Player_Behaviours;
using ProjectHastings.Behaviours.Player_Behaviours.Health;

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
    public class Player : GameEntity, ICollidable, IPhysics
    {
        #region Properties

        //Movement
        public static bool canClimb = false;
        public bool sprint = false;
        public bool isTrigger { get; set; }

        //Jump Variables
        private float jumpForce = 10;
        private float maxJump = 120;
        private bool canJump = false;
        private bool isJumping = false;
        private float jumpHeight = 0;
        private float timer = 0;

        
        public float Timer { get { return timer; } set { timer = value; } }
        private KeyboardState keyState;
        private IAnimation SpriteSheet;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;


        //Behaviours & scripts
        public PlayerMind mind;
        public IDamageable healthScript;



        #endregion

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "Player";
            mind = new PlayerMind(this);
            isTrigger = false;
            GravityBool = true;
            // CollisionManager.GetColliderInstance.subscribe(onCollision);
            input.AddKeyListener(OnNewKeyInput);
            healthScript = new HealthScript();
        }

        public override void OnCollision(IEntity collision)
        {
            mind.collisionResults(collision);
        }

        /// <summary>
        /// Event Handler for Keyboard Input
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        public virtual void OnNewKeyInput(object source, KeyEventData data)
        {
            keyState = data._newKey;

            if (keyState.IsKeyDown(Keys.W) && canClimb)
            {
                ApplyForce(new Vector2(0, 10));
            }
            if (keyState.IsKeyDown(Keys.S) && canClimb)
            {
                ApplyForce(new Vector2(0, 3));
            }
            #region SPACEBAR
            if (keyState.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                jump();
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
            mind.Animate(spriteBatch);
                //spriteBatch.Draw(Texture, Position, Color.AntiqueWhite);
        }
        /// <summary>
        /// Called Every Frame
        /// </summary>
        /// <param name="game"></param>
        public override void Update(GameTime game)
        {
            Timer += game.ElapsedGameTime.Seconds;
       //     Hitbox = new Rectangle((int)Position.X - 25, (int)Position.Y - 25, Texture.Width/2, Texture.Height/2);
            SetPoints(3,3);
            mind.Update(game);
        }

    }

}