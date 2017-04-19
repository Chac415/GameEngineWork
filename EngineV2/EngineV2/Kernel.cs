﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using EngineV2.Interfaces;
using EngineV2.Managers;
using EngineV2.Entities;
using EngineV2.Behaviours;
using EngineV2.Collision_Management;
using EngineV2.Input;
using EngineV2.Physics;


namespace EngineV2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        #region Instance Variables
        //Constants
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Interfaces

        IEntity player;
        IEntity enemy;
        IEntity crate;
        IEntityManager ent;
        ISceneManager scn;
        CollisionManager col;
        ICollidable collider;
        InputManager inputMgr;
        IBehaviourManager behaviours;
        IAnimationMgr animation;
        ISoundManager snd;
        PhysicsManager physicsMgr;
        IPhysicsObj physicsObj;

        //Screen Size
        int screenWidth = 900;
        int screenHeight = 600;

        #endregion


        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            inputMgr = new InputManager();
            ent = new EntityManager();
            col = new CollisionManager();
            physicsObj = new PhysicsObj();
            physicsMgr = new PhysicsManager(physicsObj);
            scn = new SceneManager(this, inputMgr, col, physicsMgr);
            player = ent.CreateEnt<Player>();
            enemy = ent.CreateEnt<Enemy>();
            crate = ent.CreateEnt<Crate>();
            behaviours = new BehaviourManager();
            animation = new AnimationMgr();
            snd = new SoundManager();
            collider = new CollidableClass();

            Components.Add((GameComponent)scn);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //PLAYER AND ENEMIES
            snd.Initialize(Content.Load<SoundEffect>("background"));
            snd.Initialize(Content.Load<SoundEffect>("Footsteps"));

            player.applyEventHandlers(inputMgr, col);
            enemy.applyEventHandlers(inputMgr, col);

            player.Initialize(Content.Load<Texture2D>("Chasting"),new Vector2(200, 400), collider, snd);
            enemy.Initialize(Content.Load<Texture2D>("Enemy"), new Vector2(100, 564),collider, snd);
            
            animation.Initialize(player, 3, 3);
            animation.Initialize(enemy, 3, 3);
            
            scn.Initalize(player,behaviours, animation);
            scn.Initalize(enemy,behaviours, animation);

            collider.isCollidableEntity(player); //0
            collider.isCollidableEntity(enemy);  //1

            physicsObj.hasPhysics(player);

            behaviours.createMind<EnemyMind>(enemy);

            //INTERACTIVE OBJECTS

            crate.applyEventHandlers(inputMgr, col);

            crate.Initialize(Content.Load<Texture2D>("crate"), new Vector2(300, 500), collider, snd);
                
            //animation.Initialize(crate, 0,0);
            scn.Initalize(crate, behaviours, animation);

            collider.isInteractiveCollidable(crate);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
    }
}