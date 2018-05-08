using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace Engine.Entity_Management
{
    public abstract class Entity : IEntity
    {
        public abstract void Initialize(Texture2D Tex, Vector2 Posn, IBehaviourManager behaviours);

        public abstract void Update(GameTime game);
        public abstract Rectangle Hitbox { get; set; }
        public abstract string Tag { get; set; }
        public abstract void SetPoints(int Columns, int Rows);
        public abstract void BuildEdges();
        public abstract void UniqueData();
        public abstract void OnColiEnter(IEntity ColiEnt, ISAT SAT);

        //Animations
        public abstract float Direction { get; set; }
        public abstract List<Vector2> Point();
        public abstract List<Vector2> Edges();
        public abstract Vector2 Center();

        //Virtual Variables
        public abstract Vector2 Position { get; set; }
        public abstract Texture2D Texture { get; set; }
        
        //Virtual Methods
        public abstract void Draw(SpriteBatch spriteBatch);

        #region Collisions
        public abstract void OnCollision(IEntity collision);
        public abstract void OnTriggerEnter(IEntity collision);

        #endregion

        #region Physics content

        private Vector2 gravity;

        public virtual Vector2 Velocity { get; set; }
        public virtual Vector2 Acceleration { get; set; }
        public Vector2 Gravity { get { return gravity; } set { gravity = value; } }
        public virtual bool GravityBool { get; set; }

        //Inverse mass to encourage multiplication and not diviision due to multiplication being faster
        protected float InverseMass = -1.5f;
        protected float Restitution = 1f;
        protected float Damping = 0.5f;

        protected void SetGravity(Vector2 grav)
        {
            Gravity = grav;
        }
        /// <summary>
        /// Apply force to an entity commonly used for movement
        /// </summary>
        /// <param name="force"></param>
        public virtual void ApplyForce(Vector2 force)
        {
            //Multiply force by the inversemass to obtain the acceleration value
            Acceleration += force * InverseMass;
        }
        /// <summary>
        /// Used for moving entities based off of collisions
        /// </summary>
        /// <param name="closingVelo"></param>
        public virtual void ApplyImpulse(Vector2 closingVelo)
        {
            //Apply Impulse by setting the velocy to the closing velocity by the Restitution of the entity
            Velocity = closingVelo * Restitution;
        }

        /// <summary>
        /// Update the physics of the entity
        /// </summary>
        public virtual void UpdatePhysics()
        {
            Velocity += Acceleration;
            Velocity *= Damping;
            Position += Velocity;

            if (GravityBool)
                Acceleration = gravity;
            else Acceleration = new Vector2(0, 0);



        }

        #endregion     

    }
}

