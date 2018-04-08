using Engine.Animations;
using Engine.Collision_Manager;
using Engine.Interfaces;
using Engine.Physics;
using Engine.State_Machines;
using Engine.State_Machines.Animations;
using Engine.State_Machines.Test_States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectHastings.Behaviours;

namespace ProjectHastings.Entities.Enemies
{
    class Thug : GamePhysicsEntity, ICollidable
    {

        public IEntity CollisionObj { get; private set; }

        public EnemyMind Mind { get; private set; }
        private IAnimation SpriteSheet;
        public IStateMachine<IPhysics> StateMachine;

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "thug";

            //Initialise the spriteSheet animation
            SpriteSheet = new SpriteSheetAnimation(Texture);
            //Create a new instance of State Machine
            StateMachine = new StateMachine<IPhysics>(this);

            //Add the states to the State Machine
            StateMachine.AddState(new AnimationState(this, SpriteSheet, 12, 1, 2f), new MoveLeft<IPhysics>(), "left");
            StateMachine.AddState(new AnimationState(this, SpriteSheet, 12, 0, 2f),new MoveRight<IPhysics>(), "right");

            //Create the Mind and pass the state machine and this entity
            Mind = new EnemyMind(this, StateMachine);

        }

        public override void Update(GameTime game)
        {
            Hitbox = new Rectangle((int)Position.X - 25, (int)Position.Y - 25, Texture.Width/2, Texture.Height/2);
            StateMachine.UpdateBehaviour();
            SetPoints();
            StateMachine.UpdateAnimation(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            StateMachine.DrawAnimation(spriteBatch);
        }
    }
}