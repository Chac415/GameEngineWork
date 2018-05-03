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
    class Thug : GameEntity, ICollidable
    {

        public IEntity CollisionObj { get; private set; }

        public EnemyMind Mind { get; private set; }
        public IAnimation SpriteSheet { get;}

        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "thug";

            //Create the Mind and pass the state machine and this entity
            Mind = new EnemyMind(this);

        }

        public override void Update(GameTime game)
        {
            Hitbox = new Rectangle((int)Position.X - 25, (int)Position.Y - 25, Texture.Width/2, Texture.Height/2);
            Mind.stateMachine.UpdateBehaviour();
            SetPoints(3,3);
            Mind.stateMachine.UpdateAnimation(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Mind.stateMachine.DrawAnimation(spriteBatch);
        }
    }
}