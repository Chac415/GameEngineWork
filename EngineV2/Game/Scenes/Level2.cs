using System.Collections.Generic;
using Engine.BackGround;
using Engine.Collision_Management;
using Engine.Input_Managment;
using Engine.Interfaces;
using Engine.Managers;
using Engine.Physics;
using Engine.Service_Locator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectHastings.Entities.Enemies;
using ProjectHastings.Entities.Environment;
using ProjectHastings.Entities.Interactive;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Scenes
{
    class Level2 : IScene
    {
        List<IEntity> Scenegraph = new List<IEntity>();
        List<IBehaviour> Behaviours = new List<IBehaviour>();

        //Managers
        IEntityManager entManager;
        IBackGrounds back;
        IPhysicsManager physicsMgr;
        private ContentManager Content;

        ICollisionManager coli;
        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IBehaviourManager behaviours = Locator.Instance.getProvider<BehaviourManager>() as IBehaviourManager;

        public Level2(int ScreenWidth, int ScreenHeight, ContentManager content, ISceneManager scn)
        {

            #region Instantiate Managers

            entManager = new EntityManager();

            physicsMgr = new PhysicsManager();

            coli = new CollisionManager(new QuadTree(2, 4, new Rectangle(0, 0, ScreenWidth, ScreenHeight)));

            #endregion

            back = new BackGrounds(ScreenWidth, ScreenHeight);
            Content = content;

        }

        public void LoadContent()
        {
            //Sounds
            sound.Initialize("Level1BackgroundMusic", Content.Load<SoundEffect>("Level1BackgroundMusic"));
            sound.Initialize("Walk", Content.Load<SoundEffect>("Footsteps"));
            sound.Initialize("Exit", Content.Load<SoundEffect>("ExitLevelSFX"));
            sound.Initialize("Key", Content.Load<SoundEffect>("KeyPickupSFX"));
            sound.Initialize("Ladder", Content.Load<SoundEffect>("LadderClimbSFX"));
            sound.Initialize("CratePush", Content.Load<SoundEffect>("CratePushSFX"));
            sound.CreateInstance();

            //BackGround
            back.Initialize("Background", Content.Load<Texture2D>("BackgroundTex1"));

            //Ladders
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(200, 110), behaviours);
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(675, 355),  behaviours);
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(100, 470), behaviours);
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(145, 470), behaviours);
            //Door
            //entManager.CreateEnt<Door>(Content.Load<Texture2D>("Door"), new Vector2(850, 555), behaviours);

            //Key
            //entManager.CreateEnt<Key>(Content.Load<Texture2D>("Key"), new Vector2(20, 560), behaviours);

            //Wall
            // entManager.CreateEnt<TriggerWall>(Content.Load<Texture2D>("Wall"), new Vector2(130, 470), behaviours);

            //Platforms  Top of screen to Bottom  & Left to Right

            //Top Layer
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(0, 80), behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(675, 80), behaviours);

            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(0, 150), behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(125, 150), behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(250, 150), behaviours);

            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(375, 300), behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(500, 300), behaviours);

            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(125, 400), behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(250, 400), behaviours);

            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(0, 550), behaviours);

            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("XLPlatformTex"), new Vector2(0, 595), behaviours);


            //entManager.CreateEnt<Platform>(Content.Load<Texture2D>("XLPlatformTex"), new Vector2(0, 355),  behaviours);
            // entManager.CreateEnt<Platform>(Content.Load<Texture2D>("Platform"), new Vector2(100, 470), behaviours);
            //INTERACTIVE OBJECTS
            entManager.CreateEnt<Crate>(Content.Load<Texture2D>("crate"), new Vector2(30, 52), behaviours);

            //The Player
            entManager.CreateEnt<Player>(Content.Load<Texture2D>("Chasting"), new Vector2(10, 583),  behaviours);

            //Enemies
            entManager.CreateEnt<Thug>(Content.Load<Texture2D>("Thug"), new Vector2(630, 564), behaviours);

            Scenegraph.AddRange(EntityManager.Entities);
            Behaviours = BehaviourManager.behaviours;



            foreach (var entity in Scenegraph)
            {
                //If Entity is of Type IPhysics
                if (entity is IPhysics)
                {
                    //Add Entites to the Physics List in the Physics Manager
                    physicsMgr.AddToList((IPhysics)entity);
                }

                if (entity is ICollidable)
                {
                    coli.hasCollisions(entity);
                }
            }

        }

        public void update(GameTime gameTime)
        {
            //Update Input
            input.Update();

            //Call the Update method for the physics Manager
            physicsMgr.Update();
            //Call the Update method for the Behaviour Manager
            behaviours.Update();

            //Call the Update method for each entity in the Scengrapgh list
            foreach (var entity in Scenegraph)
            {
                entity.Update(gameTime);
            }

            //Update COllisions
            coli.Update();


        }


        public void Draw(SpriteBatch spriteBatch)
        {

            back.Draw(spriteBatch);


            for (int i = 0; i < Scenegraph.Count; i++)
            {
                Scenegraph[i].Draw(spriteBatch);
            }


        }


    }
}
