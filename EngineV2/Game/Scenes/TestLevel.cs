using System;
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
using ProjectHastings.Entities.Interactive.Ladders;
using ProjectHastings.Entities.Player;

namespace ProjectHastings.Scenes
{
    class TestLevel : IScene
    {
        List<IEntity> Scenegraph = new List<IEntity>();
        List<IBehaviour> Behaviours = new List<IBehaviour>();

        //Managers
        IEntityManager entManager;
        IBackGrounds back;
        ICollidable collider;
        IPhysicsManager physicsMgr;

        IInputManager input = Locator.Instance.getProvider<InputManager>() as IInputManager;
        ICollisionManager coli = Locator.Instance.getProvider<CollisionManager>() as ICollisionManager;
        ISoundManager sound = Locator.Instance.getProvider<SoundManager>() as ISoundManager;
        IBehaviourManager behaviours = Locator.Instance.getProvider<BehaviourManager>() as IBehaviourManager;

        public TestLevel()
        {

            #region Instantiate Managers

            entManager = new EntityManager();

            physicsMgr = new PhysicsManager();

            collider = new CollidableClass();

            #endregion

            back = new BackGrounds(900, 600);

        }

        public void LoadContent(ContentManager Content)
        {
            //Sounds
            sound.Initialize("Level1BackgroundMusic", Content.Load<SoundEffect>("Level1BackgroundMusic"));
            sound.Initialize("Walk", Content.Load<SoundEffect>("Footsteps"));
            sound.Initialize("Crate", Content.Load<SoundEffect>("CratePushSFX"));
            sound.Initialize("Exit", Content.Load<SoundEffect>("ExitLevelSFX"));
            sound.Initialize("Key", Content.Load<SoundEffect>("KeyPickupSFX"));
            sound.Initialize("Ladder", Content.Load<SoundEffect>("LadderClimbSFX"));
            sound.CreateInstance();

            //BackGround
            back.Initialize("Background", Content.Load<Texture2D>("BackgroundTex1"));

            //Ladders
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(200, 110), collider, behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("LLadderTex"), new Vector2(400, 107), collider, behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("LLadderTex"), new Vector2(675, 107), collider, behaviours);


            //Door
            entManager.CreateEnt<Door>(Content.Load<Texture2D>("Door"), new Vector2(850, 555), collider, behaviours);

            //Key
            entManager.CreateEnt<Key>(Content.Load<Texture2D>("Key"), new Vector2(850, -30), collider, behaviours);

            //Platforms          
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("XLPlatformTex"), new Vector2(0, 595), collider, behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(695, 475), collider, behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("XLPlatformTex"), new Vector2(0, 355), collider, behaviours);
            entManager.CreateEnt<TriggerPlatform>(Content.Load<Texture2D>("XLPlatformTex"), new Vector2(400, -10), collider, behaviours);
            entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(-4, 107), collider, behaviours);
            //INTERACTIVE OBJECTS

            //Crates
            entManager.CreateEnt<Crate>(Content.Load<Texture2D>("crate"), new Vector2(10, 80), collider, behaviours);

            //Pressure Plates
            entManager.CreateEnt<PressurePlate>(Content.Load<Texture2D>("PPlateTex"), new Vector2(10, 355), collider, behaviours);

            //Walls
            entManager.CreateEnt<TriggerWall>(Content.Load<Texture2D>("Wall"), new Vector2(705, 357), collider, behaviours);

            //Lever
            entManager.CreateEnt<Lever>(Content.Load<Texture2D>("Lever"), new Vector2(840, 450), collider, behaviours);

            //The Player
            entManager.CreateEnt<Player>(Content.Load<Texture2D>("Chasting"), new Vector2(50, 100), collider, behaviours);

            //Enemies
            entManager.CreateEnt<Thug>(Content.Load<Texture2D>("Thug"), new Vector2(630, 564), collider, behaviours);

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
            }

        }

        public void UnloadContent()
        {

        }

        public void update(GameTime gameTime)
        {
            //Update Input
            input.Update();
            //Update COllisions
            coli.update();

            //Call the Update method for the physics Manager
            physicsMgr.Update();
            //Call the Update method for the Behaviour Manager
            behaviours.Update();

            //Call the Update method for each entity in the Scengrapgh list
            foreach (var entity in Scenegraph)
            {
                entity.Update(gameTime);
            }



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
