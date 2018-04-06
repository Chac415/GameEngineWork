using Engine.Collision_Management;
using Engine.Interfaces;
using Engine.Physics;
using Engine.Service_Locator;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Engine.State_Machines.Test_States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectHastings.Behaviours;

namespace ProjectHastings.Entities.Enemies
{
    class Thug : GamePhysicsEntity
    {
        public IAnimations ani;

        private IEntity collisionObj;

        private EnemyMind mind;
        public IStateMachine<IPhysics> StateMachine;

        ICollisionManager coli = Locator.Instance.getProvider<CollisionManager>() as ICollisionManager;

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "thug";
            //ani = new ThugAnimation();
            //ani.Initialize(this, 3, 3);

            StateMachine = new StateMachine<IPhysics>(this);

            StateMachine.AddState(new AnimationState<IPhysics>(this, Texture, 2, 1, 3), new MoveLeft<IPhysics>(), "left");
            StateMachine.AddState(new AnimationState<IPhysics>(this, Texture, 2, 2, 3), new MoveRight<IPhysics>(), "right");


            mind = new EnemyMind(this, StateMachine);
            _Collisions.isCollidableEntity(this);
            coli.subscribe(onCollision);
        }

        public virtual void onCollision(object source, CollisionEventData data)
        {
            collisionObj = data.objectCollider;

            //if (Hitbox.X > 850)
            //{
            //    Position = new Vector2(849, Position.Y);
            //    row = 0;
            //    Behaviours.EnemyMind.speed *= -1;
            //}
            //if (Hitbox.X < 0)
            //{
            //    Position = new Vector2(1, Position.Y);
            //    row = 1;
            //    Behaviours.EnemyMind.speed *= -1;
            //}

        }

        public override void Update(GameTime game)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            StateMachine.UpdateBehaviour();
        }

        //public override int getRows()
        //{
        //    return row;
        //}

        //public override void setRow(int rows)
        //{
        //    row = rows;
        //}

        public override void Draw(SpriteBatch spriteBatch)
        {
            StateMachine.UpdateAnimation(spriteBatch);
           StateMachine.DrawAnimation(spriteBatch);
        }
    }
}