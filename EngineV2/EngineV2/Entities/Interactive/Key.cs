﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EngineV2.Interfaces;
using EngineV2.Managers;
using EngineV2.Collision_Management;
using EngineV2.Entities;
using EngineV2.Input;
using EngineV2.Input_Managment;


namespace EngineV2.Behaviours
{
    /**
    * on collision with player and keyboard input Enter the next scene 
    *
    *
    */
    class Key : GameEntity
    {
        #region Instance Variables
        public bool keyContact = false;
        public static bool Unlock = false;
        public bool gravity = true;


        //Input Management
        private KeyboardState keyState;
        private InputManager InputMgr;

        //Collision Management Variables
        private IEntity collisionObj;
        private CollisionManager collisionMgr;
        private ICollidable colliders;
        private ISoundManager sound;



        //Lists
        private List<IEntity> interactiveObjs;

        #endregion

        //OnKeyboard Event Change scene

        public override void Initialize(Texture2D Tex, Vector2 Posn, ICollidable _collider, ISoundManager snd, IPhysicsObj phys, IBehaviourManager behaviours)
        {
            Position = Posn;
            Texture = Tex;
            colliders = _collider;
            sound = snd;

            //SUBSCRIBERS
            InputMgr = InputManager.GetInputInstance;
            InputMgr.AddListener(OnNewInput);
            collisionMgr.subscribe(onCollision);

            //CALL COLLIDABLEOBJS()
            CollidableObjs();
            phys.hasPhysics(this);


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.AntiqueWhite);
        }

        public override void update()
        {
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        #region Player Collision


        #endregion


        #region EVENTS

        //INITIALISE EVENT HANDLERS
        public override void applyEventHandlers(CollisionManager collisions)
        {
            collisionMgr = collisions;
        }

        //INPUT EVENTS
        public virtual void OnNewInput(object source, EventData data)
        {
            keyState = data.newKey;
            if (keyContact)
            {
                sound.Volume(4, 0.5f);
                sound.Playsnd(4);
                Unlock = true;

            }
            if (keyContact == false)
            {
                sound.Stopsnd(4);
            }
        }

        //INITIALISE INTERACTIVEOBJS LIST
        public override void CollidableObjs()
        {
            interactiveObjs = colliders.getPlayableObj();
        }

        //COLLISION EVENTS
        public virtual void onCollision(object source, CollisionEventData data)
        {
            collisionObj = data.objectCollider;
            gravity = true;

            for (int i = 0; i < interactiveObjs.Count; i++)
            {
                //checks to see if player is in contact with the door 
                if (HitBox.Intersects((interactiveObjs[0].getHitbox())))
                {
                    keyContact = true;
                    EntityManager.Entities.Remove(this);
                }
                else
                {
                    keyContact = false;
                }
            }
        }
        #endregion
        #region GET/SETS
        public override bool getGrav()
        {
            return gravity;
        }
        public override void setGrav(bool active)
        {
            gravity = active;
        }
        #endregion
    }
}
