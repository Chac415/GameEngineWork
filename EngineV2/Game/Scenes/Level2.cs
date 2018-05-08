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
            //Ladders -------  Left > Right
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(354, 159), behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(105, 310), behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(670, 355), behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(230, 450), behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(655, 55), behaviours);
            entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(475, 55), behaviours);
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(100, 470), behaviours);
            //entManager.CreateEnt<Ladder>(Content.Load<Texture2D>("SLadderTex"), new Vector2(145, 470), behaviours);
            //Door
            entManager.CreateEnt<Door>(Content.Load<Texture2D>("Door"), new Vector2(300, 555), behaviours);

            //Platforms  Top of screen to Bottom  & Left to Right

            //Top Layer
            // entManager.CreateEnt<Platform>(Content.Load<Texture2D>("MPlatformTex"), new Vector2(0, 80), behaviours);
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
            entManager.CreateEnt<Crate>(Content.Load<Texture2D>("crate"), new Vector2(210, 130), behaviours);
            entManager.CreateEnt<PressurePlate>(Content.Load<Texture2D>("PPlateTex"), new Vector2(100, 148), behaviours);


            //Enemies
            entManager.CreateEnt<Thug>(Content.Load<Texture2D>("Thug"), new Vector2(630, 564), behaviours);
            entManager.CreateEnt<Thug>(Content.Load<Texture2D>("Thug"), new Vector2(90, 133), behaviours);


            //The Player
            entManager.CreateEnt<Player>(Content.Load<Texture2D>("Chasting"), new Vector2(50, 405), behaviours);

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
