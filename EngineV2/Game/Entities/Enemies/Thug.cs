using Engine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectHastings.Behaviours;
using Engine.Collision_Management;
using Engine.Physics;

namespace ProjectHastings.Entities.Enemies
{
    class Thug : GameEntity, ICollidable, IPhysics
    {
        public EnemyMind Mind { get; private set; }
        public bool isTrigger { get; set; }


        /// <summary>
        /// Initialise the Variables specific to this object
        /// </summary>
        public override void UniqueData()
        {
            Tag = "thug";
            isTrigger = false;
            //Create the Mind and pass the state machine and this entity
            Mind = new EnemyMind(this);

        }

        public override void OnCollision(IEntity collision)
        {
            Mind.Attack(collision);
        }

        public override void Update(GameTime game)
        {
            // Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            Mind.Update(game);
            SetPoints(3,3);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Mind.Animate(spriteBatch);
        }
    }
}